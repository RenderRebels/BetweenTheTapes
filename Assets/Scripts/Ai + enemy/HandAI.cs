using Unity.VisualScripting;
using UnityEngine;

public class HandAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    [Header("Detection Settings")]
    public Transform player;
    public GameObject handPrefab;
    public float detectionRange = 5f;
    public int resetTime = 5;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;
    private bool isGroundClose = false;

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
            MoveTowardsAttackPlayer();
        }
    }


    private void TrackPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        movement = direction;
    }

    private void MoveTowardsAttackPlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void ResetPoint()
    {
        if (handPrefab)
        {

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

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (CompareTag("No go"))
        {
            isGroundClose = true;
        }
    }
}
