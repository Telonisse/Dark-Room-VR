using System.Collections.Generic;
using UnityEngine;

public class PlayMusicIpod : MonoBehaviour
{
    [Tooltip("List of audio clips to play randomly")]
    public List<AudioClip> audioResources;

    [Tooltip("Volume of the audio player")]
    public float volume = 1f;

    private AudioSource audioSource;
    private bool isPlaying = false;
    private int musicIndex = -1;

    void Start()
    {
        // Initialize the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio source volume
        audioSource.volume = volume;
    }

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            PlayRandomSong();
        }

        isPlaying = !isPlaying;
    }

    public void PlayRandomSong()
    {
        if (audioResources == null || audioResources.Count == 0)
        {
            Debug.LogWarning("Audio resources list is empty or null.");
            return;
        }

        int previousIndex = musicIndex;

        // Generate a new random index that is different from the current one
        do
        {
            musicIndex = Random.Range(0, audioResources.Count);
        } while (musicIndex == previousIndex);

        SetSongToPlayer(musicIndex);
    }

    private void SetSongToPlayer(int index)
    {
        // Assign the selected audio clip and play
        audioSource.clip = audioResources[index];
        audioSource.Play();
        isPlaying = true;
    }

    private void OnDestroy()
    {
        // Ensure the audio source stops playing when the object is destroyed
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
