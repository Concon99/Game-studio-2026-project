using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;       // Bullet speed
    public float homingSpeed = 5f;  // Speed when homing
    private Transform target;        // Current target
    private bool isHoming = false;   // Flag for homing
    private Rigidbody2D rb;
    private float timer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isHoming && target != null)
        {
            // Direction toward the target
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * homingSpeed;

            // Optional: rotate bullet to face target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHoming && other.CompareTag("Reflect"))
        {
            // Change tag to PlayerAttack
            gameObject.tag = "PlayerAttack";

            // Start homing
            isHoming = true;
            timer += Time.deltaTime; // Count time


            // Find the nearest Enemy
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
        }

        // Optional: destroy bullet on hitting something else
        if (other.CompareTag("enemy") && isHoming)
        {
            Destroy(gameObject);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
} 