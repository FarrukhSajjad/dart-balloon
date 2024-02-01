using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaloonDart
{

    public class LevelManager : MonoBehaviour
    {
        public static string CurrentBaloonLevel = "CurrentBaloonLevel";
        public GameObject currentLevel;
        public GameObject[] levels;

        public int currentLevelCount;
        public GameObject baloonPopParticles;

        public ParticleSystem[] confettiParticles;

        public static LevelManager Instance;

        private void Awake()
        {
            if(Instance == null)
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
            Application.targetFrameRate = 300;

            LoadLevel();
        }

        private void LoadLevel()
        {
            if (!PlayerPrefs.HasKey(CurrentBaloonLevel))
            {
                PlayerPrefs.SetInt(CurrentBaloonLevel, 0);
                currentLevelCount = 0;
            }
            else
            {
                currentLevelCount = PlayerPrefs.GetInt(CurrentBaloonLevel);

                if(currentLevelCount >= levels.Length)
                {
                    currentLevelCount = 0;
                    PlayerPrefs.SetInt(CurrentBaloonLevel, 0);
                }
            }


            currentLevel = Instantiate(levels[currentLevelCount]);
        }

        public void OnLevelCompleteEvent()
        {
            Debug.Log("CURRENT LEVEL: BEFORE: " + currentLevelCount);
            currentLevelCount = currentLevelCount + 1;
            Debug.Log("CURRENT LEVEL: AFTER: " + currentLevelCount);
            //currentLevelCount--;

            if (currentLevelCount >= levels.Length)
                currentLevelCount = 0;

            //play confettis
            for (int i = 0; i < confettiParticles.Length; i++)
            {
                confettiParticles[i].Play();
            }

            PlayerPrefs.SetInt(CurrentBaloonLevel, currentLevelCount);

            Invoke(nameof(DelayInLevelCompleted), 1f);
        }

        public void OnLevelFailedEvent()
        {
            Invoke(nameof(DelayInLevelFailed), 1f);
        }

        private void DelayInLevelFailed()
        {
            UIManager.Instance.levelFailedPanel.SetActive(true);
        }

        private void DelayInLevelCompleted()
        {
            UIManager.Instance.levelCompletePanel.SetActive(true);
        }
    }
}