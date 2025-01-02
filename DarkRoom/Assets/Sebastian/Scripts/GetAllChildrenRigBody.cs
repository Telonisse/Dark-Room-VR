using UnityEngine;

public class GetAllChildrenRigBody : MonoBehaviour
{
    [SerializeField]
    public GameObject[] children;

    void Start()
    {
        GetChildrenRigidBodys();
    }

    void GetChildrenRigidBodys()
    {        
        Rigidbody[] childRigidBodies = GetComponentsInChildren<Rigidbody>();

        children = new GameObject[childRigidBodies.Length];

        for (int i = 0; i < childRigidBodies.Length; i++)
        {
            children[i] = childRigidBodies[i].gameObject;
        }
    }

    public void ToggleChildrenRBSettings(int typeToToggle, bool state)
    {
        foreach (GameObject child in children)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                switch (typeToToggle)
                {
                    case 0: 
                        rb.isKinematic = state;
                        break;
                    case 1: 
                        rb.useGravity = state;
                        break;
                    // Just add more cases as needed for other properties
                    default:
                        Debug.LogWarning($"Unsupported typeToToggle: {typeToToggle}");
                        break;
                }
            }
        }
    }
}

