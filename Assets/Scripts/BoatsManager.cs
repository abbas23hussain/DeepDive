using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsManager : MonoBehaviour
{
    public List<Boat> boats;
    
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
        EventManager.onBoatSelected += OnBoatSelected;
    }

    private void UnbindEvents()
    {
        EventManager.onBoatSelected -= OnBoatSelected;
    }

    private void OnBoatSelected(Boat boatsData)
    {
        ToggleBoat(boatsData.boatObject);
    }

    private void ToggleBoat(GameObject boat)
    {
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].boatObject.SetActive(false);
        }
        boat.gameObject.SetActive(true);
    }
    
    
    
}

[Serializable]
public class Boat
{
    public string boatName;
    public GameObject boatObject;
}
