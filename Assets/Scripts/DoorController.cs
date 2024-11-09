using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;        // Angle to which the door opens
    public float openSpeed = 2f;         // Speed at which the door opens
    public float closeSpeed = 2f;        // Speed at which the door closes
    public float openPauseDuration = 2f; // Pause duration before the door closes automatically

    [Header("Sound Settings")]
    public AudioClip openSound;          // Sound effect for opening the door
    public AudioClip closeSound;         // Sound effect for closing the door

    private AudioSource audioSource;     // AudioSource component for playing sounds
    private bool isPlayerNearby = false; // Tracks if the player is within range
    private bool isOpen = false;         // Tracks if the door is currently open
    private Quaternion initialRotation;  // The door's initial rotation
    private Quaternion targetRotation;   // The target rotation when door opens
    private Collider solidCollider;      // Reference to the solid collider component

    void Start()
    {
        // Store initial rotation and calculate target rotation
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);

        // Add an AudioSource component to play sound if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();

        // Find the solid collider component (the non-trigger collider)
        solidCollider = GetComponent<Collider>();

        // Ensure the solid collider is enabled initially (door is closed)
        solidCollider.enabled = true;
    }

    void Update()
    {
        // Check for player input to toggle the door open or close
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        // Disable the solid collider so the player can pass through
        if (solidCollider != null)
        {
            solidCollider.enabled = false;
        }

        // Play the open sound
        if (openSound)
        {
            audioSource.clip = openSound;
            audioSource.Play();
        }

        // Smoothly rotate the door to the target open rotation
        float elapsedTime = 0f;
        while (elapsedTime < openSpeed)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set to targetRotation
        transform.rotation = targetRotation;
        isOpen = true;

        // Wait for the specified pause duration before starting to close the door
        yield return new WaitForSeconds(openPauseDuration);

        // Close the door after the pause
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        // Play the close sound
        if (closeSound)
        {
            audioSource.clip = closeSound;
            audioSource.Play();
        }

        // Smoothly rotate the door back to its initial rotation
        float elapsedTime = 0f;
        while (elapsedTime < closeSpeed)
        {
            transform.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime / closeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set to initialRotation
        transform.rotation = initialRotation;
        isOpen = false;

        // Enable the solid collider to block the player again
        if (solidCollider != null)
        {
            solidCollider.enabled = true;
        }
    }

    // Detect if the player is near the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    // Detect if the player leaves the door area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
