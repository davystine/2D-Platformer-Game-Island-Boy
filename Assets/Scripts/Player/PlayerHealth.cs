using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar; // Reference to your health bar slider
    public SpriteRenderer playerSpriteRenderer; // Reference to the player's SpriteRenderer component
    public Color damageColor = Color.red; // Color to change the sprite when taking damage
    public float damageDuration = 0.2f; // Duration of the sprite color change
    public Collider2D PlayerColl;
    public Collider2D DeathColl;
    public GameObject gameOverUI;

    private Animator anim;
    private int continuousDamage = 1; // Adjust the damage amount as needed
    private float damageInterval = 1.0f; // Adjust the delay between each damage application
    private float timeSinceLastDamage = 0.0f; // Track the time since the last damage application

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Ensure the health bar and player sprite renderer are not null
        if (healthBar == null)
        {
            Debug.LogError("Health bar reference is missing! Please assign the health bar in the Inspector.");
        }

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer == null)
            {
                Debug.LogError("Player's SpriteRenderer component is missing! Please assign it in the Inspector or ensure the player has a SpriteRenderer component.");
            }
        }
        else
        {
            // Set the initial value of the health bar
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        // This script relies on OnCollisionStay2D for continuous damage
        timeSinceLastDamage += Time.deltaTime;

        // Check if the player falls below Y position -10
        if (transform.position.y < -10)
        {
            currentHealth = 0;
            UpdateHealthBar();
            // Call the Die method to handle player death
            Die();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the player is colliding with an enemy or obstacle
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Obstacle"))
        {
            // Check if enough time has passed since the last damage application
            if (timeSinceLastDamage >= damageInterval)
            {
                // Get the damage strength from the collided object and apply damage
                int damageStrength = 0;
                if (collision.collider.CompareTag("Enemy"))
                {
                    // If the collided object is an enemy, get the EnemyHealth component and retrieve the damage strength
                    EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        damageStrength = enemyHealth.damageStrength;
                    }
                }
                else if (collision.collider.CompareTag("Obstacle"))
                {
                    // If the collided object is an obstacle, get the ObstacleHealth component and retrieve the damage strength
                    ObstacleHealth obstacleHealth = collision.collider.GetComponent<ObstacleHealth>();
                    if (obstacleHealth != null)
                    {
                        damageStrength = obstacleHealth.damageStrength;
                    }
                }

                // Apply damage based on the retrieved damage strength
                TakeDamage(damageStrength);


                // Reset the timer
                timeSinceLastDamage = 0.0f;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        UpdateHealthBar();

        // Change the player's sprite color to red
        StartCoroutine(FlashSpriteColor());

        // Check if the player is dead
        if (currentHealth == 0)
        {
            Die();
        }
    }

    IEnumerator FlashSpriteColor()
    {
        playerSpriteRenderer.color = damageColor;
        anim.SetTrigger("Hurt");
        yield return new WaitForSeconds(damageDuration);
        playerSpriteRenderer.color = Color.white; // Assuming the default color is white, adjust if needed
        //anim.SetBool("isHurt", false);

    }

    void UpdateHealthBar()
    {
        // Update the value of the health bar
        healthBar.value = currentHealth;
    }

    void Die()
    {
        anim.SetTrigger("Death");
        PlayerColl.enabled = false;
        DeathColl.enabled = true;

        // Delay the deactivation of the player GameObject by 0.2 seconds
        Invoke("DeactivatePlayer", 1.5f);

        // Enable Game Over GUI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Add any other death-related logic here
        Debug.Log("Player has died!");
    }

    void DeactivatePlayer()
    {
        // Deactivate the player GameObject
        gameObject.SetActive(false);
    }


    // Example function for healing the player
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Ensure health doesn't exceed the maximum
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        UpdateHealthBar();
    }

    // Example usage of healing when a power-up is collected
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp")) // Assuming the power-up has the "PowerUp" tag
        {
            // Heal the player when a power-up is collected
            Heal(20); // Adjust the heal amount as needed
            Destroy(other.gameObject); // Destroy the power-up object
        }
    }
}
