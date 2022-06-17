using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform boat;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI oxygenLevelText;
    [SerializeField] private TextMeshProUGUI maxDistanceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Slider distanceSlider;
    [SerializeField] private Slider oxygenSlider;
    
    private int distance = 0;
    private int maxDistance = 0;
    private int coins = 0;

    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        EventManager.onDive += ReFillOxygen;
        EventManager.shoot += ReFillOxygen;
        EventManager.onCoinsUpdated += OnCoinsUpdated;

    }

    private void UnbindEvents()
    {
        EventManager.onDive -= ReFillOxygen;        
        EventManager.shoot -= ReFillOxygen;
        EventManager.onCoinsUpdated -= OnCoinsUpdated;
        
    }
    private void Start()
    {
        distanceSlider.value = 0;
        distanceText.text = "0f";
        oxygenSlider.value = 1f;
        oxygenLevelText.text = "100%";
    }

    private void Update()
    {
        CalculateAndUpdateDistance();
    }
    private void CalculateAndUpdateDistance()
    {
        distance = (int) Vector3.Distance(boat.transform.position, player.transform.position);
        distanceText.text = distance.ToString() + "f";
        UpdateMaxDistance();
        UpdateSlider();
        if (maxDistance > 10)
        {
            EventManager.onDistancesUpdated?.Invoke(distance, maxDistance);
        }
        else
        {
            EventManager.onDistancesUpdated?.Invoke(distance, 10);
        }
    }

    private void UpdateMaxDistance()
    {
        if (distance > maxDistance)
        {
            maxDistance = distance;
            maxDistanceText.text = maxDistance.ToString() + "f";
            EventManager.onMaxDistanceUpdated?.Invoke(maxDistance);
        }
    }

    private void UpdateSlider()
    {
        if (maxDistance > 0)
        {
            float sliderVal = distance / (float) maxDistance;
            //distanceSlider.DOValue(sliderVal, 1f);
            distanceSlider.value = sliderVal;
        }
    }

    private void ReFillOxygen()
    {
        oxygenLevelText.text = "100%";
        oxygenSlider.DOKill();
        oxygenSlider.DOValue(1f, 1f).OnComplete(DepleteOxygen);
        

    }

    private void DepleteOxygen()
    {
        float depletionTime = maxDistance;
        oxygenSlider.DOValue(0f, depletionTime).OnUpdate(delegate
        {
            int oxygenVal = (int) (oxygenSlider.value * 100);
            oxygenLevelText.text = oxygenVal.ToString() + "%";
        });
    }

    private void OnCoinsUpdated(int _coins)
    {
        coins = _coins;
        coinsText.text = coins.ToString();
    }
    
}
