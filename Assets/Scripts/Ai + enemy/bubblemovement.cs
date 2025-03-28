using UnityEngine;
using System.Collections;

public class BubbleProjectile : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float initialSpeed = 2f;  // Speed of the bubble
    public float floatStrength = 0.3f;  // Floatiness (upward movement)
    public float trackingSpeed = 1.5f;  // Speed at which it tracks the player
    public float lifetime = 5f;  // Time before bubble is destroyed

    private Rigidbody bubbleRb;

    void Start()
    {
        // Get the Rigidbody component
        bubbleRb = GetComponent<Rigidbody>();

        // Disable gravity for the bubble
        if (bubbleRb != null)
        {
            bubbleRb.useGravity = false;
        }

        // Start the tracking coroutine
        StartCoroutine(TrackPlayer());
    }

    private IEnumerator TrackPlayer()
    {
        float timer = 0f;

        while (timer < lifetime)
        {
            timer += Time.deltaTime;

            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Apply floating effect and move towards the player
            Vector3 floatyDirection = direction + Vector3.up * floatStrength;

            // Use linearVelocity to set the speed and direction
            bubbleRb.linearVelocity = Vector3.Lerp(bubbleRb.linearVelocity, floatyDirection * initialSpeed, trackingSpeed * Time.deltaTime);

            // Add a wave-like force for extra effect
            float wave = Mathf.Sin(Time.time * 2f) * 0.3f;
            bubbleRb.AddForce(new Vector3(wave, 0, 0), ForceMode.Acceleration);

            yield return null;
        }

        // Destroy the bubble after its lifetime ends
        Destroy(gameObject);
    }
}
