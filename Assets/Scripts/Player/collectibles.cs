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

    //IEnumerator HidePopupAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    if (notePopup != null)
    //    {
    //        Debug.Log("Hiding popup now!"); // Debugging
    //        notePopup.SetActive(false); // Hide the UI
    //    }

    //    yield return new WaitForSeconds(2f); // Wait 2 more seconds
    //    Debug.Log("Destroying collectible now!"); // Debugging
    //    Destroy(gameObject); // Destroy the collectible
    //}
}
