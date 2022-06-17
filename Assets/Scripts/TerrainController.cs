using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int step;
    private int currentMaxDistance = 0;
    

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
        
    }

    private void UnbindEvents()
    {
    }

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,  - step,
            transform.localPosition.z);
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.GameState == eGameState.Gameplay)
        {
            float yPos = player.transform.position.y - 100f;
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

    private void ChangeTerrainPos()
    {
        float newYPos = -2*currentMaxDistance - step;
        Vector3 newLocalPos =
            new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
        transform.DOLocalMove(newLocalPos, 0.5f);
    }

    private void OnMaxDistanceUpdated(int newMaxDistance)
    {
        currentMaxDistance = newMaxDistance;
    }
}