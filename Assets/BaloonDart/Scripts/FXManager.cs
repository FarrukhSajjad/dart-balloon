using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static string Sound = "Sound";
    public static string Vibrations = "Vibrations";

    public bool isSoundEnabled = false;
    public bool isVibrationEnabled = false;
    [SerializeField]
    private AudioSource balloonPopSound;
    [SerializeField]
    private AudioSource victorySound;
    [SerializeField]
    private AudioSource levelFailedSound;
    [SerializeField]
    private AudioSource clickSound;

    public static FXManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        InitFX();

        Vibration.Init();
    }

    public void InitFX()
    {
        if (!PlayerPrefs.HasKey(Sound))
        {
            PlayerPrefs.SetInt(Sound, 1);
        }


        if (!PlayerPrefs.HasKey(Vibrations))
        {
            PlayerPrefs.SetInt(Vibrations, 1);
        }

        if (PlayerPrefs.GetInt(Vibrations, 1) == 1)
        {
            isVibrationEnabled = true;
        }
        else
        {
            isVibrationEnabled = false;
        }


        if (PlayerPrefs.GetInt(Sound, 1) == 1)
        {
            isSoundEnabled = true;
        }
        else
        {
            isSoundEnabled = false;
        }
    }

    public void PlayBalloonPopSound()
    {
        if (isSoundEnabled)
            if (!balloonPopSound.isPlaying)
                balloonPopSound.Play();
    }

    public void OnVibrateEvent()
    {
        if (isVibrationEnabled)
            Vibration.VibratePop();
    }

    public void PlayVictorySound()
    {
        if(isSoundEnabled)
            victorySound.Play();
    }

    public void PlayLevelFailedSound()
    {
        if(isSoundEnabled)
            levelFailedSound.Play();
    }

    public void PlayClickSound()
    {
        if(isSoundEnabled)
            clickSound.Play();
    }

}
