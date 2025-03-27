using UnityEngine;
using System.Collections;

public class HandBoss : MonoBehaviour
{
    public Transform target;
    public float swoopSpeed = 10f;
    public float slamSpeed = 15f;
    public float returnSpeed = 5f;
    public float attackDelay = 2f;
    public GameObject[] debrisPrefabs; // Array of debris prefabs
    public float debrisSpawnChance = 0.3f; // 30% chance
    public int health = 5; // Boss health

    private Vector3 startPosition;
    private bool isAttacking = false;
    private bool enraged = false;
    private bool playerInRange = false;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(AttackLoop());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            if (playerInRange && !isAttacking) // Only attack if the player is in range
            {
                if (Random.value > 0.5f)
                {
                    yield return StartCoroutine(SwoopDown());
                }
                else
                {
                    yield return StartCoroutine(SlamDown());
                }
                yield return StartCoroutine(ReturnToStart());
            }
        }
    }

    private IEnumerator SwoopDown()
    {
        isAttacking = true;
        Vector3 swoopTarget = target.position;
        while (Vector3.Distance(transform.position, swoopTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, swoopTarget, swoopSpeed * Time.deltaTime);
            yield return null;
        }
        isAttacking = false;
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

        if (Random.value < debrisSpawnChance) // Chance to spawn debris
        {
            SpawnRandomDebris();
        }

        isAttacking = false;
    }

    private void SpawnRandomDebris()
    {
        if (debrisPrefabs.Length > 0) // Make sure there are debris prefabs assigned
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

        if (health <= 2 && !enraged)
        {
            Enrage();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Enrage()
    {
        enraged = true;
        swoopSpeed *= 1.5f;
        slamSpeed *= 1.5f;
        returnSpeed *= 1.5f;
        attackDelay *= 0.7f;
        debrisSpawnChance = 0.6f; // Increase chance to spawn debris
        Debug.Log("Hand Boss is enraged! It moves faster and attacks more aggressively.");
    }

    private void Die()
    {
        Debug.Log("Hand Boss Defeated!");
        Destroy(gameObject);
    }
}
