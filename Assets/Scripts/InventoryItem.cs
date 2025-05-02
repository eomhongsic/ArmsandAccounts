using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;

    private WeaponData weaponData;
    private InventoryManager manager;

    private bool isSelected = false;
    private Image backgroundImage;
    private Color normalColor = Color.white;
    private Color selectedColor = new Color(0.8f, 0.95f, 1f, 1f);

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    // ✅ InventoryManager도 함께 넘기도록 수정
    public void Setup(WeaponData data, InventoryManager mgr)
    {
        weaponData = data;
        manager = mgr;

        if (nameText != null)
            nameText.text = data.weaponName;

        if (infoText != null)
        {
            int quantity = data.quantity;
            int total = data.unitPrice * quantity;
            infoText.text = $"{quantity} units | ${data.unitPrice} each | ${total} total";
        }

        UpdateVisual();
    }

    public void ToggleSelected()
    {
        isSelected = !isSelected;
        UpdateVisual();
        manager?.UpdateSelectedTotal(); // ✅ 선택 상태 바뀔 때 자동 반영
    }

    // ✅ 외부에서 직접 설정할 수 있게
    public void SetSelected(bool value)
    {
        isSelected = value;
        UpdateVisual();
    }

    public bool IsSelected() => isSelected;

    public WeaponData GetData() => weaponData;
    public string GetWeaponName() => weaponData.weaponName;
    public WeaponData GetWeaponData() => weaponData;

    // ✅ 숫자형 총가격 반환
    public int GetTotalPrice()
    {
        return weaponData.quantity * weaponData.unitPrice;
    }

    private void UpdateVisual()
    {
        if (backgroundImage != null)
            backgroundImage.color = isSelected ? selectedColor : normalColor;
    }
}