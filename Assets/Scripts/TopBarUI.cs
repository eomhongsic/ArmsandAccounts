using TMPro;
using UnityEngine;

public class TopBarUI : MonoBehaviour
{
    public static TopBarUI Instance;

    public TextMeshProUGUI cleanMoneyText;
    public TextMeshProUGUI dirtyMoneyText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoney(int clean, int dirty)
    {
        if (cleanMoneyText != null)
            cleanMoneyText.text = $"Clean: ${clean}";

        if (dirtyMoneyText != null)
            dirtyMoneyText.text = $"Dirty: ${dirty}";
    }
}