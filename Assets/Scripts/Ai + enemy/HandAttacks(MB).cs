using UnityEngine;
using System.Collections;

public class HandBoss : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public float slamSpeed = 15f;
    public float returnSpeed = 5f;
    public float attackDelay = 2f;
    public float slamStayDuration = 1f;
    public GameObject[] debrisPrefabs;
    public float debrisSpawnChance = 0.3f;
    public int health = 5;
    private Vector3 startPosition;
    private bool isAttacking = false;
    private bool playerInRange = false;
    private bool isPlayerInRange = false;

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AttackLoop());
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            TrackPlayer();
        }
    }

    private void TrackPlayer()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * slamSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerInRange = false;
        }
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            if (playerInRange && !isAttacking)
            {
                yield return StartCoroutine(SlamDown());
                yield return StartCoroutine(ReturnToStart());
            }
        }
    }

    private IEnumerator SlamDown()
    {
        isAttacking = true;
        Vector3 slamPosition = new Vector3(target.position.x, target.position.y - 2f, target.position.z);

        while (Vector3.Distance(transform.position, slamPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, slamPosition, slamSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(slamStayDuration);
        DamagePlayer();
        if (Random.value < debrisSpawnChance)
        {
            SpawnRandomDebris();
        }

        isAttacking = false;
    }

    private void DamagePlayer()
    {
        if (playerInRange)
        {
            CharacterHealth playerHealth = target.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void SpawnRandomDebris()
    {
        if (debrisPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, debrisPrefabs.Length);
            GameObject debrisPrefab = debrisPrefabs[randomIndex];
            if (debrisPrefab != null)
            {
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator ReturnToStart()
    {
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Hand Boss Defeated!");
        Destroy(gameObject);
    }
}
