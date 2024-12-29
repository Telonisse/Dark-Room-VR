using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomizeVideoPlayer : MonoBehaviour
{
    [Tooltip("List of Videos that will be played at random")]
    [SerializeField]
    public List<VideoClip> listOfVideos;
            
    [Tooltip("If left empty, it takes this objects Video player")]
    [SerializeField]
    public VideoPlayer videoplay;        

    private int videoindex = 0;
  
    void Start()
    {
        if (videoplay == null) { videoplay = gameObject.AddComponent<VideoPlayer>(); }        
    }

    public void RandomVideoToPlayer()
    {
        if (listOfVideos == null || listOfVideos.Count == 0)
        {  Debug.LogWarning("List of videos is empty or null.");  return;  }

        if (listOfVideos.Count == 1) {  videoindex = 0; }
        else
        {
            int i = videoindex;
            while (videoindex == i)
            { videoindex = UnityEngine.Random.Range(0, listOfVideos.Count); }
        }
        SetVideoToPlayer(videoindex);
    }

    private void SetVideoToPlayer(int index)
    {
        videoplay.clip = listOfVideos[index];
        videoplay.Play();
    }
}
