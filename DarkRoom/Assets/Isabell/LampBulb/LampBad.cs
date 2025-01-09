using UnityEngine;

public class LampBad : MonoBehaviour
{
    public Quaternion startRot;
    void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.DeltaAngle(startRot.y, transform.rotation.y));
        if (Mathf.DeltaAngle(startRot.eulerAngles.y, transform.rotation.eulerAngles.y) <= -40)
        {
            Debug.Log("remove");
            Destroy(this.GetComponent<HingeJoint>());
        }
    }
}
