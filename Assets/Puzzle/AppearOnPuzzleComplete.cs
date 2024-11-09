using UnityEngine;

public class AppearOnPuzzleComplete : MonoBehaviour
{
    void Start()
    {
        // Initially hide the object
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Check if the puzzle is completed
        if (KeyEnabler.instance != null && KeyEnabler.instance.puzzle1Completed)
        {
            // Make the object appear
            gameObject.SetActive(true);
        }
    }
}
