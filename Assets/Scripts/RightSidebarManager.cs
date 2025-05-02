using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RightSidebarManager : MonoBehaviour
{
    public static RightSidebarManager Instance;

    public GameObject officePanel;
    public TextMeshProUGUI officeStatusText;
    public Button installOfficeButton;

    private string currentCityName;

    public GameObject warehousePanel;
    public WarehouseUIManager warehouseUIManager;
    public GameObject warehouseModalPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowOffice(string cityName)
    {
        Debug.Log($"[RightSidebar] ShowOffice CALLED for {cityName}");

        officePanel.gameObject.SetActive(true);

        bool isInstalled = OfficeManager.Instance.HasOffice(cityName);
        if (isInstalled)
        {
            officeStatusText.text = $"✅ Office installed in {cityName}";
            installOfficeButton.gameObject.SetActive(false);
        }
        else
        {
            officeStatusText.text = $"☐ Office not installed in {cityName}";
            installOfficeButton.gameObject.SetActive(true);

            // 버튼 클릭 이벤트 연결
            installOfficeButton.onClick.RemoveAllListeners(); // 중복 방지
            installOfficeButton.onClick.AddListener(() =>
            {
                OfficeManager.Instance.InstallOffice(cityName);
                ShowOffice(cityName); // 다시 UI 갱신
            });
        }
    }
    public void UpdateOfficeUI(string cityName, bool isInstalled)
    {
        if (officeStatusText != null)
            officeStatusText.text = isInstalled ?
                $"✅ Office installed in {cityName}" :
                $"❌ No office in {cityName}";

        if (installOfficeButton != null)
        {
            installOfficeButton.gameObject.SetActive(!isInstalled);
            installOfficeButton.onClick.RemoveAllListeners();
            installOfficeButton.onClick.AddListener(() =>
            {
                OfficeManager.Instance.InstallOffice(cityName);
                UpdateOfficeUI(cityName, true);
            });
        }
    }

    public void ShowWarehouse(string cityName)
    {
        if (warehouseModalPanel != null)
            warehouseModalPanel.SetActive(true);

        var data = WarehouseManager.Instance.GetWarehouseByCity(cityName);
        warehouseUIManager.ShowWarehouseContents(data);
    }

    public void CloseWarehouse()
    {
        if (warehouseModalPanel != null)
            warehouseModalPanel.SetActive(false);
    }
}