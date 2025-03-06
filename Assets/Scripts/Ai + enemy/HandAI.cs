using UnityEngine;

public class HandAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    [Header("Detection Settings")]
    public Transform player;
    public Transform startPosition;
    public float detectionRange = 5f; 

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;

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
