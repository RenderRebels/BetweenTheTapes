using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;

    private bool isWorld1Active = true;
    private AudioSource audioSource;

    public AudioClip switchSound; // Assign in the Inspector

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure one world is active and the other is inactive at the start
        if (world1 != null && world2 != null)
        {
            world1.SetActive(true);
            world2.SetActive(false);
        }
        else
        {
            Debug.LogError("World objects not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(0))
        {
            SwitchWorld();
        }

    }

    void SwitchWorld()
    {
        if (world1 != null && world2 != null)
        {
            isWorld1Active = !isWorld1Active;

            // Toggle the worlds
            world1.SetActive(isWorld1Active);
            world2.SetActive(!isWorld1Active);

            // Play sound effect when switching
            if (switchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(switchSound);
            }
            else
            {
                Debug.LogError("No AudioClip assigned or AudioSource missing!");
            }

            Debug.Log(isWorld1Active ? "Switched to World 1" : "Switched to World 2");
        }
        else
        {
            Debug.LogError("World objects not assigned!");
        }
    }
}
