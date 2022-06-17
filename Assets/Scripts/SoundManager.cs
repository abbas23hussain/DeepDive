using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource heartbeatSound;
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
        EventManager.SetHeartbeatVolume += SetHeartbeatSoundLevel;
        EventManager.onGameOver += OnGameOver;
    }

    private void UnbindEvents()
    {
        EventManager.SetHeartbeatVolume -= SetHeartbeatSoundLevel;
        EventManager.onGameOver -= OnGameOver;
    }

    private void Start()
    {
        heartbeatSound.volume = 0f;
    }

    private void SetHeartbeatSoundLevel(float level)
    {
        heartbeatSound.volume = level;
    }

    private void OnGameOver()
    {
        heartbeatSound.volume = 0;
    }
    
    
}
