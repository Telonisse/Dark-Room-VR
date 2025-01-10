using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WindowHandle : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] float rotateCheck;

    [SerializeField] AudioSource sound;

    private void Start()
    {
        window.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        window.transform.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles.x);
        if (transform.rotation.eulerAngles.x > rotateCheck && GetComponent<HingeJoint>() != null)
        {
            Destroy(this.GetComponent<HingeJoint>());
            sound.Play();
            Destroy(this.GetComponent<XRGrabInteractable>());
            Destroy(this.GetComponent<Rigidbody>());
            window.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            window.transform.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }
}
