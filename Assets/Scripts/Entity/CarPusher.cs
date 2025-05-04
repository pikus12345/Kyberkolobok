using UnityEngine;

public class CarPusher : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] public float carPushStrengh;
    [SerializeField] private ForceMode forceMode;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        rb.AddForce(-transform.up*carPushStrengh, forceMode);
        var velmag = rb.linearVelocity.magnitude;
        audioSource.pitch = velmag / 50f;
        audioSource.volume = velmag / 50f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.instance.PlayerDeath();
        }
    }
}
