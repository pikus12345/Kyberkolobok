using System.Collections;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float time;
    private void Start()
    {
        StartCoroutine("Timer");
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
