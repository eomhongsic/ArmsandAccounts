using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponListManager : MonoBehaviour
{
    public GameObject weaponItemPrefab;
    public Transform contentParent;

    private void Start()
    {
        CreateWeaponItem("AK-47", 1000);
        CreateWeaponItem("M4A1", 1200);
        CreateWeaponItem("Glock-18", 600);
        CreateWeaponItem("Sniper Rifle", 2500);
        CreateWeaponItem("Shotgun", 900);
    }

    private void CreateWeaponItem(string weaponName, int price)
    {
        GameObject item = Instantiate(weaponItemPrefab, contentParent);
        TMP_Text text = item.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.text = $"{weaponName} - {price}";
        }

        WeaponItem weaponItem = item.GetComponent<WeaponItem>();
        if (weaponItem != null)
        {
            weaponItem.price = price;  // price를 string에서 int로 변환
            weaponItem  .weaponName = weaponName;
        }
    }
}
