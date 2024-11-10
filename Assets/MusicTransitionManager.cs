using System.Collections;
using UnityEngine;

public class MusicTransitionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;          // The main audio source
    public float fadeDuration = 1.5f;        // Duration of the fade effect
    public AudioClip defaultMusic;           // Default background music

    [Range(0f, 1f)] public float maxVolume = 0.8f;  // Maximum volume for the audio source
    [Range(0.1f, 3f)] public float pitch = 1f;      // Pitch for the audio source

    private Coroutine fadeCoroutine;         // To keep track of ongoing fade coroutine

    private void Start()
    {
        // Set initial volume and pitch
        audioSource.volume = maxVolume;
        audioSource.pitch = pitch;

        // Play default music at the start
        if (defaultMusic != null)
        {
            PlayMusic(defaultMusic);
        }
    }

    // Method to play specific music
    public void PlayMusic(AudioClip newClip)
    {
        // Stop any existing fade effect
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Start fading to the new music
        fadeCoroutine = StartCoroutine(FadeMusic(newClip));
    }

    // Coroutine to smoothly fade between music tracks
    private IEnumerator FadeMusic(AudioClip newClip)
    {
        // If there's music playing, fade it out
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        // Change the clip and set volume and pitch
        audioSource.clip = newClip;
        audioSource.pitch = pitch;
        audioSource.Play();
        audioSource.volume = 0;

        // Fade in the new music
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = maxVolume;
    }
}
