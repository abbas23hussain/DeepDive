using UnityEngine;
using ElephantSDK;
public class UIManager : MonoBehaviour
{
    

    public void StartButton()
    {
        Elephant.LevelStarted(LevelManager.instance._levelNumber + 1);
        Debug.Log("Level start" + LevelManager.instance._levelNumber + 1);
        //LevelManager.instance.NextLevel();
    }

    public void RestartButton()
    {
        
        LevelManager.instance.NextLevel();
    }


}
