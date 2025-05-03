using UnityEngine;

public class PlayerCollisionsTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
    }
}
