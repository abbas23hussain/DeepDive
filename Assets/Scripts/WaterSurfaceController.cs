using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurfaceController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.parent.GetComponent<Rigidbody>().velocity.y > 0)
            {
                other.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().isTrigger = false;
            other.isTrigger = false;
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Player"))
    //     {
    //         EventManager.OnGameOver();
    //     }
    // }
}
