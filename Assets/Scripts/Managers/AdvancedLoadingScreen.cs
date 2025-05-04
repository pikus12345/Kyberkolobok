using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class AdvancedLoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text tipText;
    [SerializeField] public static int targetSceneIndex;
    [SerializeField] private string[] tips = {};

    void Start()
    {
        ShowRandomTip();
        StartCoroutine(LoadSceneAsync());
    }

    void ShowRandomTip()
    {
        if (tips.Length > 0)
        {
            tipText.text = tips[Random.Range(0, tips.Length)];
        }
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;

            if (progress >= 1f)
            {
                yield return new WaitForSeconds(3f);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}