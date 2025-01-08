using System;
using System.Collections.Generic;
using UnityEngine;
using Ja;

public class BurnUpObjects : SerialDataTransciever
{
    [Tooltip("List of objects that will be destroyed")]
    [SerializeField]
    public List<GameObject> destroyableObjects = new List<GameObject>();

    [Tooltip("Special object that will be destroyed for exchanged object")]
    [SerializeField]
    public GameObject specialDestroyableObj;
    [Tooltip("Special object that will be exchanged for the special destroyed object")]
    [SerializeField]
    public GameObject specialExchangeObj;

    [Tooltip("Reference collider for scaling spawned objects")]
    [SerializeField]
    public Collider referenceCollider;

    [Tooltip("Reference collider for scaling spawned objects")]
    [SerializeField]
    public GameObject effectToPlay;

    [Tooltip("The delay time befor object gets destroyed and spawn of special object")]
    [SerializeField]
    public float burningTimeDelay = 2f;

    [SerializeField] ParticleSystem smokelit;
    [SerializeField] ParticleSystem fire;
    [SerializeField] Light pointLight;
    [SerializeField] ParticleSystem smokeOnWater;
    [SerializeField] ParticleSystem afterSmoke;

    private Vector3 exchangeTransform;
    private Quaternion exchangeRotation;

    private bool specialDestroyed = false;

    private bool lampOn = false;
    private int tries = 0;

    void Start()
    {
        // Warning system if forgor
        if (destroyableObjects == null || destroyableObjects.Count == 0)
        {
            Debug.LogWarning("The 'destroyableObjects' list is empty. Add objects to the list in the Inspector.");
        }
        if (specialDestroyableObj == null)
        {
            Debug.LogWarning("The 'specialDestroyableObj' is not assigned. Assign a GameObject in the Inspector.");
        }
        if (specialExchangeObj == null)
        {
            Debug.LogWarning("The 'specialExchangeObj' is not assigned. Assign a GameObject in the Inspector.");
        }
        if (referenceCollider == null)
        {
            Debug.LogWarning("The 'referenceCollider' is not assigned. Assign a Collider in the Inspector.");
        }
    }

    private void Update()
    {
        if (lampOn == false && communicator.PortOpen() && tries < 5)
        {
            tries++;
            SendData("turnonled");
            if (tries == 5)
            {
                lampOn = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(other);

        if (destroyableObjects.Contains(other.gameObject))
        {
            Destroy(other.gameObject, burningTimeDelay);
            effectToPlay.GetComponent<ParticleSystem>().Play();
        }
        if (IsPartOfSpecialDestroyable(other.gameObject) && !specialDestroyed)
        {
            effectToPlay.GetComponent<ParticleSystem>().Play();
            SaveTransformOfObject(other.gameObject);
            Destroy(specialDestroyableObj, burningTimeDelay);
            specialDestroyed = true;

            Invoke(nameof(HandleSpecialExchangeObject), burningTimeDelay);       
        }
    }
    private void HandleSpecialExchangeObject()
    {
        if (specialExchangeObj == null)
        {
            specialExchangeObj = Instantiate(specialExchangeObj, exchangeTransform, exchangeRotation);
        }
        else
        {
            specialExchangeObj.SetActive(true);
            specialExchangeObj.transform.position = exchangeTransform;
            specialExchangeObj.transform.rotation = exchangeRotation;
        }
        ScaleToFit(specialExchangeObj);
    }

    void SaveTransformOfObject(GameObject obj)
    {
        exchangeTransform = obj.transform.position;
        exchangeRotation = obj.transform.rotation;        
    }

    private bool IsPartOfSpecialDestroyable(GameObject obj)
    {
        if (!specialDestroyed)
        return obj == specialDestroyableObj || obj.transform.IsChildOf(specialDestroyableObj.transform);
        else return false;
    }

    private void ScaleToFit(GameObject obj)
    {
        if (referenceCollider == null) return;

        Vector3 referenceSize = referenceCollider.bounds.size;

        Bounds objBounds;
        Renderer objRenderer = obj.GetComponent<Renderer>();
        Collider objCollider = obj.GetComponent<Collider>();

        if (objRenderer != null) { objBounds = objRenderer.bounds; }
        else if (objCollider != null) { objBounds = objCollider.bounds; }
        else { return; }

        Vector3 objSize = objBounds.size;
        float scaleX = referenceSize.x / objSize.x;
        float scaleY = referenceSize.y / objSize.y;
        float scaleZ = referenceSize.z / objSize.z;

        float scaleFactor = Mathf.Min(scaleX, scaleY, scaleZ);
        obj.transform.localScale *= scaleFactor;
    }

    public void WaterOn()
    {
        SendData("turnoffled");
        smokelit.Stop();
        fire.Stop();
        pointLight.gameObject.SetActive(false);
        smokeOnWater.Play();

        float timer = 0f;
        bool timerActive = true;

        if (timerActive)
        {
            timer += Time.deltaTime; // Increment the timer
            if (timer >= 2f) // Check if 2 seconds have passed
            {
                timerActive = false;
                afterSmoke.Play();
            }
        }
    }
}
