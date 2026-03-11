using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletTimeCoolDown : MonoBehaviour
{
    public bool Check = false;
    public float timer = 0f;

    public Slider slider;



    public float cooldownTime;

    void Start()
    {
        slider.maxValue = 1f;
    }

    void Update()
    {
        slider.value = timer;

        if (!GameManager.Instance.BulletTimeActive && !Check)
        {
            Check = true;
            StartCoroutine(CooldownLoop());
        }

        if (GameManager.Instance.BulletTimeActive)
        {
            timer = 0f;
            Check = false;
        }
    }

    IEnumerator CooldownLoop()
    {
        timer = 0f;

        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;

            slider.value = timer / cooldownTime;

            yield return null; // wait one frame (this makes it smooth)
        }

        slider.value = 1f;
    }
}