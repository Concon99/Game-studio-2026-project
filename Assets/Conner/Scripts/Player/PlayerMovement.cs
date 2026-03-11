using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;

    public Rigidbody2D rb;

    private Vector2 Movement;

    public string Direction;

    private void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");


        if (Movement.x > 0)
            Direction = "Up";
        else if (Movement.x < 0)
            Direction = "Down";
        else if (Movement.y > 0)
            Direction = "Left";
        else if (Movement.y < 0)
            Direction = "Right";
        
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement * Speed * Time.fixedDeltaTime);
    }
}
