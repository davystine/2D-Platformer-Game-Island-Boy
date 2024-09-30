using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float chaseRange = 10f;
    public float attackRange = 12f;
    public float speed = 5f;
    public Animator animator;

    private GameObject player;

    private void Start()
    {
        // Find the player once at the start
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // Check if the player is not null
        if (player != null)
        {
            // Check if the player is within the chase range
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= chaseRange)
            {
                // Chase the player
                ChasePlayer();

                // Check if the enemy is close enough to attack
                if (distanceToPlayer <= attackRange)
                {
                    // Set the wolfAttack parameter to true
                    if (animator != null)
                    {
                        animator.SetBool("wolfAttack", true);
                    }

                    // You may also call a function to handle the attack logic
                    // Example: AttackPlayer();
                }
                else
                {
                    // Set the wolfAttack parameter to false when not attacking
                    if (animator != null)
                    {
                        animator.SetBool("wolfAttack", false);
                    }
                }
            }
        }
    }

    void ChasePlayer()
    {
        // Calculate the direction to the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Move towards the player
        transform.Translate(direction * speed * Time.deltaTime);

        // Flip the sprite based on the player's position
        if (direction.x > 0)
        {
            // Player is to the right
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x < 0)
        {
            // Player is to the left
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
