using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerAlive = true;

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void PlayerDeath()
    {
        if (isPlayerAlive)
        {
            isPlayerAlive = false;
            InGameUIManager.instance.AppearDeadScreen();
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public void NextLevel()
    {
        AdvancedLoadingScreen.targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene("LoadingScene");
    }
}
