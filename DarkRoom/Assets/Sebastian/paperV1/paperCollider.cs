using System.Collections.Generic;
using UnityEngine;

public class AddCapsuleCollidersToCloth : MonoBehaviour
{
    // Reference to the GameObject from which we will add colliders
    public GameObject collidersSource;

    // Reference to the Cloth component
    private Cloth cloth;

    void Start()
    {
        // Get the Cloth component on this GameObject
        cloth = GetComponent<Cloth>();

        if (cloth == null)
        {
            Debug.LogError("No Cloth component found on this object.");
            return;
        }

        if (collidersSource == null)
        {
            Debug.LogError("No source GameObject for CapsuleColliders assigned.");
            return;
        }

        // Create a list to hold the ClothSphereColliderPair objects
        var clothColliders = new List<ClothSphereColliderPair>();

        // Find all CapsuleColliders in the source GameObject and its children
        CapsuleCollider[] capsuleColliders = collidersSource.GetComponentsInChildren<CapsuleCollider>();

        // Loop through all CapsuleColliders and add them as pairs
        foreach (var capsule in capsuleColliders)
        {
            ApproximateCapsuleCollider(capsule, clothColliders);
        }

        // Assign the colliders to the cloth
        cloth.sphereColliders = clothColliders.ToArray();
    }

    private void ApproximateCapsuleCollider(CapsuleCollider capsule, List<ClothSphereColliderPair> clothColliders)
    {
        // Get the capsule's endpoints in world space
        Vector3 capsuleCenter = capsule.transform.TransformPoint(capsule.center);
        Vector3 upDirection = capsule.transform.up; // Capsule's local up direction
        float halfHeight = (capsule.height * 0.5f) - capsule.radius;

        Vector3 start = capsuleCenter + upDirection * halfHeight;
        Vector3 end = capsuleCenter - upDirection * halfHeight;

        // Create two SphereCollider approximations at the start and end points
        SphereCollider sphere1 = CreateVirtualSphere(start, capsule.radius);
        SphereCollider sphere2 = CreateVirtualSphere(end, capsule.radius);

        // Add the pair to the cloth colliders list
        clothColliders.Add(new ClothSphereColliderPair(sphere1, sphere2));
    }

    private SphereCollider CreateVirtualSphere(Vector3 position, float radius)
    {
        // Create a virtual SphereCollider
        GameObject tempObject = new GameObject("VirtualSphereCollider");
        SphereCollider sphere = tempObject.AddComponent<SphereCollider>();
        sphere.transform.position = position;
        sphere.radius = radius;

        // Hide the object in the scene (optional)
        tempObject.hideFlags = HideFlags.HideAndDontSave;

        return sphere;
    }
}
