using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniGameTimer : MonoBehaviour
{
    public bool Check = false;
    public float timer = 0f;

    public Slider slider;
    public float cooldownTime = 7f;

    void Start()
    {
        slider.maxValue = 1f;
        slider.value = 1f;
        timer = cooldownTime;
    }

    void Update()
    {
        if (GameManager.Instance.BulletTimeActive && !Check)
        {
            Check = true;
            StartCoroutine(CooldownLoop());
        }

        if (!GameManager.Instance.BulletTimeActive)
        {
            StopAllCoroutines();
            timer = cooldownTime;
            Check = false;
        }
    }

    IEnumerator CooldownLoop()
    {
        timer = cooldownTime;

        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;

            float normalized = timer / cooldownTime;
            slider.value = Mathf.Clamp01(normalized);

            yield return null;
        }

        timer = 0f;
        slider.value = 0f;
    }
}