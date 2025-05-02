using System.Collections.Generic;
using UnityEngine;

public class WarehouseManager : MonoBehaviour
{

    public static WarehouseManager Instance;

    public List<WarehouseData> warehouses = new List<WarehouseData>();
    private List<WeaponData> storedWeapons = new List<WeaponData>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 제거
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        warehouses.Add(new WarehouseData("Tirana", 10));
        Debug.Log("Tirana 창고 등록됨");

        // ✅ 테스트용 무기 추가 (임시)
        WeaponData testWeapon = new WeaponData("AK-47", 3, 1200);

        StoreWeapon("Tirana", testWeapon);
    }

    public WarehouseData GetWarehouseByCity(string cityName)
    {
        foreach (var w in warehouses)
        {
            Debug.Log($"[검색] 창고: {w.location}");
        }
        return warehouses.Find(w => w.location == cityName);
    }

    public void StoreWeapon(string cityName, WeaponData weapon)
    {
        WarehouseData target = GetWarehouseByCity(cityName);


        if (target == null)
        {
            Debug.LogError($"❌ 창고 '{cityName}'를 찾을 수 없습니다. 현재 등록된 창고 수: {warehouses.Count}");
            return;
        }
        if (!target.CanStore(weapon))
        {
            Debug.LogWarning($"창고는 찾았지만 용량 부족: {cityName}");
            return;
        }

        target.Store(weapon);
        Debug.Log($"✅ Stored {weapon.weaponName} in {cityName}");       
    }
}   