using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public float chaseDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public AudioSource chaseSound;

    private Vector3 targetPoint;
    private bool isChasing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        targetPoint = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

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
}