using UnityEngine;
using System.Collections;

public class TrailFade : MonoBehaviour
{
    public float fadeTime = 0.5f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        Color startColor = sr.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            sr.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, t / fadeTime));
            yield return null;
        }

        Destroy(gameObject);
    }
}