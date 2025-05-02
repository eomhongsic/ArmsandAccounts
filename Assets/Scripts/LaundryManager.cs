using System.Collections.Generic;
using UnityEngine;

public class LaundryManager : MonoBehaviour
{
    public static LaundryManager Instance;

    private class LaundryTask
    {
        public string cityName;
        public int amount;
        public float startTime;
        public float duration;
        public bool isComplete => Time.time >= startTime + duration;
    }

    private List<LaundryTask> activeTasks = new List<LaundryTask>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        for (int i = activeTasks.Count - 1; i >= 0; i--)
        {
            if (activeTasks[i].isComplete)
            {
                CompleteTask(activeTasks[i]);
                activeTasks.RemoveAt(i);
            }
        }
    }

    public void StartLaundry(string cityName, int amount, float duration = 10f)
    {
        // 자금 확인 및 차감
        if (FinanceManager.Instance.GetDirtyMoney() < amount)
        {
            Debug.LogWarning("❌ 세탁 시작 실패: 자금 부족");
            return;
        }

        FinanceManager.Instance.AddDirtyMoney(-amount);

        var task = new LaundryTask
        {
            cityName = cityName,
            amount = amount,
            startTime = Time.time,
            duration = duration
        };

        activeTasks.Add(task);
        Debug.Log($"🧼 세탁 시작: {amount} in {cityName} (완료까지 {duration}s)");
    }

    private void CompleteTask(LaundryTask task)
    {
        FinanceManager.Instance.AddCleanMoney(task.amount);
        Debug.Log($"✅ 세탁 완료: {task.amount} in {task.cityName}");
    }

    public List<(string cityName, int amount, float progress)> GetActiveTasks()
    {
        List<(string, int, float)> result = new List<(string, int, float)>();
        foreach (var task in activeTasks)
        {
            float progress = Mathf.Clamp01((Time.time - task.startTime) / task.duration);
            result.Add((task.cityName, task.amount, progress));
        }
        return result;
    }
}