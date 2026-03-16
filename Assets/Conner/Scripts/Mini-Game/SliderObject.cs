using UnityEngine;

public class SliderObject : MonoBehaviour
{
    [Header("Slider Settings")]
    public float slideSpeed = 2f;           // how fast the slider moves
    public Vector3 slideDirection;          // direction to slide
    public float slideDuration = 2f;        // how long before destroying
    public int pointsOnHit = 1;             // points awarded if mouse is over at destroy

    [Header("Trail Settings")]
    public GameObject trailPrefab;          // small sprite prefab for trail
    public float trailSpawnRate = 0.05f;    // seconds between trail particles
    public float trailRandomOffset = 0.2f;  // random offset from slider center
    public Vector2 trailSizeRange = new Vector2(0.3f, 0.6f); // min/max scale

    private bool isMouseOver = false;
    private float timer = 0f;
    private float trailTimer = 0f;

    void Start()
    {
        // Pick one of 8 directions randomly at spawn
        int dir = Random.Range(0, 8);
        switch (dir)
        {
            case 0: slideDirection = Vector3.up; break;
            case 1: slideDirection = Vector3.down; break;
            case 2: slideDirection = Vector3.left; break;
            case 3: slideDirection = Vector3.right; break;
            case 4: slideDirection = (Vector3.up + Vector3.right).normalized; break;
            case 5: slideDirection = (Vector3.up + Vector3.left).normalized; break;
            case 6: slideDirection = (Vector3.down + Vector3.right).normalized; break;
            case 7: slideDirection = (Vector3.down + Vector3.left).normalized; break;
        }
    }

    void Update()
    {
        // Move the slider
        transform.position += slideDirection * slideSpeed * Time.unscaledDeltaTime;

        // Count slide duration
        timer += Time.unscaledDeltaTime;
        if (timer >= slideDuration)
        {
            // Award points if mouse is over when slider ends
            if (isMouseOver)
            {
                GameManager.Instance.Points += pointsOnHit;
            }

            Destroy(gameObject);
        }

        // Spawn trail
        trailTimer += Time.unscaledDeltaTime;
        if (trailPrefab != null && trailTimer >= trailSpawnRate)
        {
            SpawnTrail();
            trailTimer = 0f;
        }
    }

    void SpawnTrail()
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-trailRandomOffset, trailRandomOffset),
            Random.Range(-trailRandomOffset, trailRandomOffset),
            0f
        );

        GameObject trail = Instantiate(trailPrefab, randomPos, Quaternion.identity);

        // Random scale
        float scale = Random.Range(trailSizeRange.x, trailSizeRange.y);
        trail.transform.localScale = new Vector3(scale, scale, 1f);

        // Random color
        SpriteRenderer sr = trail.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(Random.value, Random.value, Random.value, 1f);
        }
    }

    void OnMouseEnter() => isMouseOver = true;
    void OnMouseExit() => isMouseOver = false;
}