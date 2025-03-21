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
    public float attackInterval = 3f; // How often the boss attacks
    public int damageAmount = 10;

    private Vector3 targetPoint;
    private float attackTimer;

    void Start()
    {
        // Start the boss moving towards point B first
        targetPoint = pointB.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            // If the player is close enough, chase them and try to attack
            ChasePlayer();
            HandleAttacks();
        }
        else
        {
            // Otherwise, just patrol back and forth
            Patrol();
        }
    }

    void Patrol()
    {
        // Move towards the current target point (either A or B)
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        // If we reach point A, switch to moving toward point B, and vice versa
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
        // Move towards the player's position
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void HandleAttacks()
    {
        attackTimer += Time.deltaTime;

        // If enough time has passed, launch a bubble attack
        if (attackTimer >= attackInterval)
        {
            StartCoroutine(BubbleProjectileAttack());
            attackTimer = 0;
        }
    }

    private IEnumerator BubbleProjectileAttack()
    {
        // Spawn a bubble at the boss's position
        GameObject bubble = Instantiate(bubbleProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody bubbleRb = bubble.GetComponent<Rigidbody>();

        if (bubbleRb != null)
        {
            float initialSpeed = 2f; // Start slower for a more bubble-like effect
            float floatStrength = 0.3f; // How much the bubble slowly rises
            float trackingSpeed = 1.5f; // How fast the bubble adjusts towards the player

            float timer = 0f;
            while (timer < 5f) // The bubble will last up to 5 seconds
            {
                timer += Time.deltaTime;

                // Continuously adjust direction toward the player
                Vector3 direction = (player.position - bubble.transform.position).normalized;
                Vector3 floatyDirection = direction + Vector3.up * floatStrength; // Adds an upward drift

                // Smoothly move toward the player while floating
                bubbleRb.linearVelocity = Vector3.Lerp(bubbleRb.linearVelocity, floatyDirection * initialSpeed, trackingSpeed * Time.deltaTime);

                // Add a gentle left-right wobble
                float wave = Mathf.Sin(Time.time * 2f) * 0.3f;
                bubbleRb.AddForce(new Vector3(wave, 0, 0), ForceMode.Acceleration);

                yield return null;
            }

            // After floating for a bit, the bubble pops
            Destroy(bubble);
        }
    }
}
