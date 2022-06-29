using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using ElephantSDK;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    //level k�sm�
    public int _currentLevel;
    public TextMeshProUGUI _currentLevelText;
    public int _levelNumber = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelControl();
    }

    #region level kay�t, restart ve next level
    public void LevelControl()
    {
        _currentLevel = PlayerPrefs.GetInt("currentLevel");
        _levelNumber = PlayerPrefs.GetInt("levelNumber");
        if (SceneManager.GetActiveScene().name != "Level" + _currentLevel)
        {
            SceneManager.LoadScene("Level" + _currentLevel);

        }
        else
        {
            _currentLevelText.text = (_levelNumber + 1).ToString();
        }

    }
    public void NextLevel()
    {
        //AllReset();
        Elephant.LevelFailed(0);
        _currentLevel = 0;
        PlayerPrefs.SetInt("currentLevel", _currentLevel);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("levelNumber", _levelNumber + 1);

        //if (_currentLevel == 0)
        //{

        //    //AllReset();
        //    Elephant.LevelFailed(0);
        //    _currentLevel = 0;
        //    PlayerPrefs.SetInt("currentLevel", _currentLevel);
        //    SceneManager.LoadScene(1);
        //    PlayerPrefs.SetInt("levelNumber", _levelNumber + 1);
        //}
        //else
        //{

        //    LevelRecord();
        //    SceneManager.LoadScene("Level" + (_currentLevel + 1));
        //    _levelNumber++;

        //}
        

    }
    public void Reset()
    {
       // UIManager.instance.LoseGamePanel(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LevelRecord()
    {
        PlayerPrefs.SetInt("currentLevel", _currentLevel + 1);
        PlayerPrefs.SetInt("levelNumber", _levelNumber + 1);
    }

    public void AllReset()
    {
        PlayerPrefs.DeleteKey("currentLevel");
        PlayerPrefs.DeleteKey("levelNumber");
    }
    public void DeletePlayerPrebs()
    {
        PlayerPrefs.DeleteKey("currentLevel");

    }
    #endregion
}
