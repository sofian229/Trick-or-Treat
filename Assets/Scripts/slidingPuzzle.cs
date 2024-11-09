using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slidingPuzzle : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // Player must be close to the object to interact
    public float interactionDistance = 3f;

    // Reference to the player
    private Transform player;

    void Start()
    {
        // Finding the player by tag; make sure the player GameObject is tagged as "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within interaction distance
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance)
        {
            // Display prompt or UI hint (optional)
            Debug.Log("Press 'E' to interact");

            // Check if the "E" key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Load the specified scene
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    // Visualize interaction range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
