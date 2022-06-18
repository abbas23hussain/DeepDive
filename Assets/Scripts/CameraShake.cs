using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public int strength;
    public int vibrato;

    private Tweener shakeTweener;
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
        EventManager.SetShakeSpeed += SetShakeSpeed;
        EventManager.onGameOver += OnGameOver;
    }

    private void UnbindEvents()
    {
        EventManager.SetShakeSpeed -= SetShakeSpeed;
        EventManager.onGameOver -= OnGameOver;
    }

    private void Start()
    {
        StartShaking();
    }

    private void StartShaking()
    { 
        shakeTweener = transform.DOShakeRotation(2f, strength: strength, vibrato: vibrato, randomness: 90f)
            .SetEase(Ease.OutQuad).SetLoops(-1);
        shakeTweener.timeScale = 0f;
    }

    private void SetShakeSpeed(float shakeSpeed)
    {
        if (shakeTweener != null)
        {
            if (GameManager.Instance.GameState == eGameState.Gameplay)
            {
                shakeTweener.timeScale = shakeSpeed;
            }
        }
    }

    private void OnGameOver()
    {
        StopShaking();
    }
    private void StopShaking()
    {
        shakeTweener?.Kill();
    }
    
    
}
