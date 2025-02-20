using UnityEngine;

public class Bassboss : MonoBehaviour
{
    [SerializeField]
    GameObject bubblePrefab;
    public float bubblespeed;

    public Transform player;
    public Transform pointA;
    public Transform pointB;
    public float chaseDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public AudioSource;

    private Vector3 targetPoint;
    private bool isChasing = false;

    void Start()
    {
        targetPoint = pointB.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
            }
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
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

        if (ChasePlayer())
        {
            GameObject bubble = Instantiate(bubblePrefab);

            bubble.transform.position = transform.position + direction * .70f;
            bubble.GetComponent<Rigidbody2D>().velocity = direction * bubblespeed;

            Destroy(bubble, 1.0f);
        }
    }
}
