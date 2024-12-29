using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Keyhole : MonoBehaviour
{
    [SerializeField] string keyTag = "Key";
    [SerializeField] bool unlocked = false;
    [SerializeField] GameObject door;

    private Quaternion rotation;

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
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == keyTag)
        {
            if (Mathf.DeltaAngle(other.transform.rotation.eulerAngles.z , rotation.eulerAngles.z) <= -60)
            {
                //transform.rotation = other.transform.rotation;
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

    private void Update()
    {
        if (unlocked == true)
        {
            Debug.Log("Unlocking");
            //door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //door.transform.GetComponent<XRGrabInteractable>().enabled = true;
            //GetComponent<XRSocketInteractor>().enabled = true;
        }
    }

    public bool Unlocked()
    {
        return unlocked;
    }
}
