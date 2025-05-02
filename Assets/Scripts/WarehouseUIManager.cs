using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarehouseUIManager : MonoBehaviour
{
    public GameObject warehousePanel; // ⭐ 패널을 직접 제어할 수 있도록 추가
    public Transform contentParent;
    public GameObject itemPrefab;

    private WarehouseData currentData;
    private List<WarehouseSlotItemUI> currentSlotItems = new List<WarehouseSlotItemUI>();
    public InventoryManager inventoryManager;

    public void ShowWarehouseContents(WarehouseData data)
    {
        currentData = data; // 현재 데이터 저장

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        currentSlotItems.Clear();

        foreach (WeaponData weapon in data.storedWeapons)
        {
            GameObject go = Instantiate(itemPrefab, contentParent);
            WarehouseSlotItemUI ui = go.GetComponent<WarehouseSlotItemUI>();
            ui.SetData(weapon);
            currentSlotItems.Add(ui);
        }
    }
    public void AddSelectedItemsToInventory()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("❌ InventoryManager가 연결되지 않았습니다.");
            return;
        }

        foreach (var slot in currentSlotItems.Where(s => s.IsSelected()))
        {
            WeaponData copy = new WeaponData(slot.GetData());
            inventoryManager.AddWeapon(copy);
        }

        ShowWarehouseContents(currentData);
    }
}