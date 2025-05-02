using UnityEngine;

public class MessageTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MessageTrigger>())
        {
            MessageTrigger mt = other.GetComponent<MessageTrigger>();
            MessageManager.instance.CreateMessage(mt.text);
            if (mt.text == "NEXTLEVEL")
            {

            }
        }
    }
}