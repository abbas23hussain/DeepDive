using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    
    public PowerUpsData powerUpsData = new PowerUpsData();
    public Money money = new Money();

    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
        SaveData();
    }

    private void BindEvents()
    {
       
    }

    private void UnbindEvents()
    {
        
    }

    private void Start()
    {
        LoadData();
    }
    
    
    #region SaveData
    private void SaveData()
    {
        SetMoney();
        SetPowerUps();
        SaveMoney();
        SavePowerUps();
        PlayerPrefs.Save();
        EventManager.onDataSaved?.Invoke();
    }

    private void SetMoney()
    {
        money.Coins = GameManager.Instance.GetCoins();
    }

    private void SetPowerUps()
    {
        powerUpsData.MoneyLevel = GameManager.Instance.GetPowerUpsManager().MoneyLevel;
        powerUpsData.StaminaLevel = GameManager.Instance.GetPowerUpsManager().StaminaLevel;
        powerUpsData.SpeedLevel = GameManager.Instance.GetPowerUpsManager().SpeedLevel;
    }
    
    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Coins", money.Coins);

    }

    private void SavePowerUps()
    {
        PlayerPrefs.SetInt("MoneyLevel", powerUpsData.MoneyLevel);
        PlayerPrefs.SetInt("StaminaLevel", powerUpsData.StaminaLevel);
        PlayerPrefs.SetInt("SpeedLevel", powerUpsData.SpeedLevel);
    }

    #endregion

    #region LoadData
    private void LoadData()
    {
        LoadMoney();
        LoadPowerUps();
        EventManager.onDataLoaded?.Invoke(this);
    }

    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            money.Coins = PlayerPrefs.GetInt("Coins");
        }
    }

    private void LoadPowerUps()
    {
        if (PlayerPrefs.HasKey("MoneyLevel"))
        {
            powerUpsData.MoneyLevel = PlayerPrefs.GetInt("MoneyLevel");
        }
        if (PlayerPrefs.HasKey("StaminaLevel"))
        {
            powerUpsData.StaminaLevel = PlayerPrefs.GetInt("StaminaLevel");
        }
        if (PlayerPrefs.HasKey("SpeedLevel"))
        {
            powerUpsData.SpeedLevel = PlayerPrefs.GetInt("SpeedLevel");
        }
    }
    

    #endregion
}

#region DataModels

public class PowerUpsData
{
    public int StaminaLevel = 1;

    public int MoneyLevel = 1;

    public int SpeedLevel = 1;
}

public class Money
{
    public int Coins = 0;
}

#endregion
