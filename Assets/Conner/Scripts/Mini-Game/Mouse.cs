using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    [Header("UI")]
    public Slider slider;

    [Header("QTE Settings")]
    public float requiredProgress = 100f;
    public float sensitivity = 10f;
    public float decayRate = 25f;

    private bool isActive = false;

    void Start()
    {
        StartQTE();
        slider.minValue = 0f;
        slider.maxValue = requiredProgress;
        slider.value = 0f;

        isActive = true; 
    }

    void Update()
    {
        if (!isActive) return;

        float movement = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        ).magnitude;

        GameManager.Instance.Points += movement * sensitivity;

        // Decrease over time (forces fast movement)
        GameManager.Instance.Points -= decayRate * Time.deltaTime;

        // Clamp progress
        GameManager.Instance.Points = Mathf.Clamp(GameManager.Instance.Points, 0f, requiredProgress);

        // Update UI
        slider.value = GameManager.Instance.Points;

        // Check success
        if (GameManager.Instance.Points >= requiredProgress)
        {
            print("Won Mini game!");
            GameManager.Instance.Suceed = true;
            GameManager.Instance.MiniGameDamage = 40;
            
            
            GameObject obj = GameObject.FindWithTag("Blur");

            BlurActive _BlurActive = obj.GetComponent<BlurActive>();
            
            _BlurActive.SlowMoOver = true;
            GameManager.Instance.Suceed = true;
            
            Destroy(gameObject);
        }
    }
    

    public void StartQTE()
    {
        GameManager.Instance.Points = 0f;
        isActive = true;
    }
}