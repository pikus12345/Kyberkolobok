using UnityEngine;

public class CarPusher : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float carPushStrengh;
    [SerializeField] private ForceMode forceMode;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rb.AddForce(-transform.up*carPushStrengh, forceMode);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.instance.PlayerDeath();
        }
    }
}
