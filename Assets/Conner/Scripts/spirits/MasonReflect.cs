using Unity.VisualScripting;
using UnityEngine;

public class MasonReflect : MonoBehaviour
{

    public bool DidReflect;

    void Awake()
    {
        DidReflect = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            DidReflect = true;
        }
    }
}
