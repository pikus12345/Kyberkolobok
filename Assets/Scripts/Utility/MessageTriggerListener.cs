using StarterAssets;
using UnityEngine;

public class MessageTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MessageTrigger>())
        {
            MessageTrigger mt = other.GetComponent<MessageTrigger>();
            if (mt.text == "NEXTLEVEL")
            {
                GameManager.instance.NextLevel();
                return;
            }
            if (mt.text == "BOUNCE")
            {
                GetComponent<ThirdPersonController>().JumpAndGravity(true);
                return;
            }
            MessageManager.instance.CreateMessage(mt.text);
            if (mt.DestroyAfterUse)
            {
                Destroy(mt.gameObject);
            }
        }
    }
}