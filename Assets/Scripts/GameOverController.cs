using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private bool isInitialTrigger = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!isInitialTrigger)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.OnGameOver();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInitialTrigger = false;
    }
}
