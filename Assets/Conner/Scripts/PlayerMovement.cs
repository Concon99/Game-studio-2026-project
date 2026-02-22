using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
}
