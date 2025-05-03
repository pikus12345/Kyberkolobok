using StarterAssets;
using UnityEngine;

public class BounceButton : MonoBehaviour
{
    [Header("Настройки подбрасывания")]
    public float bounceForce = 10f; // Сила подбрасывания
    //public ForceMode forceMode = ForceMode.Impulse; // Режим применения силы

    [Header("Эффекты")]
    public ParticleSystem bounceEffect; // Эффект при активации
    public AudioClip bounceSound; // Звук при активации

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("коснулся");
        // Проверяем, что столкнулся игрок
        if (collision.gameObject.CompareTag("Player"))
        {
            ThirdPersonController th = collision.gameObject.GetComponent<ThirdPersonController>();

            if (th != null)
            {
                // Применяем силу вверх
                var tempHeight = th.JumpHeight;
                th.JumpHeight = bounceForce;
                th.JumpAndGravity(true);
                th.JumpHeight = tempHeight;

                // Проигрываем эффекты
                if (bounceEffect != null)
                    bounceEffect.Play();

                if (bounceSound != null)
                    AudioSource.PlayClipAtPoint(bounceSound, transform.position);
            }
        }
    }
}