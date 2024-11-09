using UnityEngine;

public class RotateOnPuzzleComplete : MonoBehaviour
{
    public float rotationAngle = 60f; // The angle to rotate on the Y-axis
    private bool hasRotated = false;

    void Update()
    {
        // Check if the puzzle is completed and if the object hasn't already rotated
        if (KeyEnabler.instance != null && KeyEnabler.instance.puzzle1Completed && !hasRotated)
        {
            // Rotate the object by 60 degrees on the Y-axis
            transform.Rotate(0, rotationAngle, 0);
            hasRotated = true; // Prevent further rotations
        }
    }
}
