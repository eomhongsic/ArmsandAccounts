using UnityEngine;
using UnityEngine.UI;

public class CityClickHandler : MonoBehaviour
{
    public string cityName = "Tirana";

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log($"도시 클릭됨: {cityName}");   
        LeftSidebarManager.Instance.ShowFacilitiesForCity(cityName);    
    }
}