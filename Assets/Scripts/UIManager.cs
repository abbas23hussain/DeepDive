using UnityEngine;
using ElephantSDK;
public class UIManager : MonoBehaviour
{
    

    public void StartButton()
    {
        Elephant.LevelStarted(0);
    }

    public void RestartButton()
    {
        Elephant.LevelFailed(0);
    }


}
