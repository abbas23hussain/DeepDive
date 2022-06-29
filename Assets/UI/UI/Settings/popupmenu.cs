using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class popupmenu : MonoBehaviour
{

    public GameObject PopUpMenu;
    public GameObject GameMenuOnButton;
    public GameObject GameMenuOffButton;
    public GameObject SoundOnButton;
    public GameObject SoundOffButton;
    public GameObject VibrationOnButton;
    public GameObject VibrationOffButton;
    
    public int SoundOnOff;
    public int VibrationOnOff;

    [Header("New Pause Menu")]
    public GameObject PauseMenu;

    void Start()
    {
        SoundOnOff = PlayerPrefs.GetInt("SoundEnabled");
        VibrationOnOff = PlayerPrefs.GetInt("VibrationEnabled");
        if (SoundOnOff == 1)
        {
            SoundOnButton.SetActive(false);
            SoundOffButton.SetActive(true);
        }
        else
        {
            SoundOnButton.SetActive(true);
            SoundOffButton.SetActive(false);
        }
        if (VibrationOnOff == 1)
        {
            VibrationOnButton.SetActive(false);
            VibrationOffButton.SetActive(true);
        }
        else
        {
            VibrationOnButton.SetActive(true);
            VibrationOffButton.SetActive(false);
        }
    }
    public void PopMenuOn()
    {
        PauseMenu.SetActive(true);
        PopUpMenu.SetActive(true);
        GameMenuOnButton.SetActive(false);
        GameMenuOffButton.SetActive(true);
        //PopUpMenu.transform.DORotate(new Vector3(0, 0, 0), 1);
        Time.timeScale = 0;
        
    }
    public void PopMenuOff()
    {
        PauseMenu.SetActive(false);
        //PopUpMenu.transform.DORotate(new Vector3(-90, 0, 0), 0.5f);
        GameMenuOnButton.SetActive(true);
        GameMenuOffButton.SetActive(false);
        Invoke(nameof(turnmenuoff), 0.5f);
        Time.timeScale = 1;

    }
    public void turnmenuoff()
    {
        PopUpMenu.SetActive(false);
    }
    public void SoundOn()
    {
        SoundOnButton.SetActive(false);
        SoundOffButton.SetActive(true);
        PlayerPrefs.SetInt("SoundEnabled", 1);
        

    }
    public void SoundOff()
    {
        SoundOnButton.SetActive(true);
        SoundOffButton.SetActive(false);
        //SoundOnOff = true;
        PlayerPrefs.SetInt("SoundEnabled", 0);

    }
    public void VibrationOn()
    {
        VibrationOnButton.SetActive(false);
        VibrationOffButton.SetActive(true);
        //VibrationOnOff = false;
        PlayerPrefs.SetInt("VibrationEnabled", 1);

    }
    public void VibrationOff()
    {
        VibrationOnButton.SetActive(true);
        VibrationOffButton.SetActive(false);
        //VibrationOnOff = true;
        PlayerPrefs.SetInt("VibrationEnabled", 0);
    }
}
