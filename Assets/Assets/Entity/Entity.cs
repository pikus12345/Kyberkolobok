using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] 
    private float maxHealth = 100f;

    private float health;

    [SerializeField] 
    private bool isJumpDamaging;

    private Animator animator;

    [SerializeField]
    private AudioClip[] deathClips;

    [SerializeField][Range(0, 1)] 
    private float sfxVolume;

    private bool isAlive = true;


    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }
    private void CheckHealth()
    {
        if(health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        isAlive = false;
        Debug.Log($"{name} умер!");
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        if (GetComponent<EntityMovement>())
        {
            GetComponent<EntityMovement>().enabled = false;
        }
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{name} получил урон в размере {damage}. Здоровье равно {health}");
        CheckHealth();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.CompareTag("Player") && isAlive)
        {
            if(collision.collider.transform.position.y > transform.position.y && isJumpDamaging)
            {
                TakeDamage(maxHealth);
            }
        }
    }
    private void OnDeath(AnimationEvent animationEvent)
    {
        if (deathClips.Length > 0)
        {
            var index = Random.Range(0, deathClips.Length);
            AudioSource.PlayClipAtPoint(deathClips[index], transform.position, 1f);
        }
        
    }
}
