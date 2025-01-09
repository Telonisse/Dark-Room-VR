using Unity.VisualScripting;
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

    [SerializeField] float bucketRadius = 0.5f;

    private bool hora = false;

    private void Start()
    {
        rend = liquid.GetComponent<Renderer>();
        maxLiquid = rend.material.GetFloat("_Fill");
    }

    private void Update()
    {
        CalculateEdgePosition();
        bool pourCheck = CalculatePourAngle() < pourThreshhold && rend.material.GetFloat("_Fill") > -0.5f;

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

        if (rend.material.GetFloat("_Fill") < -0.4)
        {
            liquid.GetComponent<Wobble>().Wobbles(true);
        }
        else
        {
            liquid.GetComponent<Wobble>().Wobbles(false);
        }

        RaycastHit hit;
        if (Physics.Raycast(origin.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(collisionTag))
            {
                if (pourCheck == true && hora == false)
                {
                    hora = true;
                    hit.transform.GetComponent<BurnUpObjects>().WaterOn();
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
    private void FillLiquid()
    {
        if (!isPouring)
        {
            float fill = rend.material.GetFloat("_Fill");
            if (fill < maxLiquid)
            {
                fill += reduceRate * Time.deltaTime;
            }
            if(fill > maxLiquid)
            {
                fill = maxLiquid;
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

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Rain")
        {
            FillLiquid();
        }
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

    private void CalculateEdgePosition()
    {
        Quaternion rotation = this.transform.rotation;

        Vector3 direction = rotation * Vector3.up;
        direction = direction.normalized;

        Vector3 newPosition = direction * bucketRadius;
        newPosition.y = 0.45f;

        origin.localPosition = newPosition;
    }
}
