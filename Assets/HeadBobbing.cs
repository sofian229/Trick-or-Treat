using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobIntensity = 0.05f; // Intensity of the bobbing (height of the movement)
    public float bobSpeed = 10f;       // Speed of the bobbing (how fast the head bobs)
    private float timer = 0.0f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition; // Store the initial position of the camera
    }

    void Update()
    {
        // Check if the player is moving
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            timer += Time.deltaTime * bobSpeed;
            float newYPosition = initialPosition.y + Mathf.Sin(timer) * bobIntensity;
            transform.localPosition = new Vector3(initialPosition.x, newYPosition, initialPosition.z);
        }
        else
        {
            // Reset the position when not moving
            timer = 0.0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * bobSpeed);
        }
    }
}
