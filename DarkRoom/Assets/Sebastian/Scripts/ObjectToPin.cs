using UnityEngine;

public class ObjectToPin : MonoBehaviour
{
    [SerializeField]
    public GameObject objectThatWillGetPin;

    private bool isPinned;
    private void OnCollisionEnter(Collision collision)
    {
        if (objectThatWillGetPin != null && !isPinned)
        {
            if (collision != null && collision.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = objectThatWillGetPin.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                isPinned = true;
                Debug.Log($"{objectThatWillGetPin.name} has been pinned.");
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (objectThatWillGetPin != null && isPinned)
        {
            if (collision != null && collision.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = objectThatWillGetPin.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                isPinned = false;
                Debug.Log($"{objectThatWillGetPin.name} has been unpinned.");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject == objectThatWillGetPin)
        {
            Rigidbody rb = objectThatWillGetPin.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            isPinned = true;
            Debug.Log($"{objectThatWillGetPin.name} has been pinned.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (objectThatWillGetPin != null && isPinned)
        {
            if (other != null && other.gameObject == objectThatWillGetPin)
            {
                Rigidbody rb = objectThatWillGetPin.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                isPinned = false;
                Debug.Log($"{objectThatWillGetPin.name} has been unpinned.");
            }
        }
    }
}
