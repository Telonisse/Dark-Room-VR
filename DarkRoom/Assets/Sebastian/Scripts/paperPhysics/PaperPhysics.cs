using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class PaperPhysics : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh sharedMesh;
    private Vector3[] originalVertices;
    private Vector3[] currentVertices;

    // Parameters for stiffness and bending resistance
    public float stiffness = 0.5f;
    public float damping = 0.1f;

    // Connections between vertices for springs
    private List<Edge> edges = new List<Edge>();

    void Start()
    {
        // Get the SkinnedMeshRenderer and its sharedMesh
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        sharedMesh = skinnedMeshRenderer.sharedMesh;

        // Get the mesh vertices
        originalVertices = sharedMesh.vertices;
        currentVertices = (Vector3[])originalVertices.Clone();

        // Initialize edges between vertices
        InitializeEdges();
    }

    void Update()
    {
        if (GetComponent<Cloth>() != null)
        {
            // Cloth physics handles deformation
            ApplyCustomClothLogic();
        }
        else
        {
            // Simulate paper physics
            SimulatePaperPhysics();

            // Update the skinned mesh
            sharedMesh.vertices = currentVertices;
            sharedMesh.RecalculateNormals();
            skinnedMeshRenderer.sharedMesh = sharedMesh;
        }
    }

    void ApplyCustomClothLogic()
    {
        // Perform custom Cloth logic if necessary
        Debug.Log("Custom Cloth deformation logic can be applied here.");
    }

    void InitializeEdges()
    {
        // Define edges based on your SkinnedMeshRenderer's vertex layout
        // Example: Connect vertices in a grid pattern
    }

    void SimulatePaperPhysics()
    {
        for (int i = 0; i < edges.Count; i++)
        {
            Edge edge = edges[i];

            // Apply spring force between connected vertices
            Vector3 force = CalculateSpringForce(edge);
            currentVertices[edge.vertexA] += force * Time.deltaTime;
            currentVertices[edge.vertexB] -= force * Time.deltaTime;

            // Apply damping to reduce oscillation
            ApplyDamping(edge);
        }
    }

    Vector3 CalculateSpringForce(Edge edge)
    {
        Vector3 posA = currentVertices[edge.vertexA];
        Vector3 posB = currentVertices[edge.vertexB];
        Vector3 direction = posB - posA;
        float distance = direction.magnitude;

        // Hooke's law: F = -k * x
        float stretch = distance - edge.restLength;
        return stiffness * stretch * direction.normalized;
    }

    void ApplyDamping(Edge edge)
    {
        // Implement damping to stabilize motion
    }

    private struct Edge
    {
        public int vertexA;
        public int vertexB;
        public float restLength;

        public Edge(int a, int b, float length)
        {
            vertexA = a;
            vertexB = b;
            restLength = length;
        }
    }
}
