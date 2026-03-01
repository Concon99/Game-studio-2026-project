using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlowMoTimer : MonoBehaviour
{
    public Slider slider; // Assign in Inspector
    public float duration = 5f;

    private bool isCounting = false;

    void Start()
    {
        slider.gameObject.SetActive(true);
        slider.value = 1f;
    }

    void Update()
    {
        // Start the slider countdown only when time is slowed
        if (Time.timeScale < 1f && !isCounting)
        {
            StartCoroutine(FillDownSlider());
        }
    }

    IEnumerator FillDownSlider()
    {
        isCounting = true;

        // Activate slider and reset value
        slider.gameObject.SetActive(true);
        slider.value = 1f;

        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // Counts real time, unaffected by timeScale
            slider.value = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }

        // Countdown finished: hide and reset
        slider.value = 1f;
        slider.gameObject.SetActive(false);

        isCounting = false;
    }
}