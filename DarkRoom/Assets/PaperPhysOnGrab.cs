using UnityEngine;

public class PaperPhysOnGrab : MonoBehaviour
{
    private Cloth cloth;
    private Rigidbody rbody;

    [Header("RigidBody options That will be effected when grabbed")]
    public float rbMass = 1.0f;
    public float rbDrag = 1.0f;
    public bool rbGravity = false;
    public bool rbKinematic = false;

    private float rbMassOrigin;
    private float rbDragOrigin;
    private bool rbGravityOrigin;
    private bool rbKinematicOrigin;

    [Header("Cloth options That will be effected when grabbed")]
    public float stretchStiff = 1.0f;
    public float bendingStiff = 1.0f;
    public bool clothGravity = false;
    public float clothDamping = 1.0f;

    private float stretchStiffOrigin;
    private float bendingStiffOrigin;
    private float clothDampingOrigin;
    private bool clothGravityOrigin;

    [Header("Call for the Functions in the grab event system")]
    public bool lol = false;

    void Start()
    {
        cloth = GetComponent<Cloth>();
        rbody = GetComponent<Rigidbody>();

        rbMassOrigin = rbody.mass;
        rbDragOrigin = rbody.angularDamping;
        rbGravityOrigin = rbody.useGravity;
        rbKinematicOrigin = rbody.isKinematic;

        stretchStiffOrigin = cloth.stretchingStiffness;
        bendingStiffOrigin = cloth.bendingStiffness;
        clothDampingOrigin = cloth.damping;
        clothGravityOrigin = cloth.useGravity;
    }

    public void SetRigidBodySettings()
    {
        if (rbody != null)
        {
            rbody.mass = rbMass;
            rbody.angularDamping = rbDrag;
            rbody.useGravity = rbGravity;
            rbody.isKinematic = rbKinematic;
        }
    }

    public void SetClothSettings()
    {
        if (cloth != null)
        {
            cloth.stretchingStiffness = stretchStiff;
            cloth.bendingStiffness = bendingStiff;
            cloth.damping = clothDamping;
            cloth.useGravity = clothGravity;
        }
    }

    public void SetSettings()
    {
        SetRigidBodySettings();
        SetClothSettings();
    }

    public void ResetRigidBody()
    {
        if (rbody != null)
        {
            rbody.mass = rbMassOrigin;
            rbody.angularDamping = rbDragOrigin;
            rbody.useGravity = rbGravityOrigin;
            rbody.isKinematic = rbKinematicOrigin;
        }
    }

    public void ResetCloth()
    {
        if (cloth != null)
        {
            cloth.stretchingStiffness = stretchStiffOrigin;
            cloth.bendingStiffness = bendingStiffOrigin;
            cloth.damping = clothDampingOrigin;
            cloth.useGravity = clothGravityOrigin;
        }
    }

    public void ResetSettings()
    {
        ResetRigidBody();
        ResetCloth();
    }
}
