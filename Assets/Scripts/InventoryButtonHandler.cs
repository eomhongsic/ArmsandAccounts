using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonHandler : MonoBehaviour
{
    public InventoryManager inventoryManager; 
    private int itemCounter = 0;

    public void OnAddItemButtonClicked()
    {
        itemCounter++;
        WeaponData newWeapon = new WeaponData("AK-" + itemCounter, Random.Range(1, 5), 1200);
        inventoryManager.AddWeapon(newWeapon);
    }       
}
