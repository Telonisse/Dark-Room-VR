using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorHandle : MonoBehaviour
{
    //Animation
    //[SerializeField] Animator animator;

    //private void Start()
    //{
    //    if (animator == null)
    //    {
    //        animator = GetComponent<Animator>();
    //    }
    //}
    //public void OnHandleGrabbed()
    //{
    //    animator.SetBool("Open", true);
    //}

    //public void OnHandleReleased()
    //{
    //    animator.SetBool("Open", false);
    //}


    [SerializeField] GameObject door;
    [SerializeField] float rotateCheck;

    private void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles.x);
        if (transform.rotation.eulerAngles.x < rotateCheck && transform.rotation.eulerAngles.x > 10 && door.GetComponentInChildren<Keyhole>().Unlocked() == true)
        {
            Destroy(this.GetComponent<HingeJoint>());
            Destroy(this.GetComponent<XRGrabInteractable>());
            Destroy(this.GetComponent<Rigidbody>());
            door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            door.transform.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void Grabbbed()
    {
        this.GetComponent<HingeJoint>().useSpring = false;
    }    
    public void Released()
    {
        this.GetComponent<HingeJoint>().useSpring = true;
    }
}
