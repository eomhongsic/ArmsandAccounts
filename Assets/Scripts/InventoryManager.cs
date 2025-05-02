using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;
    public TMP_Text selectedTotalText;

    public void AddWeapon(WeaponData weapon)
    {
        GameObject itemObj = Instantiate(inventoryItemPrefab, inventoryContent);
        InventoryItem item = itemObj.GetComponent<InventoryItem>();

        if (item != null)
        {
            item.Setup(weapon, this);
        }

        // 💰 수익 추가
        int income = weapon.unitPrice * weapon.quantity;
        FinanceManager.Instance.AddDirtyMoney(income);

        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryContent.GetComponent<RectTransform>());
    }

    public void RemoveWeapon(WeaponData data)
    {
        foreach (Transform child in inventoryContent)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if (item != null && item.GetData().weaponName == data.weaponName)
            {
                Destroy(child.gameObject);
                break;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryContent.GetComponent<RectTransform>());
    }
    public List<WeaponData> GetSelectedWeapons()
    {
        List<WeaponData> selected = new List<WeaponData>();

        foreach (Transform child in inventoryContent)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if (item != null && item.IsSelected())
            {
                selected.Add(item.GetData());
            }
        }

        return selected;
    }
    public void RemoveSelectedItems()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (Transform child in inventoryContent)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if (item != null && item.IsSelected())
            {
                Debug.Log("IsSelected() pass");
                toRemove.Add(child.gameObject);
            }
        }

        foreach (GameObject obj in toRemove)
        {
            Destroy(obj);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryContent.GetComponent<RectTransform>());
    }

    public void UpdateSelectedTotal()
    {
        int total = 0;

        foreach (Transform child in inventoryContent)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if (item != null && item.IsSelected())
            {
                total += item.GetTotalPrice(); // ✅ 안전한 방식
            }
        }

        selectedTotalText.text = "Selected Total: $" + total.ToString();
    }
    public void OnTakeSelected()
    {
        Debug.Log("TakeSelected 버튼이 눌렸습니다");

        List<WeaponData> selectedWeapons = GetSelectedWeapons();

        if (selectedWeapons.Count == 0)
        {
            Debug.Log("선택된 무기가 없습니다.");
            return;
        }

        TransportManager.Instance.ReceiveWeapons(selectedWeapons);
        RemoveSelectedItems();
        UpdateSelectedTotal();
    }
}