using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusicIpod : MonoBehaviour
{
    public List<AudioResource> audioResources;
    public float volume = 1f;
    
    private AudioSource audioSource;
    private bool onOff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioResources = new List<AudioResource>();
        audioSource.volume = volume;
        onOff = true;
}


    public void OnOffMusic()
    {
        if (onOff)
        { 
            audioSource.Play();
            onOff = !onOff;
        }
        if (!onOff)
        {
            audioSource.Stop();
            onOff = !onOff;
        }
    }

}
