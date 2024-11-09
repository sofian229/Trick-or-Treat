using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;            // Movement speed
    public float mouseSensitivity = 100f;   // Mouse sensitivity for camera rotation
    public float gravity = -9.81f;          // Gravity effect

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;           // Camera vertical rotation

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock and hide the cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Handle Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Clamps vertical rotation to prevent flipping

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // Vertical camera rotation
        transform.Rotate(Vector3.up * mouseX);  // Horizontal player rotation

        // 2. Handle WASD Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;  // Movement relative to camera direction
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 3. Apply Gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;  // Reset velocity when grounded
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
