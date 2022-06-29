using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSound : MonoBehaviour
{
    public RectTransform button;
    public bool newPos;
    public int SoundOnOff;
    public RawImage backColor;

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
            PlayerPrefs.SetInt("SoundEnabled", 0);
            backColor.color = new Color(0.5471698f, 0.5471698f, 0.5471698f);
        }
        else
        {
            button.anchoredPosition = new Vector2(40, 0);
            newPos = true;
            PlayerPrefs.SetInt("SoundEnabled", 1);
            backColor.color = Color.green;
        }


    }
}
