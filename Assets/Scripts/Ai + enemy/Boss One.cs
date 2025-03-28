using UnityEngine;
using System.Collections;

public class BossOne : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public GameObject bubbleProjectilePrefab;
    public float chaseDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public float attackInterval = 3f;
    public int damageAmount = 1;
    public int bossHealth = 5; // Boss starts with 5 HP
    private bool isEnraged = false; // Enraged state

    private Vector3 targetPoint;
    private float attackTimer;

    public Transform bubbleSpawnPoint;  // Bubble spawn point

    void Start()
    {
        targetPoint = pointB.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
        {
            targetPoint = pointB.position;
        }
        else if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            targetPoint = pointA.position;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void HandleAttacks()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            // When enraged, spawn 3 bubbles in one attack cycle
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
        // Instantiate a new bubble at the spawn point
        GameObject bubble = Instantiate(bubbleProjectilePrefab, bubbleSpawnPoint.position, Quaternion.identity);

        // Set the player reference on the bubble to track the player
        BubbleProjectile bubbleScript = bubble.GetComponent<BubbleProjectile>();
        if (bubbleScript != null)
        {
            bubbleScript.player = player;  // Pass the player reference to the bubble
        }

        yield return null;
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("Boss took damage! HP left: " + bossHealth);

        if (bossHealth <= 2 && !isEnraged)
        {
            isEnraged = true;
            Debug.Log("Boss is enraged!");
        }

        if (bossHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Boss defeated!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // Destroy bubble on contact
        }
    }
}
