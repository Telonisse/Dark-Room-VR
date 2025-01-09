using System.Collections.Generic;
using UnityEngine;

public class WireRoot : MonoBehaviour
{
    public string nameOfChildrenToEffect;
    public float RigidbodyMass = 1f;
    public float ColliderRadius = 0.1f;
    public float JointSpring = 0.1f;
    public float JointDamper = 5f;
    public Vector3 RotationOffset;
    public Vector3 PositionOffset;

    protected List<Transform> CopySource;
    protected List<Transform> CopyDestination;
    protected static GameObject RigidBodyContainer;

    void Awake()
    {
        if (RigidBodyContainer == null)
            RigidBodyContainer = new GameObject("RopeRigidbodyContainer");

        CopySource = new List<Transform>();
        CopyDestination = new List<Transform>();

        //add children
        AddChildren(transform);
    }

    private void AddChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (child != null && child.name.Contains(nameOfChildrenToEffect))
            {
                var representative = new GameObject(child.gameObject.name);
                representative.transform.parent = RigidBodyContainer.transform;
                //rigidbody
                var childRigidbody = representative.gameObject.AddComponent<Rigidbody>();
                childRigidbody.useGravity = true;
                childRigidbody.isKinematic = false;
                childRigidbody.freezeRotation = true;
                childRigidbody.mass = RigidbodyMass;

                //collider
                var collider = representative.gameObject.AddComponent<SphereCollider>();
                collider.center = Vector3.zero;
                collider.radius = ColliderRadius;

                //DistanceJoint
                var joint = representative.gameObject.AddComponent<DistanceJoint3D>();
                joint.ConnectedRigidbody = parent;
                joint.DetermineDistanceOnStart = true;
                joint.Spring = JointSpring;
                joint.Damper = JointDamper;
                joint.DetermineDistanceOnStart = false;
                joint.Distance = Vector3.Distance(parent.position, child.position);

                //add copy source
                CopySource.Add(representative.transform);
                CopyDestination.Add(child);

                AddChildren(child);
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < CopySource.Count; i++)
        {
            CopyDestination[i].position = CopySource[i].position + PositionOffset;
            CopyDestination[i].rotation = CopySource[i].rotation * Quaternion.Euler(RotationOffset);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            FreezeRigidBodyChildren(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            FreezeRigidBodyChildren(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FreezeRigidBodyChildren(true);
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FreezeRigidBodyChildren(false);
        }
    }
    void FreezeRigidBodyChildren(bool onOff)
    {
        if (onOff)
        {            
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child != null && child.name.Contains(nameOfChildrenToEffect))
                {
                    var representative = new GameObject(child.gameObject.name);
                    representative.transform.parent = RigidBodyContainer.transform;
                    //rigidbody
                    var childRigidbody = representative.gameObject.AddComponent<Rigidbody>();

                    childRigidbody.useGravity = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child != null && child.name.Contains(nameOfChildrenToEffect))
                {
                    var representative = new GameObject(child.gameObject.name);
                    representative.transform.parent = RigidBodyContainer.transform;
                    //rigidbody
                    var childRigidbody = representative.gameObject.AddComponent<Rigidbody>();

                    childRigidbody.useGravity = false;
                }
            }
        }
    }
                 
       
}
