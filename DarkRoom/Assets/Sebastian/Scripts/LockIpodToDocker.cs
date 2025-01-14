using System.Collections.Generic;
using UnityEngine;

public class LockIpodToDocker : MonoBehaviour
{
    public GameObject objectToLockOnTo;

    public Vector3 lockToPlaceOffset;
    public Quaternion lockToPlaceRotation;
    
    public bool isDocked { get; private set; }
    private Transform transformToLockTo;
    private Collider trigger;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transformToLockTo = objectToLockOnTo.GetComponent<Transform>();
        trigger = transformToLockTo.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (trigger != null)
        {
            if (!isDocked)
            {
                if (other == trigger)
                {
                    transform.position = objectToLockOnTo.transform.position + lockToPlaceOffset;
                    transform.rotation = lockToPlaceRotation;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    isDocked = true;
                    GetComponentInChildren<PlayMusicIpod>().enabled = true;                       
                }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (trigger != null)
        {
            if (isDocked)
            {
                if (other == trigger)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    isDocked = false;
                    GetComponentInChildren<PlayMusicIpod>().enabled = false;
                }
            }
        }

    }


}
