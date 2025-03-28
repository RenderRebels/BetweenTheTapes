using UnityEngine;
using System.Collections;

public class BubbleProjectile : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float initialSpeed = 2f;  // Speed of the bubble
    public float floatStrength = 0.3f;  // Floatiness (upward movement)
    public float trackingSpeed = 1.5f;  // Speed at which it tracks the player
    public float lifetime = 5f;  // Time before bubble is destroyed

    private Rigidbody2D bubbleRb;  // Rigidbody2D component
    public int damageAmount = 1;  // The amount of damage the bubble does

    void Start()
    {
        // Get the Rigidbody2D component
        bubbleRb = GetComponent<Rigidbody2D>();

        // Disable gravity for the bubble
        if (bubbleRb != null)
        {
            bubbleRb.gravityScale = 0f; // Ensure gravity doesn't affect the bubble
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on Bubble! Ensure it's attached to the prefab.");
        }

        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player with 'Player' tag not found!");
        }

        // Start the tracking coroutine
        StartCoroutine(TrackPlayer());
    }

    private IEnumerator TrackPlayer()
    {
        float timer = 0f;

        while (timer < lifetime && player != null)
        {
            timer += Time.deltaTime;

            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Apply floating effect and move towards the player
            Vector3 floatyDirection = direction + Vector3.up * floatStrength;

            // Use linearVelocity to set the speed and direction (in 2D)
            bubbleRb.velocity = Vector2.Lerp(bubbleRb.velocity, floatyDirection * initialSpeed, trackingSpeed * Time.deltaTime);

            // Add a wave-like force for extra effect
            float wave = Mathf.Sin(Time.time * 2f) * 0.3f;
            bubbleRb.AddForce(new Vector2(wave, 0), ForceMode2D.Force);

            yield return null;
        }

        // Destroy the bubble after its lifetime ends
        Destroy(gameObject);
    }

    // Detect collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bubble collided with the player
        if (other.CompareTag("Player"))
        {
            // Get the player's CharacterHealth script and call TakeDamage
            CharacterHealth playerHealth = other.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Call the TakeDamage method on the player
            }

            // Destroy the bubble after it damages the player
            Destroy(gameObject);
        }
    }
}
