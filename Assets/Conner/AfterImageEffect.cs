using UnityEngine;
using System.Collections;

public class AfterImageEffect : MonoBehaviour
{
    [Header("Afterimage Settings")]
    public float spawnRate = 0.1f;   // seconds between ghost frames
    public float fadeTime = 0.3f;    // duration of fade
    public float alpha = 0.5f;       // starting transparency
    public bool followAnimations = true;

    private int ColorAmount;

    private float timer;

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
        ColorAmount += 1;
        if (ColorAmount > 7)
        {
            ColorAmount = 1;
        }
        
        
        // Clone the object independently
        GameObject ghost = Instantiate(gameObject, transform.position, transform.rotation);
        ghost.transform.parent = null; // DO NOT make it a child
        
        if (ColorAmount == 1)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (ColorAmount == 2)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.orange;
        }
        if (ColorAmount == 3)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (ColorAmount == 4)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (ColorAmount == 5)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (ColorAmount == 6)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.purple;
        }
        if (ColorAmount == 7)
        {
            ghost.GetComponent<SpriteRenderer>().color = Color.pink;
        }

        // Remove all scripts
        MonoBehaviour[] scripts = ghost.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            Destroy(script);
        }

        // Remove Rigidbody2D if exists
        Rigidbody2D rb = ghost.GetComponent<Rigidbody2D>();
        if (rb != null) Destroy(rb);

        // Freeze animation frames
        if (followAnimations)
        {
            Animator[] animators = ghost.GetComponentsInChildren<Animator>();
            foreach (Animator animator in animators)
            {
                animator.enabled = false;
            }
        }

        // Fade out and destroy
        StartCoroutine(FadeAndDestroy(ghost, fadeTime, alpha));
    }

    IEnumerator FadeAndDestroy(GameObject ghost, float duration, float startAlpha)
    {
        SpriteRenderer[] renderers = ghost.GetComponentsInChildren<SpriteRenderer>();
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            float a = Mathf.Lerp(startAlpha, 0f, t);

            foreach (SpriteRenderer sr in renderers)
            {
                if (sr != null)
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            }

            yield return null;
        }

        Destroy(ghost);
    }
}