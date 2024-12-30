using UnityEngine;
using UnityEngine.Video;

public class TurnOnTV : MonoBehaviour
{
    [Tooltip("screen that shows Screen turned off")]
    [SerializeField]
    public GameObject screenOff;
    [Tooltip("screen that shows Screen turned On")]
    [SerializeField]
    public GameObject screenOn;
    [Tooltip("screen that shows a static or special Screen")]
    [SerializeField]
    public GameObject screenStatic;

    [Tooltip("gameobject that will handle the raycasting and activate the tv")]
    [SerializeField]
    public GameObject remote;
    [Tooltip("target to hit with raycast to activate tv with remote")]
    [SerializeField]
    public Transform targetRaycast;
    [Tooltip("Range of raycast")]
    [SerializeField]
    public float rayRange = 10.0f;

    [Header("manual toggle for testing if VHS is in player")]
    public bool tapeInVhsPlayer = false;

    private bool flipflop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenOff.SetActive(true);
        screenOn.SetActive(false);
        screenStatic.SetActive(false);
    }

    public void TurnOnTvScreen()
    {
        flipflop = !flipflop;        

        if (RaycastToTarget() && tapeInVhsPlayer && flipflop)
        {
            screenOff.SetActive(false);
            screenOn.SetActive(true);
            screenStatic.SetActive(false);
        }
        else if (RaycastToTarget() && flipflop && !tapeInVhsPlayer)
        {

            screenOn.GetComponent<RandomizeVideoPlayer>().RandomVideoToPlayer();
            if (screenOn.GetComponent<VideoPlayer>().clip != null)
            {
                screenOff.SetActive(false);
                screenOn.SetActive(false);
                screenStatic.SetActive(true);
            }
            else Debug.Log("No Clip set");
        }
        else
        {
            if (RaycastToTarget())
            {
                screenOff.SetActive(true);
                screenOn.SetActive(false);
                screenStatic.SetActive(false);
            }
        }

    }

    bool RaycastToTarget()
    {
        Ray ray = new Ray(remote.transform.position, remote.transform.forward);

        if (Physics.Raycast(ray,out RaycastHit hit, rayRange)) 
        {
            return hit.transform == targetRaycast;
        }
        else return false;
    }
}
