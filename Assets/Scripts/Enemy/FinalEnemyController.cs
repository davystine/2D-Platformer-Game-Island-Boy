using UnityEngine;

public class FinalEnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float attackDuration = 2f;
    public float returnDuration = 3f;
    public float attackCooldown = 5f;
    public float shootingRange = 10f;
    public float shootingCooldown = 2f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
   
    private Vector3 initialPosition;
    private float attackEndTime;
    private float returnEndTime;
    private float lastShotTime;

    private enum EnemyState
    {
        Moving,
        Attacking,
        Returning,
    }

    private EnemyState currentState;

    void Start()
    {
        initialPosition = transform.position;
        SetNewAttackTimes();
        SetState(EnemyState.Moving);
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                MoveTowardsPlayer();
                break;
            case EnemyState.Attacking:
                AttackPhase();
                break;
            case EnemyState.Returning:
                ReturnToInitialPosition();
                break;
        }
    }

    void SetState(EnemyState newState)
    {
        currentState = newState;
    }

    void AttackPhase()
    {
        if (CanShoot())
        {
            Shoot();
            lastShotTime = Time.time;
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = GetDirectionToPlayer();
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the enemy should transition to the attack phase
        if (currentState == EnemyState.Moving && Vector2.Distance(transform.position, player.position) < shootingRange)
        {
            SetState(EnemyState.Attacking);
        }
        // Check if the enemy should transition to the return phase
        else if (currentState == EnemyState.Attacking && Time.time > attackEndTime)
        {
            SetState(EnemyState.Returning);
        }
    }

    Vector2 GetDirectionToPlayer()
    {
        return (player.position - transform.position).normalized;
    }

    void ReturnToInitialPosition()
    {
        Vector2 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the initial position
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            SetState(EnemyState.Moving);
            SetNewAttackTimes();
        }
    }

    void SetNewAttackTimes()
    {
        attackEndTime = Time.time + attackDuration;
        returnEndTime = attackEndTime + returnDuration + attackCooldown;
    }

    bool CanShoot()
    {
        return (Time.time - lastShotTime) > shootingCooldown && Vector2.Distance(transform.position, player.position) < shootingRange;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    }
}
