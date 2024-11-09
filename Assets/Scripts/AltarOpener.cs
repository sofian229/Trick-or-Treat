using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarOpener : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves in the X-axis
    public float moveDistance = 5f; // The distance the object will move in the X direction
    public AudioClip interactionSound; // Sound to play when interacting with the object
    private AudioSource audioSource; // AudioSource component to play sound
    private bool isMoving = false; // To track whether the object is currently moving
    private float startPositionX; // Starting X position of the object

    private void Start()
    {
        // Store the initial position of the object
        startPositionX = transform.position.x;

        // Get the AudioSource component attached to the object
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the player presses the "E" key and if the object is not already moving
        if (Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            // Start the interaction process
            StartInteraction();
        }

        // If the object is moving, update its position
        if (isMoving)
        {
            MoveObject();
        }
    }

    // Method that starts the interaction and begins moving the object
    void StartInteraction()
    {
        // Optionally play the sound effect if it's assigned
        if (interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound); // Play the sound once
        }

        // Set the flag to true so the object starts moving
        isMoving = true;
    }

    // Method that moves the object along the X-axis
    void MoveObject()
    {
        // Calculate the target position based on the starting position and desired distance
        float targetPositionX = startPositionX + moveDistance;

        // Move the object smoothly towards the target position
        float step = moveSpeed * Time.deltaTime; // Speed of movement per frame
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPositionX, transform.position.y, transform.position.z), step);

        // If the object has reached the target position, stop moving
        if (transform.position.x == targetPositionX)
        {
            isMoving = false; // Stop moving once the target position is reached
        }
    }
}
