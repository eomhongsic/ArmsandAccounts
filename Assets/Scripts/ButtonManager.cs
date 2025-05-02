using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject buyWeaponPanel;
    public GameObject transportPanel;
    public int playerMoney = 10000;
    public TextMeshProUGUI moneyText;

    public TransportManager transportManager;

    private void Start()
    {
        UpdateMoneyUI();
    }

    public void OnBuyWeaponClicked()
    {
        buyWeaponPanel.SetActive(true);
        Debug.Log("Buy Weapon Panel Opened");
    }

    public void OnStartTransportClicked()
    {
        transportPanel.SetActive(true);
        Debug.Log("Transport Panel Opened");
    }

    public void BuyWeapon(int price)
    {
        if (playerMoney >= price)
        {
            playerMoney -= price;
            UpdateMoneyUI();
            Debug.Log("Weapon purchased for $" + price);
        }
        else
        {
            Debug.Log("Not enough money to buy weapon!");
        }
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = "$" + playerMoney.ToString("N0");
    }

    public void OpenBuyWeaponPanel()
    {
        buyWeaponPanel.SetActive(true);
    }

    public void OpenTransportPanel()
    {
        transportPanel.SetActive(true);
    }

    public void StartTransport()
    {
        transportManager.StartTransport();
    }
    public void OnCompleteTransportClicked()
    {
        transportManager.CompleteTransport();
    }
}