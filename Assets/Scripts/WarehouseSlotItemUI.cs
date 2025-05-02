using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarehouseSlotItemUI : MonoBehaviour
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI infoText;
    public Button actionButton;

    private bool isSelected = false;
    private Color normalColor = Color.white;
    private Color selectedColor = new Color(0.6f, 0.8f, 1f, 1f); // 연파란

    private Image backgroundImage;
    public WeaponData storedWeaponData; // ⭐ 인벤토리에 넘기기 위한 데이터

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        if (actionButton != null)
        {
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(OnItemClicked); // 버튼에 토글 연결
        }
    }

    public void SetData(WeaponData data)
    {
        storedWeaponData = data;

        if (weaponNameText != null)
            weaponNameText.text = data.weaponName;

        if (infoText != null)
            infoText.text = $"{data.quantity} units | ${data.unitPrice * data.quantity}";
    }

    public void OnItemClicked()
    {
        isSelected = !isSelected;
        UpdateVisual();
    }

    public bool IsSelected() => isSelected;
    public WeaponData GetData() => storedWeaponData;

    private void UpdateVisual()
    {
        if (backgroundImage != null)
            backgroundImage.color = isSelected ? selectedColor : normalColor;
    }
}