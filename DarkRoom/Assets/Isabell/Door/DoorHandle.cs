using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void OnHandleGrabbed()
    {
        animator.SetBool("Open", true);
    }

    public void OnHandleReleased()
    {
        animator.SetBool("Open", false);
    }
    //[SerializeField] GameObject door;

    //private void Start()
    //{
    //    JointLimits limits = door.GetComponent<HingeJoint>().limits;
    //    limits.min = 0;
    //    limits.max = 0;
    //    door.GetComponent<HingeJoint>().limits = limits;
    //}

    //private void Update()
    //{
    //    if (transform.localRotation.eulerAngles.x <= 325 && transform.localRotation.eulerAngles.x >= 20 && door.GetComponentInChildren<Keyhole>().Unlocked() == true)
    //    {
    //        Debug.Log("Turning door handle");
    //        JointLimits limits = door.GetComponent<HingeJoint>().limits;
    //        limits.min = -120;
    //        limits.max = 80;
    //        door.GetComponent<HingeJoint>().limits = limits;
    //        //maybe no need?
    //        Destroy(this.GetComponent<HingeJoint>());
    //        Destroy(this.GetComponent<Rigidbody>());
    //        Destroy(this.GetComponent<XRGrabInteractable>());
    //        transform.localRotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}
}
