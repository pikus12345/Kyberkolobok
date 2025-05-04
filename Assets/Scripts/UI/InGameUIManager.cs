using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Animator deadScreenAnimator;
    [SerializeField] private AudioSource deadScreenAudioSource;
    public static InGameUIManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    


    public void AppearDeadScreen()
    {
        
        deadScreenAnimator.SetTrigger("Appear");
    }
    private void PlayDeadScreenMusic()
    {
        deadScreenAudioSource.Play();
    }
    public void ReloadScene()
    {
        GameManager.instance.ReloadLevel();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
