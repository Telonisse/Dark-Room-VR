using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Keyhole : MonoBehaviour
{
    [SerializeField] string keyTag = "Key";
    [SerializeField] bool unlocked = false;
    [SerializeField] GameObject door;

    public Quaternion rotation;

    private void Start()
    {
        //door no move
        door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        door.transform.GetComponent<XRGrabInteractable>().enabled = false;
        GetComponent<XRSocketInteractor>().enabled = false;
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
            Debug.Log(rotation.eulerAngles.z + 45);
            if (other.transform.rotation.eulerAngles.z >= rotation.eulerAngles.z + 90)
            {
                transform.rotation = other.transform.rotation;
                unlocked = true;
            }
        }
    }

    private void Update()
    {
        if (unlocked == true && door.transform.GetComponent<XRGrabInteractable>().enabled == false)
        {
            Debug.Log("Unlocking");
            door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            door.transform.GetComponent<XRGrabInteractable>().enabled = true;
            GetComponent<XRSocketInteractor>().enabled = true;
        }
    }
}
