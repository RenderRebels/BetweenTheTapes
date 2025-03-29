using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    [Header("Detection Settings")]
    public Transform player; 
    public float detectionRange = 5f; 

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;
    private bool movingLeft = true; 

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
            
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                movingLeft = !movingLeft; 
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
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