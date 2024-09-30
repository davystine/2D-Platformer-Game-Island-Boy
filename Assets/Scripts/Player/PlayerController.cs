using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the player horizontally
        MovePlayer(horizontalInput);

        // Set isRunning for animation
        bool isRunning = Mathf.Abs(horizontalInput) > 0;
        animator.SetBool("isRunning", isRunning);
    }

    void MovePlayer(float horizontal)
    {
        // Move the player
        Vector2 movement = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip the player sprite if moving in the opposite direction
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
