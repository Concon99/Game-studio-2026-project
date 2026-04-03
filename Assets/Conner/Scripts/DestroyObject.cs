using UnityEngine;
using System.Collections;


public class DestroyObject : MonoBehaviour
{
    public float DestroyTime;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }


    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }
}
