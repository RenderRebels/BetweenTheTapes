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
    public float attackInterval = 3f; // Time between attacks
    public int damageAmount = 10;

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
            StartCoroutine(BubbleProjectileAttack());
            attackTimer = 0;
        }
    }

    private IEnumerator BubbleProjectileAttack()
    {
        // Instantiate bubble projectile and shoot towards player
        GameObject bubble = Instantiate(bubbleProjectilePrefab, transform.position, Quaternion.identity);
        bubble.GetComponent<Rigidbody>().linearVelocity = (player.position - transform.position).normalized * 5f;
        yield return new WaitForSeconds(1f);
    }
}
