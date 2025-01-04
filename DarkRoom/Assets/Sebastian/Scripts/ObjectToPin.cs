using UnityEngine;

public class ObjectToPin : MonoBehaviour
{
    [SerializeField]
    public GameObject objectThatWillGetPin;

    private bool gotChilden;
    private bool isPinned;

    private void OnCollisionEnter(Collision collision)
    {
        if (objectThatWillGetPin != null && !isPinned)
        {
            if (collision != null && collision.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

                rb.isKinematic = true;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;

                isPinned = true;

                GetAllChildrenRigBody getAllChildren = collision.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (collision.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, true);
                    getAllChildren.ToggleChildrenRBSettings(1, false);
                }
                Debug.Log($"{objectThatWillGetPin.name} has been pinned.OnCollisionEnter");
            }

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (objectThatWillGetPin != null && isPinned)
        {
            if (collision != null && collision.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

                rb.isKinematic = false;
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;

                isPinned = false;

                GetAllChildrenRigBody getAllChildren = collision.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (collision.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, false);
                    getAllChildren.ToggleChildrenRBSettings(1, true);
                }
                Debug.Log($"{objectThatWillGetPin.name} has been unpinned.OnCollisionExit");
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == objectThatWillGetPin)
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            isPinned = true;

            GetAllChildrenRigBody getAllChildren = other.gameObject.GetComponent<GetAllChildrenRigBody>();
            if (other.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
            {
                getAllChildren.ToggleChildrenRBSettings(0, true);
                getAllChildren.ToggleChildrenRBSettings(1, false);
            }
            Debug.Log($"{objectThatWillGetPin.name} has been pinned.OnTriggerEnter");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (objectThatWillGetPin != null && isPinned)
        {
            if (other != null && other.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

                rb.isKinematic = false;
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;

                isPinned = false;

                GetAllChildrenRigBody getAllChildren = other.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (other.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, false);
                    getAllChildren.ToggleChildrenRBSettings(1, true);
                }
                Debug.Log($"{objectThatWillGetPin.name} has been unpinned.OnTriggerExit");
            }

        }
    }
}
