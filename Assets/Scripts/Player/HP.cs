using UnityEngine;

public class HP : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < -14)
        {
            GameManager.instance.PlayerDeath();
        }
    }
}
