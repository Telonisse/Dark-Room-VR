using UnityEngine;

public class LampBad : MonoBehaviour
{
    private Quaternion startRot;

    [SerializeField] AudioSource sound;

    private Quaternion lastRotation;
    private bool audioPlayed = false;
    void Start()
    {
        startRot = transform.rotation;
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.DeltaAngle(startRot.eulerAngles.y, transform.rotation.eulerAngles.y) <= -40)
        {
            Debug.Log("remove");
            Destroy(this.GetComponent<HingeJoint>());
            sound.Stop();
        }
        if (transform.rotation != lastRotation && audioPlayed == false && GetComponent<HingeJoint>() != null)
        {
            sound.Play();
            audioPlayed = true;
            Debug.Log("audio played");
        }
        else if (transform.rotation == lastRotation && audioPlayed == true)
        {
            audioPlayed= false;
        }

        lastRotation = transform.rotation;
    }
}
