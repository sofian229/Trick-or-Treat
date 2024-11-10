using System.Collections;
using UnityEngine;

public class AltarOpener : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;       // Speed at which the object moves in the X-axis
    public float moveDistance = 5f;    // The total distance the object will move in the X direction
    public float stoppingDistance = 0.1f; // The distance at which the object stops moving

    [Header("Audio Settings")]
    public AudioClip interactionSound; // Sound to play when interacting with the object
    private AudioSource audioSource;   // AudioSource component to play sound

    [Header("Camera Shake Settings")]
    public Camera mainCamera;          // Reference to the main camera
    public float shakeDuration = 0.5f; // Duration of the camera shake
    public float shakeMagnitude = 0.2f; // Magnitude of the camera shake

    private bool isMoving = false;     // To track whether the object is currently moving
    private bool hasMoved = false;     // To track if the object has already moved
    private float startPositionX;      // Starting X position of the object
    private float targetPositionX;     // Target X position for the object

    private void Start()
    {
        // Store the initial position of the object
        startPositionX = transform.position.x;

        // Calculate the target position based on the starting position and move distance
        targetPositionX = startPositionX + moveDistance;

        // Get the AudioSource component attached to the object
        audioSource = GetComponent<AudioSource>();

        // Find the main camera if not assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        // Check if the player presses the "E" key and if the object is not already moving or moved
        if (Input.GetKeyDown(KeyCode.E) && !isMoving && !hasMoved)
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

        // Start the camera shake effect
        StartCoroutine(CameraShake());
    }

    // Method that moves the object along the X-axis
    void MoveObject()
    {
        // Move the object smoothly towards the target position
        float step = moveSpeed * Time.deltaTime; // Speed of movement per frame
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPositionX, transform.position.y, transform.position.z), step);

        // Check if the object is within the stopping distance of the target position
        if (Mathf.Abs(transform.position.x - targetPositionX) <= stoppingDistance)
        {
            isMoving = false;  // Stop moving once the target position is reached
            hasMoved = true;   // Mark the object as moved, preventing further interaction
        }
    }

    // Coroutine for camera shake effect
    IEnumerator CameraShake()
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0.0f;

        while (isMoving && elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.transform.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position
        mainCamera.transform.localPosition = originalPosition;
    }
}
