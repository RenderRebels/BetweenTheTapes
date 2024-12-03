using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    [Header("Parent GameObjects")]
    [Tooltip("The first parent GameObject.")]
    public GameObject parent1;

    [Tooltip("The second parent GameObject.")]
    public GameObject parent2;

    [Header("Key Settings")]
    [Tooltip("The key to toggle between parent GameObjects.")]
    public KeyCode toggleKey = KeyCode.E;

    void Start()
    {
        // Validate inputs and initialize states
        if (parent1 == null || parent2 == null)
        {
            Debug.LogError("Parent GameObjects not assigned. Please assign them in the Inspector.");
            return;
        }

        // Ensure only one parent is active initially
        parent1.SetActive(true);
        parent2.SetActive(false);
    }

    void Update()
    {
        // Check for the toggle key press
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleParents();
        }
    }

    private void ToggleParents()
    {
        // Validate inputs again before toggling
        if (parent1 == null || parent2 == null)
        {
            Debug.LogError("Parent GameObjects are missing.");
            return;
        }

        // Toggle active states
        bool isParent1Active = parent1.activeSelf;
        parent1.SetActive(!isParent1Active);
        parent2.SetActive(isParent1Active);

        Debug.Log($"Switched to {(isParent1Active ? parent2.name : parent1.name)}");
    }
}
