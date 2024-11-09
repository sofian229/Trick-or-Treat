using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpener : MonoBehaviour
{
    public float interactionRange = 3f;  // Interaction range for the player
    public KeyCode interactKey = KeyCode.E;  // The key to interact (E)
    public AudioClip interactionSound;  // The sound to play when interacted with
    private AudioSource audioSource;  // The AudioSource component to play the sound

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the player is close enough to interact with the object
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= interactionRange)
        {
            // If the player presses the interact key (E)
            if (Input.GetKeyDown(interactKey))
            {
                InteractWithObject();
            }
        }
    }

    // Method that is called when the object is interacted with
    void InteractWithObject()
    {
        // Play the interaction sound if it's not null
        if (interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);  // Play the sound once
        }
    }
}
