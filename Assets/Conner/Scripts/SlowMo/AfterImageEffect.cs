using UnityEngine;
using System.Collections;

public class AfterImageEffect : MonoBehaviour
{
    [Header("Afterimage Settings")]
    public float spawnRate = 0.1f;   // seconds between ghost frames
    public float fadeTime = 0.3f;    // duration of fade
    public float alpha = 0.5f;       // starting transparency

    private float timer;
    private SpriteRenderer original;

    void Start()
    {
        original = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale >= 1f) return; // only during bullet time

        timer += Time.unscaledDeltaTime;

        if (timer >= spawnRate)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    void SpawnGhost()
    {
        if (original == null) return;

        GameObject ghost = new GameObject("AfterImage");

        ghost.transform.position = transform.position;
        ghost.transform.rotation = transform.rotation;
        ghost.transform.localScale = transform.localScale;

        SpriteRenderer ghostRenderer = ghost.AddComponent<SpriteRenderer>();

        ghostRenderer.sprite = original.sprite;
        ghostRenderer.flipX = original.flipX;
        ghostRenderer.flipY = original.flipY;
        ghostRenderer.sortingLayerID = original.sortingLayerID;
        ghostRenderer.sortingOrder = original.sortingOrder - 1;

        Color c = original.color;
        c.a = alpha;
        ghostRenderer.color = c;

        StartCoroutine(FadeAndDestroy(ghostRenderer));
    }

    IEnumerator FadeAndDestroy(SpriteRenderer sr)
    {
        float t = 0f;
        Color startColor = sr.color;

        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(startColor.a, 0f, t / fadeTime);

            if (sr != null)
                sr.color = new Color(startColor.r, startColor.g, startColor.b, a);

            yield return null;
        }

        if (sr != null)
            Destroy(sr.gameObject);
    }
}