using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;        // Angle to which the door opens
    public float openSpeed = 2f;         // Speed at which the door opens
    public float closeDelay = 2f;        // Delay before the door closes automatically

    [Header("Sound Settings")]
    public AudioClip doorSound;          // Sound effect for the door
    private AudioSource audioSource;     // AudioSource component for playing the sound

    private bool isPlayerNearby = false; // Tracks if the player is within range
    private bool isOpen = false;         // Tracks if the door is currently open
    private Quaternion initialRotation;  // The door's initial rotation
    private Quaternion targetRotation;   // The target rotation when door opens
    private Collider doorCollider;       // The door's collider

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + openAngle, transform.eulerAngles.z);

        // Add an AudioSource component to play sound if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = doorSound;

        // Get the door's collider component
        doorCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // Check for player input to open the door
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        // Play sound if available
        if (doorSound && audioSource)
        {
            audioSource.Play();
        }

        // Disable collider while rotating to avoid unwanted collisions
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // Smoothly rotate the door to the target open rotation
        float elapsedTime = 0f;
        while (elapsedTime < openSpeed)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = true;

        // Re-enable the collider
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }

        // Wait for a set delay before closing the door
        yield return new WaitForSeconds(closeDelay);

        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        // Disable collider while rotating back to avoid unwanted collisions
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // Smoothly rotate the door back to its initial rotation
        float elapsedTime = 0f;
        while (elapsedTime < openSpeed)
        {
            transform.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation;
        isOpen = false;

        // Re-enable the collider
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
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
