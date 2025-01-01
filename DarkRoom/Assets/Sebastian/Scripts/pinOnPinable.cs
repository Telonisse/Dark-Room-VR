using System.Collections.Generic;
using UnityEngine;

public class pinOnPinable : MonoBehaviour
{
    [SerializeField]
    private LayerMask pinableLayer;

    [SerializeField]
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();     

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & pinableLayer) != 0)
        {
            Debug.Log($"{collision.gameObject.name} is pinable!");
            foreach (var rigidbody in rigidbodies)
            {
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = true;
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & pinableLayer) != 0)
        {
            Debug.Log($"{other.gameObject.name} is pinable!");
            foreach (var rigidbody in rigidbodies)
            {
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = true;
                }
            }
        }
    }

}
