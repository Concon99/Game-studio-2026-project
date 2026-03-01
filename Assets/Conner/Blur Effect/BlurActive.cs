using UnityEngine;
using System.Collections;

public class BlurActive : MonoBehaviour
{
    [Header("Time Settings")]
    public float slowTimeScale = 0.2f;   // time scale during slow motion
    public float transitionSpeed = 4f;   // how fast time lerps
    public float slowDuration = 1.5f;    // how long slow motion lasts

    private float originalTimeScale;
    private bool isSlowing = false;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlowing)
        {
            StartCoroutine(DoBulletTime());
        }
    }

    IEnumerator DoBulletTime()
    {
        isSlowing = true;

        // Smoothly slow down
        yield return StartCoroutine(SmoothTime(slowTimeScale));

        // Wait for the duration of slow motion (real time, not scaled)
        float timer = 0f;
        while (timer < slowDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Smoothly return to normal time
        yield return StartCoroutine(SmoothTime(originalTimeScale));

        isSlowing = false;
    }

    IEnumerator SmoothTime(float target)
    {
        float start = Time.timeScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * transitionSpeed;
            Time.timeScale = Mathf.Lerp(start, target, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // keep physics stable
            yield return null;
        }
    }
}