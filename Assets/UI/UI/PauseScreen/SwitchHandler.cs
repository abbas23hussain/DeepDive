using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SwitchHandler : MonoBehaviour
{
    private int switchState = 1;
    public GameObject switchbutton;
    public RectTransform button;
    public bool newPos;
    public RawImage backColor;

    //public void OnSwitchButtonClicked()
    //{
    //    switchbutton.transform.DOLocalMoveX(-switchbutton.transform.localPosition.x, 0.2f);
    //    switchState = Math.Sign(-switchbutton.transform.localPosition.x);
    //    Debug.Log("btn state - " + switchState);
    //}

    private void Start()
    {
        newPos = true;
    }

    public void StartSwitchButtonClicked()
    {
        if (newPos)
        {
            button.anchoredPosition = new Vector2(-40, 0);
            newPos = false;
            PlayerPrefs.SetInt("VibrationEnabled", 0);
            backColor.color = new Color(0.5471698f, 0.5471698f, 0.5471698f);
            //0.5471698
        }
        else
        {
            button.anchoredPosition = new Vector2(40, 0);
            newPos = true;
            PlayerPrefs.SetInt("VibrationEnabled", 1);
            backColor.color = Color.green;
        }

        
    }
}

