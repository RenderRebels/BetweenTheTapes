using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public float chaseDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public AudioSource chaseSound;

    private Rigidbody2D rb;
    private Vector2 targetPoint;
    private bool isChasing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
                if (chaseSound && !chaseSound.isPlaying)
                {
                    chaseSound.Play();
                }
            }
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                if (chaseSound && chaseSound.isPlaying)
                {
                    chaseSound.Stop();
                }
            }
            Patrol();
        }

        // Flip sprite based on movement direction
        if (rb.velocity.x > 0)
            spriteRenderer.flipX = true;
        else if (rb.velocity.x < 0)
            spriteRenderer.flipX = false;
    }

    void Patrol()
    {
        // Determine movement direction
        Vector2 moveDirection = ((Vector2)targetPoint - rb.position).normalized;

        // Apply movement using physics
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Switch target when reaching a point
        if (Vector2.Distance(rb.position, pointA.position) < 0.1f)
            targetPoint = pointB.position;
        else if (Vector2.Distance(rb.position, pointB.position) < 0.1f)
            targetPoint = pointA.position;
    }

    void ChasePlayer()
    {
        // Move towards the player using physics
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
}
