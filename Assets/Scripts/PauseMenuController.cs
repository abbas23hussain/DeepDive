using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }
    
    private void AddListeners()
    {
        resumeButton.onClick.AddListener(OnGameResumed);
    }

    private void RemoveListeners()
    {
        resumeButton.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }


    private void OnGameResumed()
    {
        EventManager.onGameResume?.Invoke();
    }
}
