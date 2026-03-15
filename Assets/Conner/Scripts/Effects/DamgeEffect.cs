using UnityEngine;
using System.Collections;
using TMPro;


public class DamgeEffect : MonoBehaviour
{
    public TextMeshPro textObject;
    public TextMeshPro DamageEffectOverlay;

    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    public void ChangeText(string damage)
    {
        textObject.text = damage;
        DamageEffectOverlay.text = damage;
    }



    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
}
