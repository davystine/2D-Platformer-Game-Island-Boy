using UnityEngine;
using UnityEngine.Events;

public class ThrownObject : MonoBehaviour
{
    public UnityEvent OnHitGround = new UnityEvent();

    void Start()
    {
        // Ignore collision between this object and any object tagged as "Player"
        Collider2D throwCollider = GetComponent<Collider2D>();
        if (throwCollider != null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                Collider2D playerCollider = player.GetComponent<Collider2D>();
                if (playerCollider != null)
                {
                    Physics2D.IgnoreCollision(throwCollider, playerCollider);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Assuming the ground has a tag "Ground"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            OnHitGround.Invoke();
            Destroy(gameObject);
        }
        // Handle other collisions as needed
    }
}
