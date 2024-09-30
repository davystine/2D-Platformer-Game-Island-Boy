using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float throwAngle = 45f; // Launch angle in degrees

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ThrowObject();
        }
    }

    void ThrowObject()
    {
        animator.SetTrigger("Throw");
        // Instantiate the throwable object from the prefab
        GameObject throwableObject = Instantiate(weaponPrefab, throwPoint.position, Quaternion.identity);

        // Access the Rigidbody2D component of the instantiated object
        Rigidbody2D rb = throwableObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Determine the throw direction based on the player's facing direction
            Vector2 throwDirection = PlayerFacingDirection();

            // Calculate the initial velocity for the desired angle
            Vector2 throwVelocity = new Vector2(throwDirection.x, Mathf.Sin(Mathf.Deg2Rad * throwAngle)) * throwForce;

            // Apply force to throw the object
            rb.AddForce(throwVelocity, ForceMode2D.Impulse);
        }

        EndThrowAnimation();
    }

    Vector3 PlayerFacingDirection()
    {
        // Assuming your player has a Transform component for facing direction
        Transform playerTransform = transform;

        if (playerTransform != null)
        {
            // Use the player's local scale to determine the facing direction
            float direction = playerTransform.localScale.x;

            return new Vector3(direction, 1f, 1f).normalized;
        }

        // Return a default direction if something goes wrong
        return Vector3.right;
    }


    void EndThrowAnimation()
    {
        animator.SetTrigger("Idle");
    }
}
