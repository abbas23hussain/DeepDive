using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public popupmenu Sound;
    public int SoundOnOff = 1;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        instance = this;
        SoundOnOff = PlayerPrefs.GetInt("SoundEnabled");
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    private void Update()
    {
        SoundOnOff = PlayerPrefs.GetInt("SoundEnabled");
        if (SoundOnOff == 0)
        {
            Debug.Log("ses açýk");
        }
        else
        {
            Debug.Log("ses kapalý");
        }
        

        
    }
    public void PlaySFX(int index)
    {
        _audioSource.clip = clips[index];
        var randomPitch = Random.Range(1.60f, 1.85f);
        _audioSource.pitch = randomPitch;
        if (SoundOnOff == 0) 
        {
            _audioSource.Play(); 
        }
        
    }

    public void PlaySound(int index)
    {
        _audioSource.clip = clips[index];
        if (SoundOnOff == 0) 
        {

            _audioSource.Play();
        }
        
    }

    public void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

}
