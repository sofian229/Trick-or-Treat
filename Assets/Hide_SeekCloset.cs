using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public Transform closetHidePosition; // Position inside the closet for hiding
    public GameObject player; // Reference to the player
    public KeyCode exitKey = KeyCode.E; // Key to exit the closet

    private bool isHiding = false;
    private Vector3 originalPosition; // Store player's original position

    void Start()
    {
        // Cache the player's original position for when they exit
        originalPosition = player.transform.position;
    }

    void OnMouseDown()
    {
        // Trigger hide mechanic if player is not already hiding
        if (!isHiding)
        {
            Hide();
        }
        else
        {
            ExitCloset();
        }
    }

    void Update()
    {
        // Check for exit key if the player is hiding
        if (isHiding && Input.GetKeyDown(exitKey))
        {
            ExitCloset();
        }
    }

    void Hide()
    {
        isHiding = true;
        player.transform.position = closetHidePosition.position; // Move player to hide position

        // Set the player's rotation to match the hiding position's rotation
        player.transform.rotation = closetHidePosition.rotation;

        player.GetComponent<PlayerController>().enabled = false; // Disable player movement
        Debug.Log("You are now hiding.");
    }


    void ExitCloset()
    {
        isHiding = false;
        player.transform.position = originalPosition; // Return player to original position
        player.GetComponent<PlayerController>().enabled = true; // Enable player movement
        Debug.Log("You have exited the closet.");
    }
}
