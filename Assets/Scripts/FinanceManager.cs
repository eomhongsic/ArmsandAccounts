using System.Collections.Generic;
using UnityEngine;

public class FinanceManager : MonoBehaviour
{
    public static FinanceManager Instance;

    private int cleanMoney = 0;
    private int dirtyMoney = 0;

    private float incomeInterval = 5f; // 5초마다 수익 발생
    private float timer = 0f;

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
        timer += Time.deltaTime;
        if (timer >= incomeInterval)
        {
            timer = 0f;
            GenerateIncome();
        }
    }

    private void GenerateIncome()
    {
        List<string> cities = OfficeManager.Instance.GetAllOffices();
        int total = 0;

        foreach (string city in cities)
        {
            int income = 500; // 더미 수익
            dirtyMoney += income;
            total += income;
        }

        Debug.Log($"💰 총 수익: ${total} (Dirty Money)");

        TopBarUI.Instance?.UpdateMoney(cleanMoney, dirtyMoney);
    }

    public void AddCleanMoney(int amount)
    {
        cleanMoney += amount;
        TopBarUI.Instance?.UpdateMoney(cleanMoney, dirtyMoney);
    }

    public void AddDirtyMoney(int amount)
    {
        dirtyMoney += amount;
        TopBarUI.Instance?.UpdateMoney(cleanMoney, dirtyMoney);
    }

    public int GetCleanMoney() => cleanMoney;
    public int GetDirtyMoney() => dirtyMoney;
}