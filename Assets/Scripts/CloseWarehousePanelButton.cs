using UnityEngine;

public class CloseWarehousePanelButton : MonoBehaviour
{
    public GameObject warehousePanel;

    public void ClosePanel()
    {
        if (warehousePanel != null)
            warehousePanel.SetActive(false);
    }
}