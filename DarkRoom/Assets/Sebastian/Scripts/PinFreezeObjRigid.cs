using System.Collections.Generic;
using UnityEngine;

public class PinFreezeObjRigid : MonoBehaviour
{
    [SerializeField] private string tagName;

    private List<GameObject> objectThatIsPined = new List<GameObject>();

    [Header("RigidBody Options When Pinned")]
    [SerializeField] private bool xRotFreeze;
    [SerializeField] private bool zRotFreeze;
    [SerializeField] private bool yRotFreeze;
    [SerializeField] private bool xPositional;
    [SerializeField] private bool zPositional;
    [SerializeField] private bool yPositional;
    [SerializeField] private bool isKinematic;
    [SerializeField] private bool useGravity;

    private bool isFrozen = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFrozen && collision.gameObject.CompareTag(tagName))
        {
            PinFreezeObject(collision.gameObject);
            if (!objectThatIsPined.Contains(collision.gameObject))
            {
                objectThatIsPined.Add(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isFrozen && collision.gameObject.CompareTag(tagName))
        {
            UnfreezeObject(collision.gameObject);
            objectThatIsPined.Remove(collision.gameObject);
        }
    }

    void PinFreezeObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;

        isFrozen = true;
        ApplyConstraints(rb, true);
    }

    void UnfreezeObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;

        isFrozen = false;
        ApplyConstraints(rb, false);
    }

    void ApplyConstraints(Rigidbody rb, bool freeze)
    {
        if (freeze)
        {
            RigidbodyConstraints constraints = RigidbodyConstraints.None;

            if (xPositional) constraints |= RigidbodyConstraints.FreezePositionX;
            if (yPositional) constraints |= RigidbodyConstraints.FreezePositionY;
            if (zPositional) constraints |= RigidbodyConstraints.FreezePositionZ;

            if (xRotFreeze) constraints |= RigidbodyConstraints.FreezeRotationX;
            if (yRotFreeze) constraints |= RigidbodyConstraints.FreezeRotationY;
            if (zRotFreeze) constraints |= RigidbodyConstraints.FreezeRotationZ;

            rb.constraints = constraints;

            rb.isKinematic = isKinematic;
            rb.useGravity = useGravity;
            rb.mass = 0.01f;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.mass = 1f;
        }
    }
}
