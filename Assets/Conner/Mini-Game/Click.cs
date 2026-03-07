using UnityEngine;
using System.Collections;


public class Click : MonoBehaviour
{
    public int DestroyTime;


    void Awake()
    {
        StartCoroutine(DestroyCount());
    }
    void OnMouseDown()
    {
        print("clicked!");
        GameManager.Instance.Points += 1;
        //code to increase points by 1
        Destroy(gameObject);
    }

    IEnumerator DestroyCount()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }
}
