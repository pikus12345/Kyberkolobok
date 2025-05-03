using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isPlayerAlive = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
