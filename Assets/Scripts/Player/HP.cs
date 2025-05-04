using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private AudioClip AbyssDeathAudioClip;
    [Range(0f,1f)][SerializeField] private float AbyssDeathAudioVolume;
    private void Update()
    {
        if(transform.position.y < -14)
        {
            if (GameManager.instance.isPlayerAlive)
            {
                GameManager.instance.PlayerDeath();
                AudioSource.PlayClipAtPoint(AbyssDeathAudioClip, transform.position, AbyssDeathAudioVolume);
            }
        }
    }
}
