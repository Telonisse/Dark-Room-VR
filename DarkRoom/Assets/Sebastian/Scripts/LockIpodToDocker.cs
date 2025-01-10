using System.Collections.Generic;
using UnityEngine;

public class LockIpodToDocker : MonoBehaviour
{
    public GameObject objectToLockOnTo;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public GameObject startStopButton;

    public Vector3 lockToPlaceOffset;
    
    private bool isDocked;
    private Transform transformToLockTo;
    private Collider trigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transformToLockTo = objectToLockOnTo.GetComponent<Transform>();
        trigger = transformToLockTo.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
