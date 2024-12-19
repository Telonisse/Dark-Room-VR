using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] GameObject door;

    private void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles.z);
        if (transform.rotation.eulerAngles.z >= 210)
        {
            Debug.Log("Turning door handle");
            JointLimits limits = door.GetComponent<HingeJoint>().limits;
            limits.min = -120;
            limits.max = 120;
            door.GetComponent<HingeJoint>().limits = limits;
        }
        else
        {
            JointLimits limits = door.GetComponent<HingeJoint>().limits;
            limits.min = 0;
            limits.max = 0;
            door.GetComponent<HingeJoint>().limits = limits;
        }
    }
}
