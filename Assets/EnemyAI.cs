using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    [Header("Detection Settings")]
    public Transform player; // Reference to the player's transform
    public float detectionRange = 5f; // Distance at which the enemy detects the player

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;
    private bool movingLeft = true; // Determines the initial movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            TrackPlayer();
        }
    }

    void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void TrackPlayer()
    {
        // Calculate direction towards player
        Vector2 direction = (player.position - transform.position).normalized;
        movement = direction;
    }

    private void MoveTowardsPlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Patrol()
    {
        Vector2 patrolDirection = movingLeft ? Vector2.left : Vector2.right;
        rb.MovePosition(rb.position + patrolDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check if the contact is on the left or right side of the enemy
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                movingLeft = !movingLeft; // Change direction when hitting a wall
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assumes player has a tag of "Player"
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
