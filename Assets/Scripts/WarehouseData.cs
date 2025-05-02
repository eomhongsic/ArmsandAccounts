using System.Collections.Generic;
using UnityEngine;

public class WarehouseData
{
    public string location; // 도시 이름
    public int capacity;
    public List<WeaponData> storedWeapons = new List<WeaponData>();

    public WarehouseData(string location, int capacity)
    {
        this.location = location;
        this.capacity = capacity;
    }

    public bool CanStore(WeaponData weapon)
    {
        return storedWeapons.Count < capacity;
    }

    public void Store(WeaponData weapon)
    {
        if (CanStore(weapon))
        {
            storedWeapons.Add(weapon);
            Debug.Log($"🟩 창고에 저장됨: {weapon.weaponName}, 총 저장 수: {storedWeapons.Count}");
        }
    }
}