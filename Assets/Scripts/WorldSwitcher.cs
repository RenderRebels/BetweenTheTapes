using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    // Assign the world parent objects in the Unity Inspector
    public GameObject world1;
    public GameObject world2;

    // Tracks the currently active world
    private bool isWorld1Active = true;

    void Start()
    {
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
        // Check if the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
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

            Debug.Log(isWorld1Active ? "Switched to World 1" : "Switched to World 2");
        }
        else
        {
            Debug.LogError("World objects not assigned!");
        }
    }
}
