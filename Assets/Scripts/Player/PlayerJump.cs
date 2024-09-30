using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float JumpForce;
    private Rigidbody2D rb;
    [SerializeField] private bool IsOnGround = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            anim.SetBool("isJumping", false);
        }
    }
    public void PerformJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsOnGround)
        {
            anim.SetBool("isJumping", true);
            rb.AddForce(Vector2.up * JumpForce);
            IsOnGround = false;
            Debug.Log("Jumping");
        }
    }

    void Update()
    {
        PerformJump();
    }
}
