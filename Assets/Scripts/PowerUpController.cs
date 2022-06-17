using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private ePowerUp powerUp;
    [SerializeField] private int basePrice;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI buyButtonText;
    [SerializeField] private Button buyButton;

    public int price { private set; get; }

    private void Awake()
    {
        BindEvents();
        AddListeners();
    }

    private void OnDestroy()
    {
        UnbindEvents();
        RemoveListeners();
    }

    private void BindEvents()
    {
        EventManager.onPowerUpsDataLoaded += OnPowerUpsDataLoaded;
        EventManager.onPoweredUp += OnPoweredUp;
    }

    private void UnbindEvents()
    {
        EventManager.onPowerUpsDataLoaded -= OnPowerUpsDataLoaded;
        EventManager.onPoweredUp -= OnPoweredUp;
    }

    private void AddListeners()
    {
        buyButton.onClick.AddListener(BuyPowerUp);
    }

    private void RemoveListeners()
    {
        buyButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        price = basePrice;
        RefreshLevel();
        SyncPrice();
        RefreshPrice();
        RefreshPurchaseButton();
    }

    private void RefreshLevel()
    {
        levelText.text = "Level: " + GameManager.Instance.GetPowerUpsManager().GetPowerUpLevel(powerUp).ToString();
    }

    private void RefreshPrice()
    {
        if (price > 0)
        {
            buyButtonText.text = price.ToString();
        }
        else
        {
            buyButtonText.text = "FREE";
        }
    }

    private void RefreshPurchaseButton()
    {
        
        if (GameManager.Instance.GetCoins() >= price)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    private void BuyPowerUp()
    {
        GameManager.Instance.DecrementCoins(price);
        EventManager.onPoweredUp?.Invoke(powerUp);
        RefreshLevel();
        SyncPrice();
        RefreshPrice();
        RefreshPurchaseButton();
    }

    private void SyncPrice()
    {
        var currentLevel = GameManager.Instance.GetPowerUpsManager().GetPowerUpLevel(powerUp);
        price = currentLevel * basePrice;

    }

    private void OnPowerUpsDataLoaded()
    {
        RefreshLevel();
        SyncPrice();
        RefreshPrice();
        RefreshPurchaseButton();
        
    }

    private void OnPoweredUp(ePowerUp _powerUp)
    {
        if (_powerUp != powerUp)
        {
            RefreshPurchaseButton();
        }
    }
}
