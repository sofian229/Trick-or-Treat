using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public AudioClip roomMusic;                   // Music to play in this room
    private MusicTransitionManager musicManager;  // Reference to the music manager

    [Header("Audio Adjustments")]
    [Range(0f, 1f)] public float roomVolume = 0.8f; // Volume for this room
    [Range(0.1f, 3f)] public float roomPitch = 1f;  // Pitch for this room

    private void Start()
    {
        // Find the MusicTransitionManager in the scene
        musicManager = FindObjectOfType<MusicTransitionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger zone
        if (other.CompareTag("Player") && musicManager != null)
        {
            // Set room-specific volume and pitch
            musicManager.maxVolume = roomVolume;
            musicManager.pitch = roomPitch;

            // Play the room-specific music
            musicManager.PlayMusic(roomMusic);
        }
    }
}
