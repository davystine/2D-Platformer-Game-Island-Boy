using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public CoinManager cm;  // Assign this in the Unity Editor

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            // Increment the coin count before destroying the object
            cm.coinCount++;

            // Print the updated count (you can replace this with your own logic)
            Debug.Log("Coins Collected: " + cm.coinCount);

            // Destroy the coin object after updating the count
            Destroy(gameObject);
        }
    }
}
