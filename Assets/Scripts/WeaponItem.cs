using UnityEngine;
using UnityEngine.UI;


public class WeaponItem : MonoBehaviour
{
    public int price; 
    public string weaponName;

    private Button button;
    private ButtonManager buttonManager;
    private InventoryManager inventoryManager;
    private void Start()
    {   
        button = GetComponent<Button>();
        buttonManager = FindObjectOfType<ButtonManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        button.onClick.AddListener(BuyThisWeapon);
    }

    private void BuyThisWeapon()
    {
        if (inventoryManager != null)
        {
            WeaponData data = new WeaponData(weaponName, 1, price);
                
            // Tirana 창고에 저장 (예시 도시)      
            WarehouseManager wm = FindObjectOfType<WarehouseManager>();
            wm.StoreWeapon("Tirana", data);

            WarehouseUIManager ui = FindObjectOfType<WarehouseUIManager>();
            ui.ShowWarehouseContents(wm.GetWarehouseByCity("Tirana"));
        }
    }
}