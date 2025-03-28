using UnityEngine;
using System.Collections;

public class BossOne : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public GameObject bubbleProjectilePrefab;
    public GameObject levelEndTrigger; // Level end object (inactive until boss is defeated)

    public float chaseDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public float attackInterval = 3f;
    public int damageAmount = 1;
    public int bossHealth = 5;

    public int enrageHealth = 2; // Health value at which the boss will enrage

    private bool isEnraged = false;
    private Vector2 targetPoint;
    private float attackTimer;

    public Transform bubbleSpawnPoint;
    public float minY = 0f;
    public float maxY = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointB.position;

        // Make sure level end trigger is inactive until boss dies
        if (levelEndTrigger != null)
            levelEndTrigger.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            ChasePlayer();
            HandleAttacks();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Move the boss smoothly between pointA and pointB
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        // If close to a patrol point, switch target
        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = (targetPoint == (Vector2)pointA.position) ? pointB.position : pointA.position;
        }
    }

    void ChasePlayer()
    {
        // Move towards the player
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Clamp Y position to prevent excessive vertical movement
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = targetPosition;
    }

    void HandleAttacks()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            if (isEnraged)
            {
                StartCoroutine(BubbleProjectileAttack());
                StartCoroutine(BubbleProjectileAttack());
                StartCoroutine(BubbleProjectileAttack());
            }
            else
            {
                StartCoroutine(BubbleProjectileAttack());
            }
            attackTimer = 0;
        }
    }

    private IEnumerator BubbleProjectileAttack()
    {
        // Instantiate a bubble at the spawn point
        GameObject bubble = Instantiate(bubbleProjectilePrefab, bubbleSpawnPoint.position, Quaternion.identity);

        // Set the player reference for tracking
        BubbleProjectile bubbleScript = bubble.GetComponent<BubbleProjectile>();
        if (bubbleScript != null)
        {
            bubbleScript.player = player;
        }

        yield return null;
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("Boss took damage! HP left: " + bossHealth);

        if (bossHealth <= enrageHealth && !isEnraged)  // Enrage health check
        {
            isEnraged = true;
            Debug.Log("Boss is enraged!");
        }

        if (bossHealth <= 0)
        {
            Debug.Log("Boss defeated!");

            // Activate level end trigger when the boss dies
            if (levelEndTrigger != null)
                levelEndTrigger.SetActive(true);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boss detected collision with: " + other.gameObject.name);

        if (other.CompareTag("Bubble"))
        {
            Debug.Log("Bubble hit the boss!");
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
