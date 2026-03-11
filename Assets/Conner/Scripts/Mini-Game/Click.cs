using UnityEngine;
using System.Collections;


public class Click : MonoBehaviour
{
    public int DestroyTime;


    void Start()
    {
        StartCoroutine(DestroyCount());
    }
    void OnMouseDown()
    {
        GameManager.Instance.Points += 1;
        //code to increase points by 1
        Destroy(gameObject);
    }

    IEnumerator DestroyCount()
    {
        yield return new WaitForSecondsRealtime(DestroyTime);
        Destroy(gameObject);
    }
}
