using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{

    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private float defaultTime = 5f;

    public static MessageManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void CreateMessage(string text)
    {
        CreateMessage(text, defaultTime);
    }
    public void CreateMessage(string text, float time)
    {
        GameObject message = Instantiate(messagePrefab, transform);
        message.GetComponent<TMP_Text>().text = text;
        message.GetComponent<AutoDestroyer>().time = time;
    }
}
