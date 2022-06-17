using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashController : MonoBehaviour
{
    [SerializeField] private Small_Splash smallSplash;

    private void Start()
    {
        smallSplash.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Splash();
        }
    }

    private void Splash()
    { 
      smallSplash.gameObject.SetActive(true);
      smallSplash.Splash();
    }
}
