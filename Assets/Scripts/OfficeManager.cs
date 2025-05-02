using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public static OfficeManager Instance;

    private HashSet<string> installedOffices = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool HasOffice(string cityName)
    {
        return installedOffices.Contains(cityName);
    }

    public void InstallOffice(string cityName)
    {
        if (HasOffice(cityName))
        {
            Debug.Log($"! 이미 설치된 사무실: {cityName}");
            return;
        }

        installedOffices.Add(cityName);
        Debug.Log($"[✔] 사무실 설치 완료: {cityName}");

        // 업데이트 콜백은 RightSidebar에서 처리
    }

    public List<string> GetAllOffices()
    {
        return new List<string>(installedOffices);
    }
}