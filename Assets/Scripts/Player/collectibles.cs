using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectibleItem : MonoBehaviour
{
    public GameObject notePopup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (notePopup != null)
        {
            Debug.Log("Popup should appear now!");
            notePopup.SetActive(true); // Show popup
            Destroy(gameObject, 5.0f);
            Destroy(notePopup, 5.0f);
            // StartCoroutine(HidePopupAfterDelay(5f)); // Hide after 5 seconds
        }
    }
}
