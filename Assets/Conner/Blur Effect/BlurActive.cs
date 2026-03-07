using UnityEngine;
using System.Collections;

public class BlurActive : MonoBehaviour
{
    [Header("Time Settings")]
    public float slowTimeScale = 0.2f;   // time scale during slow motion
    public float transitionSpeed = 4f;   // how fast time lerps
    public float slowDuration = 1.5f;    // how long slow motion lasts

    public bool SlowMoOver = false;      // set to true to immediately exit slow mo
    private float originalTimeScale;
    private bool isSlowing = false;

    private bool CoolDownOver = true;
    public float CoolDownTime;

    [SerializeField] private BackGroundSpawn _BackGroundSpawn;
    [SerializeField] private MiniGame _MiniGame;

    void Start()
    {
        CoolDownTime += slowDuration;
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlowing && CoolDownOver)
        {
            StartCoroutine(DoBulletTime());
            StartCoroutine(SlowMoCoolDown());
        }
    }

    IEnumerator DoBulletTime()
    {
        _BackGroundSpawn.BulletTimeActive();
        _MiniGame.StartMiniGame();
        isSlowing = true;
        SlowMoOver = false;

        // Smoothly slow down
        yield return StartCoroutine(SmoothTime(slowTimeScale));

        // Wait for the duration of slow motion OR until SlowMoOver is true
        float timer = 0f;
        while (timer < slowDuration)
        {
            if (SlowMoOver)
            {
                print("Won Mini game!");
                GameManager.Instance.Suceed = true;
                break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        if (!GameManager.Instance.Suceed)
        {
            print("lost mini game");
            GameManager.Instance.Suceed = false;
        }
        
        // Smoothly return to normal time
        yield return StartCoroutine(SmoothTime(originalTimeScale));
        _BackGroundSpawn.BulletTimeDeActive();

        isSlowing = false;
        SlowMoOver = false;
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

    IEnumerator SlowMoCoolDown()
    {
        CoolDownOver = false;
        yield return new WaitForSeconds(CoolDownTime);
        CoolDownOver = true;
    }
}