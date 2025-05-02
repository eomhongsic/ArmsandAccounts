using UnityEngine;
using TMPro;

public class WeaponSlotItemUI : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;

    private WeaponData weaponData;

    public void Setup(WeaponData data)
    {
        weaponData = data;

        if (weaponNameText != null)
        {
            int total = data.quantity * data.unitPrice;
            weaponNameText.text = $"{data.weaponName} x{data.quantity} | ${total}";
        }
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }
}