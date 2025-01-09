using UnityEngine;

public class KinematicAtGroundCollition : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground") rb.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Ground") rb.isKinematic = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground") rb.isKinematic = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") rb.isKinematic = true;
    }
}
