using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceneManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject privacyPanel;
    public Image loadingBarFillImage;

    public static string PrivacyPolicy = "PrivacyPolicy";

    [SerializeField]
    private TextMeshProUGUI loadingDisplayText;
    [SerializeField]
    private string[] loadingTexts;

    private void Start()
    {
        CheckForPrivacyPolicy();
    }

    private void CheckForPrivacyPolicy()
    {
        if (!PlayerPrefs.HasKey(PrivacyPolicy))
        {
            privacyPanel.SetActive(true);
            loadingPanel.SetActive(false);
        }
        else
        {
            privacyPanel.SetActive(false);
            loadingPanel.SetActive(true);

            StartCoroutine(LoadMainMenuScene());
        }
    }

    public void OnPrivacyButtonPressed()
    {
        Application.OpenURL("https://sites.google.com/view/angrypanda/privacy-policy");
    }

    public void OnAcceptPolicyButtonPressed()
    {
        PlayerPrefs.SetInt(PrivacyPolicy, 1);
        privacyPanel.SetActive(false);
        loadingPanel.SetActive(true);

        StartCoroutine(LoadMainMenuScene());
    }

    private IEnumerator LoadMainMenuScene()
    {
        loadingBarFillImage.DOFillAmount(1f, 4f);
        StartCoroutine(UpdateLoadingTexts());
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }

    private IEnumerator UpdateLoadingTexts()
    {
        loadingDisplayText.text = loadingTexts[0];
        for (int i = 0; i < loadingTexts.Length; i++)
        {
            loadingDisplayText.text = loadingTexts[i];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
