using UnityEngine;

public class MessageTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MessageTrigger>())
        {
            MessageManager.instance.CreateMessage(other.GetComponent<MessageTrigger>().text);
        }
    }
}