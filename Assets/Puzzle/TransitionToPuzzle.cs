using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToPuzzle : MonoBehaviour
{
    public void SaveAndGoToPuzzleScene()
    {
        // Check if GameManager is available
        if (KeyEnabler.instance != null)
        {
            // Save player position and rotation in GameManager
            KeyEnabler.instance.playerPosition = transform.position;
            KeyEnabler.instance.playerRotation = transform.rotation;
        }

        // Load the puzzle scene
        SceneManager.LoadScene("PuzzleScene");
    }
}
