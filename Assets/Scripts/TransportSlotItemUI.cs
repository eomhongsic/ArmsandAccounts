using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransportSlotItemUI : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public WeaponData weaponData;
    public Button completeButton;

    private string destinationCity = "Tirana"; // 나중에 동적으로 넣을 수 있음

    void Start()
    {
        /*
        if (completeButton != null)
        {
            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(OnCompleteTransport);
        }
        */
    }

    public void SetItem(string weaponName, WeaponData data, string city)
    {
        if (weaponNameText != null)
            weaponNameText.text = weaponName;

        weaponData = data;
        destinationCity = city;
    }

    public void OnCompleteTransport()
    {
        Debug.Log($"🧪 저장 시도: {weaponData}, 도시: {destinationCity}");

        WarehouseManager warehouse = WarehouseManager.Instance;

        if (warehouse != null && weaponData != null && !string.IsNullOrEmpty(destinationCity))
        {
            warehouse.StoreWeapon(destinationCity, weaponData);
            Debug.Log("✅ Delivered: " + weaponData.weaponName);
        }
        else
        {
            Debug.LogError("❌ 저장 실패! null 참조 있음");
        }
    }
}