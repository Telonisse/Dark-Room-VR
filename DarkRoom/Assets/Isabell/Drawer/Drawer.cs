using UnityEngine;

public class Drawer : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] AudioSource soundOpen;
    private bool openPlayed = false;
    [SerializeField] AudioSource soundClose;
    private bool closePlayed = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(rb.linearVelocity.x);
        if (rb.linearVelocity.x > 0.01f && openPlayed == false)
        {
            closePlayed = true;
            openPlayed = false;
            soundOpen.Stop();
            soundClose.Play();
        }
        else if (rb.linearVelocity.x < -0.01f && closePlayed == false)
        {
            Debug.Log("Open");
            openPlayed = true;
            closePlayed = false;
            soundClose.Stop();
            soundOpen.Play();
        }
    }
}
