using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public int damageStrength = 10;
    public bool isAlive = true;
    public int weaponDamageAmount = 10;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public string DeathAnimation = "wolfDie";
    public string AttackAnimation = "wolfAttack";
    public GameObject slidecollider;

    private const float DefaultAnimationLength = 2.0f;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive)
        {
            // Your enemy's AI logic goes here
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.collider.name);

        if (collision.collider.CompareTag("SlideCollider"))
        {
            // Disable the collider on the current GameObject (the enemy)
            Collider2D enemyCollider = GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }

            // Trigger death animation and destroy enemy after the animation
            animator.SetBool(DeathAnimation, true);
            Invoke("DestroyEnemy", GetAnimationLength());
        }

        if (collision.gameObject.CompareTag("Weapon"))
        {
            // Handle weapon collision
            TakeDamage(weaponDamageAmount);
            StartCoroutine(TintSpriteRed());
            animator.SetBool(AttackAnimation, true);
        }

       
    }




    private float GetAnimationLength()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
        return DefaultAnimationLength;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void Die()
    {
        isAlive = false;
        animator.SetBool(DeathAnimation, true);
        Invoke("DestroyEnemy", GetAnimationLength());
        Debug.Log("Enemy has died!");
    }

    IEnumerator TintSpriteRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
