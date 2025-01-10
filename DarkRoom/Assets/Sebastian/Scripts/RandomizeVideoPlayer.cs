using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomizeVideoPlayer : MonoBehaviour
{
    [Tooltip("List of Videos that will be played at random")]
    [SerializeField]
    private List<VideoClip> listOfVideos;

    [Tooltip("If left empty, it takes this object's Video Player")]
    [SerializeField]
    private VideoPlayer videoplay; 

    [Tooltip("If left empty, it takes this object's AudioSource")]
    [SerializeField]
    private AudioSource videoSoundSource;

    [Tooltip("The turned-off screen")]
    [SerializeField]
    private GameObject blackScreen;

    private int videoindex = -1;

    void Start()
    {
        // Initialize VideoPlayer and AudioSource
        if (videoplay == null)
        {
            videoplay = gameObject.AddComponent<VideoPlayer>();
        }

        if (videoSoundSource == null)
        {
            videoSoundSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure VideoPlayer audio output
        videoplay.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoplay.SetTargetAudioSource(0, videoSoundSource);

        // Add the loopPointReached event listener
        videoplay.loopPointReached += OnVideoStopped;

        // Play the first video if there are videos in the list
        if (listOfVideos != null && listOfVideos.Count > 0)
        {
            RandomVideoToPlayer();
        }
    }

    public void RandomVideoToPlayer()
    {
        if (listOfVideos == null || listOfVideos.Count == 0)
        {
            Debug.LogWarning("List of videos is empty or null.");
            return;
        }

        int previousIndex = videoindex;

        // Generate a new random index that is different from the current one
        do
        {
            videoindex = UnityEngine.Random.Range(0, listOfVideos.Count);
        } while (videoindex == previousIndex);

        SetVideoToPlayer(videoindex);
    }

    private void SetVideoToPlayer(int index)
    {
        // Assign the new video clip and play
        videoplay.clip = listOfVideos[index];

        videoplay.Play();

        // Ensure the black screen is turned off
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }

        // Reactivate the video player in case it was deactivated
        videoplay.gameObject.SetActive(true);
    }

    private void OnVideoStopped(VideoPlayer vp)
    {
        // Handle video playback completion
        TurnOffTv();
    }

    private void TurnOffTv()
    {
        // Turn off the video player and activate the black screen
        if (videoplay != null)
        {
            videoplay.gameObject.SetActive(false);
        }

        if (blackScreen != null)
        {
            blackScreen.SetActive(true);
        }

        // Randomize a new video for the next play
        RandomVideoToPlayer();
    }

    private void OnDestroy()
    {
        // Remove the event listener to avoid memory leaks
        if (videoplay != null)
        {
            videoplay.loopPointReached -= OnVideoStopped;
        }
    }
}
