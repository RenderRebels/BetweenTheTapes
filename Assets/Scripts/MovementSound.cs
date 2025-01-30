using UnityEngine;

public class MovementSound : MonoBehaviour
{
    public AudioSource movementAudio; // Assign in Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (!movementAudio.isPlaying)
            {
                movementAudio.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
        {
            movementAudio.Stop();
        }
    }
}
