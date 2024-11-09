using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Will Auto Add Character Controller To Gameobject If It's Not Already Applied:
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Camera:
    public Camera playerCam;

    // Movement Settings:
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpPower = 0f;
    public float gravity = 10f;

    // Camera Settings:
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;
    public float cameraRotationSmooth = 5f;

    // Ground Sounds:
    public AudioClip[] woodFootstepSounds;
    public AudioClip[] tileFootstepSounds;
    public AudioClip[] carpetFootstepSounds;
    public Transform footstepAudioPosition;
    public AudioSource audioSource;

    private bool isWalking = false;
    private bool isFootstepCoroutineRunning = false;
    private AudioClip[] currentFootstepSounds;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float rotationY = 0;

    // Camera Zoom Settings:
    public int ZoomFOV = 35;
    public float initialFOV;
    public float cameraZoomSmooth = 1;

    private bool isZoomed = false;

    // Can The Player Move?:
    private bool canMove = true;

    CharacterController characterController;

    private void Start()
    {
        // Ensure We Are Using The Character Controller Component:
        characterController = GetComponent<CharacterController>();

        // Lock And Hide Cursor:
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize current footstep sounds to wood sounds by default
        currentFootstepSounds = woodFootstepSounds;

        // Set initial FOV based on current camera FOV
        initialFOV = playerCam.fieldOfView;
    }

    void Update()
    {
        // Walking/Running In Action:
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jumping In Action:
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Camera Movement In Action:

        if (canMove)
       {
            // Capture mouse movement for both axes
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // Horizontal rotation (side-to-side) - applied to the player body
            rotationY += mouseX;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

            // Vertical rotation (up-and-down) - applied only to the camera
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // Clamp to avoid over-rotation
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
       }



        // Zooming In Action:
        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = false;
        }

        if (isZoomed)
        {
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, ZoomFOV, Time.deltaTime * cameraZoomSmooth);
        }
        else
        {
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, initialFOV, Time.deltaTime * cameraZoomSmooth);
        }

        // Play footstep sounds when walking
        if ((curSpeedX != 0f || curSpeedY != 0f) && !isWalking && !isFootstepCoroutineRunning)
        {
            isWalking = true;
            StartCoroutine(PlayFootstepSounds(1.3f / (isRunning ? runSpeed : walkSpeed)));
        }
        else if (curSpeedX == 0f && curSpeedY == 0f)
        {
            isWalking = false;
        }
    }

    // Play footstep sounds with a delay based on movement speed
    IEnumerator PlayFootstepSounds(float footstepDelay)
    {
        isFootstepCoroutineRunning = true;

        while (isWalking)
        {
            if (currentFootstepSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, currentFootstepSounds.Length);
                audioSource.transform.position = footstepAudioPosition.position;
                audioSource.clip = currentFootstepSounds[randomIndex];
                audioSource.Play();
                yield return new WaitForSeconds(footstepDelay);
            }
            else
            {
                yield break;
            }
        }

        isFootstepCoroutineRunning = false;
    }

    // Detect ground surface and set the current footstep sounds array accordingly
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            currentFootstepSounds = woodFootstepSounds;
        }
        else if (other.CompareTag("Tile"))
        {
            currentFootstepSounds = tileFootstepSounds;
        }
        else if (other.CompareTag("Carpet"))
        {
            currentFootstepSounds = carpetFootstepSounds;
        }
    }
}
