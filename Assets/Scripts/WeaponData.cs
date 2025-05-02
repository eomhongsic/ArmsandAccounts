using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string weaponName;
    public int quantity;
    public int unitPrice;

    public WeaponData(string name, int qty, int price)
    {
        weaponName = name;
        quantity = qty;
        unitPrice = price;
    }

    // 복사용 생성자
    public WeaponData(WeaponData other)
    {
        weaponName = other.weaponName;
        quantity = other.quantity;
        unitPrice = other.unitPrice;
    }
}