using UnityEngine;

public class TurnOnTV : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    public GameObject screenOff;
    [Tooltip("")]
    [SerializeField]
    public GameObject screenOn;
    [Tooltip("")]
    [SerializeField]
    public GameObject screenStatic;

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
        if (tapeInVhsPlayer && flipflop)
        {
            screenOff.SetActive(false);
            screenOn.SetActive(true);
            screenStatic.SetActive(false);
        }
        else if (flipflop && !tapeInVhsPlayer)
        {
            screenOff.SetActive(false);
            screenOn.SetActive(false);
            screenStatic.SetActive(true);
            screenOn.GetComponent<RandomizeVideoPlayer>().RandomVideoToPlayer();                
        }
        else
        {
            screenOff.SetActive(true);
            screenOn.SetActive(false);
            screenStatic.SetActive(false);
        }

    }

}
