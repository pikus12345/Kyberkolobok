using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        AdvancedLoadingScreen.targetSceneIndex = 2;
        SceneManager.LoadScene("LoadingScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
