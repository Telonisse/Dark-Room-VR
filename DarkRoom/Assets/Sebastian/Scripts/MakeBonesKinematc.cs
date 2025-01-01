using System.Collections.Generic;
using UnityEngine;

public class MakeBonesKinematc : MonoBehaviour
{   
    [Tooltip("Toggles 'Is Kinematic'")]
    [SerializeField]
    public bool isKinematic = false;
    [Tooltip("Toggles 'Use Gravity'")]
    [SerializeField]
    public bool useGravity = false;

    private List<GameObject> objectBones = new List<GameObject>();

    private void Start()
    {
        // adds all the children in this.gameobject.contains("bone") to the list objectsBones        
        foreach (Transform child in transform)
        {            
            if (child.gameObject.name.ToLower().Contains("bone"))
            {                
                objectBones.Add(child.gameObject);
            }
        }
    }

    public void ToggleSettings()
    {
        foreach (GameObject obj in objectBones)
        {
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = !isKinematic;
            rb.useGravity = !useGravity;
        }
    }

    public void ToggleKinematicOn()
    {
        foreach (GameObject obj in objectBones)
        {
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
    public void ToggleKinematicOff()
    {
        foreach (GameObject obj in objectBones)
        {
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }

}
