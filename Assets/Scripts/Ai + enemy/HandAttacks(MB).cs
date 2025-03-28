using UnityEngine;
using System.Collections;

public class HandBoss : MonoBehaviour
{
    // Reference to the player (target to attack)
    public Transform target;

    // Movement speeds for different attack phases
    public float swoopSpeed = 10f;
    public float slamSpeed = 15f;
    public float returnSpeed = 5f;
    public float attackDelay = 2f; // Time between attacks

    // Debris spawning variables
    public GameObject[] debrisPrefabs;
    public float debrisSpawnChance = 0.3f; // 30% chance to spawn debris on attack

    // Boss health and state tracking
    public int health = 5;
    private Vector3 startPosition;
    private bool isAttacking = false;
    private bool enraged = false;
    private bool playerInRange = false;

    private void Start()
    {
        startPosition = transform.position; // Store the initial position
        StartCoroutine(AttackLoop()); // Begin attack cycle
    }

    // Detect when the player enters the boss's range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // Detect when the player leaves the boss's range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Handles continuous attack cycle if the player is in range
    private IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            if (playerInRange && !isAttacking)
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

    // Swoop attack moves the hand toward the player
    private IEnumerator SwoopDown()
    {
        isAttacking = true;
        Vector3 swoopTarget = target.position;

        while (Vector3.Distance(transform.position, swoopTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, swoopTarget, swoopSpeed * Time.deltaTime);
            yield return null;
        }

        DamagePlayer(); // Damage the player if hit
        isAttacking = false;
    }

    // Slam attack moves the hand down hard, possibly spawning debris
    private IEnumerator SlamDown()
    {
        isAttacking = true;
        Vector3 slamPosition = new Vector3(target.position.x, target.position.y - 2f, target.position.z);

        while (Vector3.Distance(transform.position, slamPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, slamPosition, slamSpeed * Time.deltaTime);
            yield return null;
        }

        DamagePlayer(); // Damage the player if hit

        // Spawn debris randomly
        if (Random.value < debrisSpawnChance)
        {
            SpawnRandomDebris();
        }

        isAttacking = false;
    }

    // Damages the player if they are in range
    private void DamagePlayer()
    {
        if (playerInRange)
        {
            CharacterHealth playerHealth = target.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.SendMessage("OnCollisionEnter2D", new Collision2D()); // Trigger damage on the player
            }
        }
    }

    // Spawns a random debris prefab at the hand's position
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

    // Returns the hand to its starting position after an attack
    private IEnumerator ReturnToStart()
    {
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Reduces the boss's health when hit
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss Health: " + health);

        if (health <= 2 && !enraged)
        {
            Enrage(); // Boss enters enraged state when low on health
        }

        if (health <= 0)
        {
            Die(); // Boss is defeated when health reaches 0
        }
    }

    // Enraged state makes the boss faster and more aggressive
    private void Enrage()
    {
        enraged = true;
        swoopSpeed *= 1.5f;
        slamSpeed *= 1.5f;
        returnSpeed *= 1.5f;
        attackDelay *= 0.7f;
        debrisSpawnChance = 0.6f;
        Debug.Log("Hand Boss is enraged! It moves faster and attacks more aggressively.");
    }

    // Handles boss defeat
    private void Die()
    {
        Debug.Log("Hand Boss Defeated!");
        Destroy(gameObject);
    }
}
