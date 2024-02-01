using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaloonDart
{


    public class UIManager : MonoBehaviour
    {
        public GameObject levelCompletePanel;
        public GameObject levelFailedPanel;
        public GameObject settingsPanel;

        public GameObject restorePurchasesButton;

        [SerializeField]
        private TextMeshProUGUI levelNumberText;

        public bool settingsOpened = false;

        public static UIManager Instance;

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
            var currentDisplayLevel = PlayerPrefs.GetInt(LevelManager.CurrentBaloonLevel, 0);
            currentDisplayLevel++;
            levelNumberText.text = "Level: " + currentDisplayLevel.ToString();

            if(Application.platform == RuntimePlatform.Android)
            {
                restorePurchasesButton.SetActive(false);
            }
            else
            {
                restorePurchasesButton.SetActive(true);
            }
        }

        public void OnNextButtonPressed()
        {
            FXManager.Instance.PlayClickSound();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnSettingsButtonPressed()
        {

            FXManager.Instance.PlayClickSound();
            if (!settingsOpened)
            {
                settingsPanel.SetActive(true);
            }
            else
            {
                settingsPanel.GetComponent<Animator>().Play("Reverse");
                Invoke(nameof(TurnOffSettingsPanel), 0.5f);
            }

            settingsOpened = !settingsOpened;
        }

        public void OnRemoveAdsButtonPressed()
        {
            FXManager.Instance.PlayClickSound();

            Purchaser.Instance.OnPurcahseItem(Purchaser.RemoveAds);
        }

        public void OnRestorePurchasesButtonPressed()
        {
            FXManager.Instance.PlayClickSound();

            Purchaser.Instance.OnRestoreButtonPressed();
        }

        private void TurnOffSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }
    }
}