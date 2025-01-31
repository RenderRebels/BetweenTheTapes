using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Starting point
    public Transform pointB; // Ending point
    public float speed = 2.0f; // Speed of the platform

    private Vector3 target;
    private bool movingToB = true;

    void Start()
    {
        target = pointB.position; // Set initial target to pointB
    }

    void Update()
    {
        // Move the platform towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached the target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch target to the other point
            if (movingToB)
            {
                target = pointA.position;
                movingToB = false;
            }
            else
            {
                target = pointB.position;
                movingToB = true;
            }
        }
    }
}