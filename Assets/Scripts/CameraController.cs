using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject boat;
    private Vector3 offset;
    private Vector3 initialPosition;

    private int maxDistance = 0;
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
        EventManager.onGameOver += OnGameOver;
    }

    private void UnbindEvents()
    {
        EventManager.onGameOver -= OnGameOver;
    }

   
    void Start () 
    {
        offset = transform.position - player.transform.position;
        initialPosition = transform.position;
    }
    
    void LateUpdate ()
    {
        if (GameManager.Instance.GameState == eGameState.Gameplay)
        {
            //transform.position = player.transform.position + offset;
            transform.position = new Vector3(transform.position.x, player.transform.position.y ,player.transform.position.z) + offset;
        }
    }
    

    private void OnGameOver()
    {
        //float distance = Math.Abs(boat.transform.position.y - transform.position.y);
        //distance -= 1f;
        //transform.DOLocalMoveY( distance, 10f).SetEase(Ease.OutSine);
        //transform.DOMove(initialPosition, 10f).SetEase(Ease.OutSine);
    }
}
