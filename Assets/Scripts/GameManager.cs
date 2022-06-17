using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, hud, pauseMenu;
    [SerializeField] private Button retryButton, playButton, pauseButton;
    [SerializeField] private Button cyan, blue, purple, green, yellow, red;
    [SerializeField] private OceanColorsScriptable oceanColorsScriptable;
    [SerializeField] private PowerUpsManager powerUpsManager;
    
    private eUiStateName currentUiStateName = eUiStateName.MainMenuPanel;
    private eUiStateName previousUiStateName = eUiStateName.MainMenuPanel;
    private eGameState gameState = eGameState.MainMenu;
    private eGameState gameStateUponPause = eGameState.MainMenu;
    public eGameState GameState => gameState;

    private int coins;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Init();
        BindEvents();
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
        UnbindEvents();
    }

    private void Init()
    {
        ChangeUIState(eUiStateName.MainMenuPanel);

    }    
    private void ChangeUIState(eUiStateName stateName)
    {
        previousUiStateName = currentUiStateName;
        currentUiStateName = stateName;
        switch (stateName)
        {
            case eUiStateName.GameOverPanel:
                gameOverPanel.SetActive(true);
                hud.SetActive(true);
                mainMenuPanel.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case eUiStateName.MainMenuPanel:
                gameOverPanel.SetActive(false);
                hud.SetActive(true);
                mainMenuPanel.SetActive(true);
                pauseMenu.SetActive(false);
                break;
            case eUiStateName.Hud:
                gameOverPanel.SetActive(false);
                hud.SetActive(true);
                mainMenuPanel.SetActive(false);
                pauseMenu.SetActive(false);
                break;
            case eUiStateName.PauseMenuPanel:
                gameOverPanel.SetActive(false);
                hud.SetActive(true);
                mainMenuPanel.SetActive(false);
                pauseMenu.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stateName), stateName, null);
        }
    }
    
    private void BindEvents()
    {
        EventManager.onGameOver += OnGameOver;
        EventManager.onCoinCollected += OnCoinCollected;
        EventManager.onDataLoaded += OnDataLoaded;
        EventManager.onGameResume += ResumeGame;
    }

    private void UnbindEvents()
    {
        EventManager.onGameOver -= OnGameOver;
        EventManager.onCoinCollected -= OnCoinCollected;
        EventManager.onDataLoaded -= OnDataLoaded;
        EventManager.onGameResume -= ResumeGame;
    }

    private void AddListeners()
    {
        retryButton.onClick.AddListener(OnRetryButtonPressed);
        playButton.onClick.AddListener(OnPlayButtonPressed);
        pauseButton.onClick.AddListener(OnPauseButtonPressed);
        cyan.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Cyan"));
            OnOceanColorSelected(oceanColor);
        });
        blue.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Blue"));
            OnOceanColorSelected(oceanColor);
        });
        purple.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Purple"));
            OnOceanColorSelected(oceanColor);
        });
        green.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Green"));
            OnOceanColorSelected(oceanColor);
        });
        yellow.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Yellow"));
            OnOceanColorSelected(oceanColor);
        });
        red.onClick.AddListener(delegate
        {
            OceanColor oceanColor = oceanColorsScriptable.oceanColors
                .FirstOrDefault(i => i.presetName.Equals("Red"));
            OnOceanColorSelected(oceanColor);
        });
    }

    private void RemoveListeners()
    {
        retryButton.onClick.RemoveAllListeners();
        playButton.onClick.RemoveAllListeners();
        pauseButton.onClick.RemoveAllListeners();
        cyan.onClick.RemoveAllListeners();
        
    }

    private void OnGameOver()
    {
        gameState = eGameState.GameOver;
        ChangeUIState(eUiStateName.GameOverPanel);
    }

    private void OnRetryButtonPressed()
    {
        retryButton.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    private void OnPlayButtonPressed()
    {
        ChangeUIState(eUiStateName.Hud);
        EventManager.initialDive?.Invoke();
        gameState = eGameState.Gameplay;
        
    }

    private void OnPauseButtonPressed()
    {
        EventManager.onGamePause?.Invoke();
        gameStateUponPause = gameState;
        gameState = eGameState.Pause;
        ChangeUIState(eUiStateName.PauseMenuPanel);
    }

    private void ResumeGame()
    {
        gameState = gameStateUponPause;
        ChangeUIState(previousUiStateName);
    }
    private void OnOceanColorSelected(OceanColor oceanColor)
    {
        EventManager.onOceanColorSelected?.Invoke(oceanColor);
    }

    public eUiStateName GetCurrentUiStateName()
    {
        return currentUiStateName;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int _coins)
    {
        coins = _coins;
    }

    public void IncrementCoins(int incrementFactor)
    {
        coins += incrementFactor;
    }

    public int DecrementCoins(int decrementFactor)
    {
        coins -= decrementFactor;
        if (coins < 0)
        {
            coins = 0;
        }
        EventManager.onCoinsUpdated?.Invoke(coins);
        return coins;
    }

    private void OnCoinCollected(GameObject coinGameObject)
    {
        IncrementCoins(1 + powerUpsManager.MoneyLevel);
        EventManager.onCoinsUpdated?.Invoke(coins);
    }
    
    private void OnDataLoaded(DataSaveController dataSaveController)
    {
        coins = dataSaveController.money.Coins;
        //EventManager.onMoneyDataLoaded?.Invoke();
        EventManager.onCoinsUpdated?.Invoke(coins);
    }

    public PowerUpsManager GetPowerUpsManager()
    {
        return powerUpsManager;
    }
    

}

public enum eUiStateName
{
    GameOverPanel,
    MainMenuPanel,
    Hud,
    PauseMenuPanel
}

public enum eGameState
{
    MainMenu,
    Gameplay,
    GameOver,
    Pause
}
