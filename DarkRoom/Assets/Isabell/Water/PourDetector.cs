using UnityEngine;

public class PourDetector : MonoBehaviour
{
    [SerializeField] int pourThreshhold = 45;
    [SerializeField] Transform origin = null;
    [SerializeField] GameObject streamPrefab = null;

    [SerializeField] string collisionTag = "Ground";

    private bool isPouring = false;
    private Stream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshhold;

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

        //RaycastHit hit;
        //if (Physics.Raycast(origin.position, Vector3.down, out hit, Mathf.Infinity))
        //{
        //    if (hit.collider.CompareTag(collisionTag))
        //    {
        //        Debug.Log("Collision detected with tag: " + collisionTag);
        //        if (pourCheck == true)
        //        {
        //            Debug.Log("Pouring on " + hit.transform.name);
        //        }
        //    }
        //}
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
        currentStream = null;
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
