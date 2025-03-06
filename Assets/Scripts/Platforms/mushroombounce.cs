using UnityEngine;
using System.Collections;

public class mushroombounce : MonoBehaviour
{
    public float bounceHeight = 4f;
    public float bounceDuration = 0.5f;
    private bool isBouncing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isBouncing)
        {
            isBouncing = true;
            StartCoroutine(StartBounceWithDelay(other.transform));
        }
    }

    private IEnumerator StartBounceWithDelay(Transform playerTransform)
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(BouncePlayer(playerTransform));
    }

    private IEnumerator BouncePlayer(Transform playerTransform)
    {
        Vector3 originalPosition = playerTransform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * bounceHeight;

        float timeElapsed = 0f;

        while (timeElapsed < bounceDuration)
        {
            playerTransform.position = Vector3.Lerp(originalPosition, targetPosition, timeElapsed / bounceDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = targetPosition;

        timeElapsed = 0f;
        Vector3 fallTargetPosition = originalPosition;

        while (timeElapsed < bounceDuration)
        {
            playerTransform.position = Vector3.Lerp(targetPosition, fallTargetPosition, timeElapsed / bounceDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = fallTargetPosition;

        isBouncing = false;
    }
}
