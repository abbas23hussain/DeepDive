using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager instance;

    public int StaminaLevel { get; private set; } = 1;

    public int MoneyLevel { get; private set; } = 1;

    public int SpeedLevel { get; private set; } = 1;


    private void Awake()
    {
        instance = this;
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        EventManager.onPoweredUp += OnPoweredUp;
        EventManager.onDataLoaded += OnDataLoaded;
    }

    private void UnbindEvents()
    {
        EventManager.onPoweredUp -= OnPoweredUp;
        EventManager.onDataLoaded -= OnDataLoaded;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        
    }

    private void OnPoweredUp(ePowerUp powerUp)
    {
        switch (powerUp)
        {
            case ePowerUp.stamina:
                StaminaLevel++;
                break;
            case ePowerUp.money:
                MoneyLevel++;
                break;
            case ePowerUp.speed:
                SpeedLevel++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
        }
    }

    public int GetPowerUpLevel(ePowerUp powerUp)
    {
        int level;
        switch (powerUp)
        {
            case ePowerUp.stamina:
                level = StaminaLevel;
                break;
            case ePowerUp.money:
                level = MoneyLevel;
                break;
            case ePowerUp.speed:
                level = SpeedLevel;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
        }

        return level;
    }

    private void OnDataLoaded(DataSaveController dataSaveController)
    {
        StaminaLevel = dataSaveController.powerUpsData.StaminaLevel;
        MoneyLevel = dataSaveController.powerUpsData.MoneyLevel;
        SpeedLevel = dataSaveController.powerUpsData.SpeedLevel;
        EventManager.onPowerUpsDataLoaded?.Invoke();
    }
}

public enum ePowerUp
{
    stamina,
    money,
    speed
}