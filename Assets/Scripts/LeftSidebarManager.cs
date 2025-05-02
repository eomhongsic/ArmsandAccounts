using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeftSidebarManager : MonoBehaviour
{
    public static LeftSidebarManager Instance;

    public Transform buttonContainer;
    public GameObject sidebarButtonPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("⚠ LeftSidebarManager 중복 인스턴스 감지됨. 삭제합니다.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowFacilitiesForCity(string cityName)
    {
        Debug.Log($"🏙 도시 클릭됨: {cityName} → 시설 목록 표시 시작");

        if (RightSidebarManager.Instance == null)
        {
            Debug.LogError("❌ RightSidebarManager.Instance가 아직 초기화되지 않음!");
            return;
        }

        ClearButtons();

        AddButton("Office", () => RightSidebarManager.Instance.ShowOffice(cityName));
        AddButton("Warehouse", () => RightSidebarManager.Instance.ShowWarehouse(cityName));
        // 이후 Seller, Buyer 등 추가 예정
    }

    void AddButton(string label, UnityEngine.Events.UnityAction action)
    {
        if (sidebarButtonPrefab == null)
        {
            Debug.LogError("❌ SidebarButtonPrefab이 Inspector에 할당되지 않음!");
            return;
        }

        GameObject go = Instantiate(sidebarButtonPrefab, buttonContainer);
        var btn = go.GetComponent<Button>();
        var txt = go.GetComponentInChildren<TextMeshProUGUI>();

        if (btn == null)
        {
            Debug.LogError("❌ Button 컴포넌트가 프리팹에 없음!");
            return;
        }

        if (txt != null)
        {
            txt.text = label;
        }
        else
        {
            Debug.LogWarning("⚠ TextMeshPro 컴포넌트가 버튼 자식에 없음. 텍스트 설정 건너뜀");
        }

        btn.onClick.AddListener(action);
        Debug.Log($"🟢 버튼 생성 완료: {label}");
    }

    void ClearButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}