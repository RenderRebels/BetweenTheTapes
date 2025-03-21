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
    public int damageAmount = 10;
    public int bossHealth = 5; // Boss starts with 5 HP
    private bool isEnraged = false; // Enraged state

    private Vector3 targetPoint;
    private float attackTimer;

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
        GameObject bubble = Instantiate(bubbleProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody bubbleRb = bubble.GetComponent<Rigidbody>();

        if (bubbleRb != null)
        {
            float initialSpeed = 2f;
            float floatStrength = 0.3f;
            float trackingSpeed = 1.5f;
            float timer = 0f;

            while (timer < 5f)
            {
                timer += Time.deltaTime;
                Vector3 direction = (player.position - bubble.transform.position).normalized;
                Vector3 floatyDirection = direction + Vector3.up * floatStrength;
                bubbleRb.linearVelocity = Vector3.Lerp(bubbleRb.linearVelocity, floatyDirection * initialSpeed, trackingSpeed * Time.deltaTime);

                float wave = Mathf.Sin(Time.time * 2f) * 0.3f;
                bubbleRb.AddForce(new Vector3(wave, 0, 0), ForceMode.Acceleration);

                yield return null;
            }

            Destroy(bubble);
        }
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
            Destroy(other.gameObject);
        }
    }
}
