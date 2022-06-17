using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MilestoneController : MonoBehaviour
{
    [SerializeField] private TextMeshPro distanceText;
    [SerializeField] private GameObject stone;

    public void Init(float distance)
    {
        
        int iDistance = (int) distance;
        distanceText.text = iDistance.ToString() + "f";
    }
}
