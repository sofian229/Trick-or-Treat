using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnabler : MonoBehaviour
{
    public static KeyEnabler instance;

    public bool puzzle1Completed = false;

    // Store player position and rotation
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    private void Awake()
    {
        // Ensure that only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Makes this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any additional GameManager instances
        }
    }
}
