using UnityEngine;

[RequireComponent(typeof(Cloth))]
public class PaperPhysics : MonoBehaviour
{
    private Cloth cloth;
    private Rigidbody rb;

    [Header("Air Physics Settings")]
    public float detectionRadius = 0.5f; // Radius for detecting surfaces
    public LayerMask groundLayer; // Layer for ground detection
    private bool isInAir;

    [Header("Forces to Simulate Paper Behavior")]
    public float downForce = 1f;    // Force to simulate gravity/drag
    public float rightForce = 0.5f; // Horizontal force to simulate wind
    public float forwardForce = 0.5f; // Forward force for motion


    private bool isPined;    
    private void Awake()
    {                
        cloth = GetComponent<Cloth>();
        rb = GetComponent<Rigidbody>();

        if (cloth == null)
        {
            Debug.LogError("No Cloth component found on this GameObject.");
        }

        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on this GameObject.");
        }
    }

    private void Update()
    {
        isPined = this.gameObject.GetComponentInParent<Rigidbody>();
        if (!isPined)
        {
            CheckIfInAir();
        }
        if (isInAir)
        {
            ApplyPaperForces();
        }
    }

    private void CheckIfInAir()
    {
        // Check if the paper is near the ground or surfaces
        isInAir = !Physics.CheckSphere(transform.position, detectionRadius, groundLayer);
    }

    private void ApplyPaperForces()
    {
        // Apply the forces
        Vector3 customForce = Vector3.down * downForce +
                              Vector3.right * rightForce +
                              Vector3.forward * forwardForce;

        rb.AddForce(customForce, ForceMode.Force);
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection sphere
        Gizmos.color = isInAir ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
