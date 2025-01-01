using System.Collections.Generic;
using UnityEngine;

public class PinFreezeObjRigid : MonoBehaviour
{
    [Tooltip("Tag name of Object that have a rigidbody that will be affected")]
    [SerializeField]
    public string tagName;    

    private List<GameObject> objectThatIsPined = new List<GameObject>();

    [Header("------------------------------------------" + "\n-----RigidBody options when Pined-----" + "\n------------------------------------------")]

    [Tooltip("Freezes Rotational X")]
    [SerializeField]
    public bool xRotFreeze = false;
    [Tooltip("Freezes Rotational Z")]
    [SerializeField]
    public bool zRotFreeze = false;
    [Tooltip("Freezes Rotational Y")]
    [SerializeField]
    public bool yRotFreeze = false;

    [Tooltip("Freezes positional X")]
    [SerializeField]
    public bool xPositional = false;
    [Tooltip("Freezes positional Z")]
    [SerializeField]
    public bool zPositional = false;
    [Tooltip("Freezes positional Y")]
    [SerializeField]
    public bool yPositional = false;

    [Tooltip("Toggles 'Is Kinematic'")]
    [SerializeField]
    public bool isKinematic = false;
    [Tooltip("Toggles 'Use Gravity'")]
    [SerializeField]
    public bool useGravity = false;

    private void Start()
    {
        // Clear the list and add all GameObjects with the specified tag
        objectThatIsPined.Clear();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);
        objectThatIsPined.AddRange(taggedObjects);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject obj in objectThatIsPined)
        {
            PinFreezeObject(collision.gameObject);
        }
    }

    private void OnCollisionExit (Collision collision)
    {      
        foreach (GameObject obj in objectThatIsPined)
        {  
          UnfreezeObject(collision.gameObject);                      
        }
    }
    
    void PinFreezeObject(GameObject obj)
    { 
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        ApplyConstraints(rb, true);
    }

    void UnfreezeObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ApplyConstraints(rb, false);
    }

    void ApplyConstraints(Rigidbody rb, bool freeze)
    {
        if (freeze)
        {
            //rb.constraints = RigidbodyConstraints.None;

            if (xPositional) rb.constraints |= RigidbodyConstraints.FreezePositionX;
            if (yPositional) rb.constraints |= RigidbodyConstraints.FreezePositionY;
            if (zPositional) rb.constraints |= RigidbodyConstraints.FreezePositionZ;

            if (xRotFreeze) rb.constraints |= RigidbodyConstraints.FreezeRotationX;
            if (yRotFreeze) rb.constraints |= RigidbodyConstraints.FreezeRotationY;
            if (zRotFreeze) rb.constraints |= RigidbodyConstraints.FreezeRotationZ;

            rb.isKinematic = isKinematic;
            rb.useGravity = useGravity;
        }
        else
        {           
            rb.constraints = RigidbodyConstraints.None;

            rb.isKinematic = false;
            rb.useGravity = true;
        }
        //DebugMsg(rb);
    }
    void DebugMsg(Rigidbody rb)
    {
        Debug.Log($"Rigidbody Constraints: {rb.constraints}, IsKinematic: {rb.isKinematic}, UseGravity: {rb.useGravity}");
    }
}
