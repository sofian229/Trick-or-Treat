using UnityEngine;
using System.Collections; // This line is needed for IEnumerator

public class RestorePlayerPosition : MonoBehaviour
{
    private bool positionRestored = false; // Track if the position has been restored

    void Start()
    {
        // Start a coroutine to restore position after the scene has fully loaded
        StartCoroutine(RestorePositionAfterLoad());
    }

    private IEnumerator RestorePositionAfterLoad()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure scene objects are loaded

        // Check if GameManager is available and puzzle1Completed is true
        if (KeyEnabler.instance != null && KeyEnabler.instance.puzzle1Completed)
        {
            Vector3 targetPosition = KeyEnabler.instance.playerPosition;
            Quaternion targetRotation = KeyEnabler.instance.playerRotation;

            // Add a small offset on the Y-axis to ensure above-ground spawn
            targetPosition.y += 0.5f;

            // Raycast downward from a slightly higher position to find the floor level
            RaycastHit hit;
            if (Physics.Raycast(targetPosition, Vector3.down, out hit, 2f))
            {
                targetPosition.y = hit.point.y + 0.1f; // Set to just above the floor level
            }

            // Apply the adjusted position and rotation
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            positionRestored = true; // Mark position as restored
        }
    }
}
