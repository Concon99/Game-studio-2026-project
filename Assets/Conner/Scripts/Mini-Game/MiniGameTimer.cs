using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGameTimer : MonoBehaviour
{
    public Slider slider;
    public float cooldownTime = 7f;

    private float timer = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (GameManager.Instance.MiniGameType == "Click")
            cooldownTime = 7f;
        else if (GameManager.Instance.MiniGameType == "Pop")
            cooldownTime = 7f;
        else if (GameManager.Instance.MiniGameType == "Slide")
            cooldownTime = 10;
        else if (GameManager.Instance.MiniGameType == "Mouse")
            cooldownTime = 12;
        else if (GameManager.Instance.MiniGameType == "Luck")
            cooldownTime = 7f;
        else if (GameManager.Instance.MiniGameType == "X")
            cooldownTime = 7f;

        slider.maxValue = cooldownTime;

        if (GameManager.Instance.BulletTimeActive && !isRunning)
        {
            isRunning = true;
            StartCoroutine(CooldownLoop());
        }

        // Stop and reset if bullet time ends
        if (!GameManager.Instance.BulletTimeActive && isRunning)
        {
            StopAllCoroutines();
            timer = cooldownTime;
            slider.value = timer;
            isRunning = false;
        }
    }

    IEnumerator CooldownLoop()
    {
        timer = cooldownTime;

        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            slider.value = Mathf.Clamp(timer, 0f, cooldownTime); // set value directly
            yield return null;
        }

        timer = 0f;
        slider.value = 0f;
        isRunning = false;
    }
}