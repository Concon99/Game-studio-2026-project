using Unity.VisualScripting;
using UnityEngine;

public class DUMB_PLAYER : MonoBehaviour
{
    public float Health;
    public float Speed;

    public Rigidbody2D rb;

    private Vector2 Movement;

    private void Update()
    {
            
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * Speed * Time.fixedDeltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("bullet"))
        {
            print("HIT");
            Destroy(hit.gameObject);
            Health -= 5;

        }
    }
}