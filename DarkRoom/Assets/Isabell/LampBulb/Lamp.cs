using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Lamp : MonoBehaviour
{
    [SerializeField] Light lightInside;
    [SerializeField] GameObject attach;

    public Quaternion rotation;
    public Quaternion currentRotation;
    private bool screwedIn = false;

    [SerializeField] AudioSource sound;
    private Quaternion lastRotation;
    private bool audioPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LampBulb")
        {
            rotation = other.transform.rotation;
            lastRotation = other.transform.rotation;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LampBulb")
        {
            currentRotation = other.transform.rotation;
            if (Mathf.DeltaAngle(other.transform.rotation.eulerAngles.y, rotation.eulerAngles.y) <= -40 && screwedIn == false)
            {
                Debug.Log("Light bulb screwed in");
                sound.Stop();
                //turn on light
                lightInside.enabled = true;
                other.transform.parent.GetComponentInChildren<Light>().enabled = true;
                //bulb stay
                other.transform.parent.GetComponent<XRGrabInteractable>().enabled = false;
                other.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.parent.position = attach.transform.position;
                other.transform.parent.rotation = Quaternion.Euler(-40, 0, 0);
                screwedIn = true;
            }

            if (other.transform.rotation != lastRotation && audioPlayed == false && screwedIn == false)
            {
                sound.Play();
                audioPlayed = true;
                Debug.Log("audio played");
            }
            else if (other.transform.rotation == lastRotation && audioPlayed == true)
            {
                audioPlayed = false;
            }
            lastRotation = other.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LampBulb")
        {
            sound.Stop();
        }
    }
}
