using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Milestone"))
        {
            EventManager.onMilestoneCollided?.Invoke();
        }

        if (other.CompareTag("Coin"))
        {
            EventManager.onCoinMissed?.Invoke();
        }
    }
}
