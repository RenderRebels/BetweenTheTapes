using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform targetLocation;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetLocation != null)
            {
                other.transform.position = targetLocation.position;
                Debug.Log("Player teleported to: " + targetLocation.position);
            }
            else
            {
                Debug.LogWarning("Target location is not set in the inspector!");
            }
        }
    }
}
