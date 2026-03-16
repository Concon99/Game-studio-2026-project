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
    [SerializeField] private MiniGameAttack _MiniGameAttack;
    

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

        if (GameManager.Instance.MiniGameType == "Click")
        {
            slowDuration = 7f;
            CoolDownTime = 6f;
        }
        if (GameManager.Instance.MiniGameType == "Click")
        {
            slowDuration = 7f;
            CoolDownTime = 6f;
        }
        if (GameManager.Instance.MiniGameType == "Slide")
        {
            slowDuration = 10;
            CoolDownTime = 6f;
        }
    }

    IEnumerator DoBulletTime()
    {
        GameManager.Instance.BulletTimeActive = true;
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
                break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        

        StartCoroutine(_MiniGameAttack.WeaponAttack());
        GameManager.Instance.BulletTimeActive = false;
        GameManager.Instance.Points = 0;
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
        yield return new WaitForSecondsRealtime(CoolDownTime);
        CoolDownOver = true;
    }
}