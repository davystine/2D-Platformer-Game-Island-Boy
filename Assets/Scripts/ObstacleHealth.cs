using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public int damageStrength = 5; // Strength of the obstacle's attack

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check if the obstacle is destroyed
        if (currentHealth == 0)
        {
            DestroyObstacle();
        }
    }

    void DestroyObstacle()
    {
        // Add any destruction-related logic here
        Debug.Log("Obstacle has been destroyed!");
        // For example, play destruction animation, disable collider, etc.
        // You might also want to spawn a destruction effect or handle any other necessary actions.
        Destroy(gameObject);
    }
}
