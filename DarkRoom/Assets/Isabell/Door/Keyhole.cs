using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Keyhole : MonoBehaviour
{
    [SerializeField] string keyTag = "Key";
    [SerializeField] bool unlocked = false;
    [SerializeField] GameObject door;

    [SerializeField] AudioSource keyInsert;
    [SerializeField] AudioSource keyTurn;

    private Quaternion rotation;

    private bool audioPlayed = false;

    private void Start()
    {
        //door no move
        door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        door.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == keyTag)
        {
            rotation = other.transform.rotation;
            keyInsert.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == keyTag)
        {
            if (Mathf.DeltaAngle(other.transform.rotation.eulerAngles.z, rotation.eulerAngles.z) <= -5 && audioPlayed == false)
            {
                audioPlayed = true;
                keyTurn.Play();
            }
            if (Mathf.DeltaAngle(other.transform.rotation.eulerAngles.z , rotation.eulerAngles.z) <= -60)
            {
                unlocked = true;
            }
            if (unlocked == true)
            {
                other.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.GetComponent<XRGrabInteractable>());
                Destroy(other.GetComponent<Rigidbody>());
                other.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioPlayed = false;
    }

    private void Update()
    {
        if (unlocked == true)
        {
            Debug.Log("Unlocking");
        }
    }

    public bool Unlocked()
    {
        return unlocked;
    }
}
