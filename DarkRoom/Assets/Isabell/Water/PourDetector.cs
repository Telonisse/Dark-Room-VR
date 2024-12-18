using UnityEngine;
using UnityEngine.Tilemaps;

public class PourDetector : MonoBehaviour
{
    [SerializeField] int pourThreshhold = 45;
    [SerializeField] Transform origin = null;
    [SerializeField] GameObject streamPrefab = null;

    [SerializeField] string collisionTag = "Ground";

    [SerializeField] GameObject liquid;
    [SerializeField] float reduceRate = 1;
    private Renderer rend;
    private float maxLiquid;

    private bool isPouring = false;
    private Stream currentStream = null;

    private void Start()
    {
        rend = liquid.GetComponent<Renderer>();
        maxLiquid = rend.material.GetFloat("_Fill");
    }

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshhold && rend.material.GetFloat("_Fill") > -0.4f;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(origin.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(collisionTag))
            {
                Debug.Log("Collision detected with tag: " + collisionTag);
                if (pourCheck == true)
                {
                    Debug.Log("Pouring on " + hit.transform.name);
                }
            }
        }
        if (currentStream != null)
        {
            currentStream.transform.rotation = Quaternion.identity;
        }

        EmptyLiquid();
    }

    private void EmptyLiquid()
    {
        if (isPouring)
        {
            float fill = rend.material.GetFloat("_Fill");
            if(fill > -0.5f)
            {
                fill -= reduceRate * Time.deltaTime;
            }
            rend.material.SetFloat("_Fill", fill);
        }
    }

    private void StartPour()
    {
        Debug.Log("start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("end");
        currentStream.End();
        //currentStream = null;
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}
