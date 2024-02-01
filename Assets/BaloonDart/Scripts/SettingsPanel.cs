using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject vibrationOnButton;
    public GameObject vibrationOffButton;

    private void OnEnable()
    {
        CheckButtonStatus();
    }

    private void CheckButtonStatus()
    {
        var soundState = PlayerPrefs.GetInt(FXManager.Sound);
        if (soundState == 0)
        {
            soundOffButton.SetActive(true);
            soundOnButton.SetActive(false);
        }
        else
        {
            soundOffButton.SetActive(false);
            soundOnButton.SetActive(true);
        }


        var vibrationState = PlayerPrefs.GetInt(FXManager.Vibrations);
        if (vibrationState == 0)
        {
            vibrationOffButton.SetActive(true);
            vibrationOnButton.SetActive(false);
        }
        else
        {
            vibrationOffButton.SetActive(false);
            vibrationOnButton.SetActive(true);
        }
    }

    public void OnToggleSound(bool toggleState)
    {
        FXManager.Instance.PlayClickSound();

        if (toggleState)
        {
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);

            PlayerPrefs.SetInt(FXManager.Sound, 1);

            FXManager.Instance.isSoundEnabled = true;
        }
        else
        {
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);

            PlayerPrefs.SetInt(FXManager.Sound, 0);

            FXManager.Instance.isSoundEnabled = false;

        }
    }


    public void OnToggleVibration(bool toggleState)
    {
        FXManager.Instance.PlayClickSound();

        if (toggleState)
        {
            vibrationOnButton.SetActive(true);
            vibrationOffButton.SetActive(false);

            PlayerPrefs.SetInt(FXManager.Vibrations, 1);

            FXManager.Instance.isVibrationEnabled = true;
        }
        else
        {
            vibrationOnButton.SetActive(false);
            vibrationOffButton.SetActive(true);

            PlayerPrefs.SetInt(FXManager.Vibrations, 0);

            FXManager.Instance.isVibrationEnabled = false;

        }
    }
}
