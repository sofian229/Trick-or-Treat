using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGone : MonoBehaviour
{
    public float interactionRange = 3f; // Interaction range for the player
    public KeyCode interactKey = KeyCode.E; // Default interaction key (E)

    private void Update()
    {
        // Check for player input only if within interaction range
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= interactionRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                // Call the method to destroy the object
                DestroyObject();
            }
        }
    }

    // Method to destroy the object
    void DestroyObject()
    {
        // Optionally, you can add a sound effect or visual effect here before destroying
        Destroy(gameObject); // This will remove the object from the scene
    }
}
