using UnityEngine;

public class PatrollingFish : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolRange = 3f;
    public Transform patrolCenter;

    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private bool movingRight = true;

    private bool playerInTrigger = false;
    private Transform player;

    void Start()
    {
        if (patrolCenter == null)
            patrolCenter = transform;

        leftPoint = patrolCenter.position - Vector3.right * patrolRange;
        rightPoint = patrolCenter.position + Vector3.right * patrolRange;
    }

    void Update()
    {
        Patrol();

        if (playerInTrigger && player != null)
        {
            AttackPlayer();
        }
    }

    void Patrol()
    {
        Vector3 target = movingRight ? rightPoint : leftPoint;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1); // Flip fish visually
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Fish attacks the player!");
        // Add actual attack logic here (e.g., reduce health, play animation, etc.)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            player = null;
        }
    }
}
