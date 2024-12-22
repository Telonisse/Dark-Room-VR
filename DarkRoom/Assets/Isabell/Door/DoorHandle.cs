using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void Start()
    {
        JointLimits limits = door.GetComponent<HingeJoint>().limits;
        limits.min = 0;
        limits.max = 0;
        door.GetComponent<HingeJoint>().limits = limits;
    }

    private void Update()
    {
        //Debug.Log(transform.localRotation.eulerAngles.x);
        if (transform.localRotation.eulerAngles.x <= 325 && transform.localRotation.eulerAngles.x >= 20 && door.GetComponentInChildren<Keyhole>().Unlocked() == true)
        {
            Debug.Log("Turning door handle");
            JointLimits limits = door.GetComponent<HingeJoint>().limits;
            limits.min = -120;
            limits.max = 80;
            door.GetComponent<HingeJoint>().limits = limits;
            //maybe no need?
            Destroy(this.GetComponent<HingeJoint>());
            Destroy(this.GetComponent<Rigidbody>());
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
