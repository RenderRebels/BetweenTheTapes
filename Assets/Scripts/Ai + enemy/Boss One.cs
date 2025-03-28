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
    public float minY = 0f;  // Minimum Y position
    public float maxY = 5f;  // Maximum Y position

    public GameObject levelEndTrigger; // Reference to the level end trigger GameObject

    void Start()
    {
        targetPoint = pointB.position;

        // Initially disable the level end trigger
        if (levelEndTrigger != null)
        {
            levelEndTrigger.SetActive(false);
        }
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
        transform.position = Vector3.Lerp(transform.position, targetPoint, 0.05f); // Smooth patrol

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
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f); // Smooth chase
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
        GameObject bubble = Instantiate(bubbleProjectilePrefab, bubbleSpawnPoint.position, Quaternion.identity);
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

        if (bossHealth <= 2 && !isEnraged)
        {
            isEnraged = true;
            Debug.Log("Boss is enraged!");
        }

        if (bossHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Boss defeated!");

            // Enable the level end trigger after boss defeat
            if (levelEndTrigger != null)
            {
                levelEndTrigger.SetActive(true);
            }
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
