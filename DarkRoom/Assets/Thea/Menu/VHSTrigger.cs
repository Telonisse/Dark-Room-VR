using UnityEngine;

public class VHSTrigger : MonoBehaviour
{
    private Animator animator;

    private bool playTriggered = false;
    private bool quitTriggered = false;
    private bool creditsTriggered = false;

    private void Start()
    { 
        //animator need to be attachtedto object where this script is
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play"))
        {
            playTriggered = true;
            // trigger opening flipper animation here
            Debug.Log("Play trigger touched");
        }
        else if (other.CompareTag("Quit"))
        {
            quitTriggered = true;
            // trigger opening flipper animation here
            Debug.Log("Quit trigger touched");
        }
        else if (other.CompareTag("Credits"))
        {
            creditsTriggered = true;
            // trigger opening flipper animation here
            Debug.Log("Credits trigger touched");
        }
    }

    public bool PlayTriggered() => playTriggered;
    public bool QuitTriggered() => quitTriggered;
    public bool CreditsTriggered() => creditsTriggered;

    public void ResetAllTriggers()
    {
        playTriggered = false;
        quitTriggered = false;
        creditsTriggered = false;
    }
}
