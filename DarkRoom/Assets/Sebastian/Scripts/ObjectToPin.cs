using Unity.VisualScripting;
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
                if (collision.gameObject.GetComponent<Cloth>() != null)
                { collision.gameObject.GetComponent<Cloth>().enabled = false; }
                GetAllChildrenRigBody getAllChildren = collision.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (collision.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, true);
                    getAllChildren.ToggleChildrenRBSettings(1, false);
                }
                Debug.Log($"{objectThatWillGetPin.name} has been pinned.OnCollisionEnter");
                if (collision.gameObject.GetComponent<PaperDampChange>() != null)
                {
                    PaperDampChange pdc = collision.gameObject.GetComponent<PaperDampChange>();

                    pdc.CallDampSetNew();
                }
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
                if (collision.gameObject.GetComponent<Cloth>() != null)
                { collision.gameObject.GetComponent<Cloth>().enabled = true; }

                GetAllChildrenRigBody getAllChildren = collision.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (collision.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, false);
                    getAllChildren.ToggleChildrenRBSettings(1, true);
                }
                Debug.Log($"{objectThatWillGetPin.name} has been unpinned.OnCollisionExit");
                if (collision.gameObject.GetComponent<PaperDampChange>() != null)
                {
                    PaperDampChange pdc = collision.gameObject.GetComponent<PaperDampChange>();

                    pdc.CallDampSetOrigin();
                }
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
            if (other.gameObject.GetComponent<Cloth>() != null)
            { other.gameObject.GetComponent<Cloth>().enabled = false; }
            GetAllChildrenRigBody getAllChildren = other.gameObject.GetComponent<GetAllChildrenRigBody>();
            if (other.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
            {
                getAllChildren.ToggleChildrenRBSettings(0, true);
                getAllChildren.ToggleChildrenRBSettings(1, false);
            }
            
            if (other.gameObject.GetComponent<PaperDampChange>() != null)
            {
                PaperDampChange pdc = other.gameObject.GetComponent<PaperDampChange>();

                pdc.CallDampSetNew();
            }
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
                if (other.gameObject.GetComponent<Cloth>() != null)
                { other.gameObject.GetComponent<Cloth>().enabled = true; }
                GetAllChildrenRigBody getAllChildren = other.gameObject.GetComponent<GetAllChildrenRigBody>();
                if (other.gameObject.GetComponent<GetAllChildrenRigBody>() != null)
                {
                    getAllChildren.ToggleChildrenRBSettings(0, false);
                    getAllChildren.ToggleChildrenRBSettings(1, true);
                }
                if (other.gameObject.GetComponent<PaperDampChange>() != null)
                {
                    PaperDampChange pdc = other.gameObject.GetComponent<PaperDampChange>();

                    pdc.CallDampSetOrigin();
                }
            }

        }
    }
}
