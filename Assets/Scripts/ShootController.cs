using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private bool initialDive = true;
    private bool isShootable = false;

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
        EventManager.onDive += OnInitialDive;
    }

    private void UnbindEvents()
    {
        EventManager.onDive -= OnInitialDive;        
        
    }

    private void OnInitialDive()
    {
        initialDive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.GameState == eGameState.Gameplay)
            {
                isShootable = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isShootable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isShootable && !initialDive)
            {
                if (GameManager.Instance.GameState == eGameState.Gameplay)
                {
                    EventManager.Shoot();
                    isShootable = false;
                }
            }
        }
    }
}
