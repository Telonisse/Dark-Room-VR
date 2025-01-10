using UnityEngine;

public class KinematicAtGroundCollision : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 originallV = Vector3.zero;
    private Vector3 originalaV = Vector3.zero;
    public bool freezeAll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originallV = rb.linearVelocity;
        originalaV = rb.angularVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {  
           
        if (other.CompareTag("Ground"))
        {
            FreezeRigidbody();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            FreezeRigidbody();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            UnfreezeRigidbody();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            UnfreezeRigidbody();
        }
    }

    private void FreezeRigidbody()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (freezeAll)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void UnfreezeRigidbody()
    {
        rb.isKinematic = false;
        rb.linearVelocity = originallV;
        rb.angularVelocity = originalaV;
        if (freezeAll)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
