using UnityEngine;
using UnityEngine.SceneManagement;  // Import this to use SceneManager

public class SceneTransitionVent : MonoBehaviour
{
    public string sceneName; // The name of the scene to transition to
    public float interactionRange = 3f; // The distance within which the player can interact with the object
    public KeyCode interactKey = KeyCode.E; // The key to interact with the object
    private bool playerInRange = false; // Track if the player is in range of the object

    private void Update()
    {
        // Check if the player is in range and if the interact key is pressed
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            // Transition to the specified scene
            TransitionToScene();
        }
    }

    // Triggered when the player enters the interaction range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Player is now in range
        }
    }

    // Triggered when the player exits the interaction range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Player is no longer in range
        }
    }

    // Method to change the scene
    void TransitionToScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load the scene by name
        }
        else
        {
            Debug.LogError("Scene name is not set.");
        }
    }
}
