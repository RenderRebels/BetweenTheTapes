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
            GameObject popupInstance = Instantiate(notePopup, transform.position, Quaternion.identity);
            StartCoroutine(HidePopupAfterDelay(popupInstance, 1f));
        }
        Destroy(gameObject);
    }

    IEnumerator HidePopupAfterDelay(GameObject popup, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (popup != null)
        {
            Destroy(popup);
        }
    }
}
