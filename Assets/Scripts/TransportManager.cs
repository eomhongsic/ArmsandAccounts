using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransportManager : MonoBehaviour
{
    public static TransportManager Instance;

    public List<string> transportQueue = new List<string>();
    public Transform transportListParent;
    public GameObject transportItemPrefab;

    private void Awake()
    {
        Instance = this;
    }
    public void ReceiveWeapons(List<WeaponData> weapons)
    {
        foreach (WeaponData weapon in weapons)
        {
            Debug.Log("Received for transport: " + weapon.weaponName + " x" + weapon.quantity);
        }
    }
    public void StartTransport()
    {
        InventoryItem[] inventoryItems = FindObjectsOfType<InventoryItem>();
        Debug.Log("총 인벤토리 아이템 수: " + inventoryItems.Length);

        foreach (InventoryItem item in inventoryItems)
        {
            if (item.IsSelected())
            {
                GameObject newItem = Instantiate(transportItemPrefab, transportListParent);
                Debug.Log("📦 TransportSlotItemUI 생성됨: " + newItem.name);
                TransportSlotItemUI newItemScript = newItem.GetComponent<TransportSlotItemUI>();

                if (newItemScript != null)
                {
                    newItemScript.SetItem(item.GetWeaponName(), item.GetWeaponData(), "Tirana");
                }

                Destroy(item.gameObject);
            }
            else
            {
                Debug.Log("선택되지 않음: " + item.name);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(transportListParent.GetComponent<RectTransform>());
    }

    public void AddToTransport(string weaponName)
    {
        transportQueue.Add(weaponName);
        Debug.Log("Added to transport: " + weaponName);
    }

    public void ShowTransportQueue()
    {
        Debug.Log("Current Transport Queue:");
        foreach (var weapon in transportQueue)
        {
            Debug.Log("- " + weapon);
        }
    }
    public void CompleteTransport()
    {
        foreach (Transform child in transportListParent)
        {
            TransportSlotItemUI ui = child.GetComponent<TransportSlotItemUI>();
            if (ui != null)
            {
                WarehouseManager.Instance.StoreWeapon("Tirana", ui.weaponData);
                Debug.Log("Delivered: " + ui.weaponData.weaponName);
            }

            Destroy(child.gameObject);
        }

        transportQueue.Clear();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transportListParent.GetComponent<RectTransform>());
    }
}