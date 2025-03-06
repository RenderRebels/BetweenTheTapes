using UnityEngine;

public class ResetPlatformOnCollision : MonoBehaviour
{
    public MovingPlatform platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.transform.position = platform.pointA.position;
            platform.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.speed = 2.0f;
        }
    }
}
