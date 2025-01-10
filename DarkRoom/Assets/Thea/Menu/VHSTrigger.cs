using UnityEngine;

public class VHSTrigger : MonoBehaviour
{
    private Animator animator;

    private bool playTriggered = false;
    private bool quitTriggered = false;
    private bool creditsTriggered = false;

    private bool isOpen = false;

    private void Start()
    { 
        //animator need to be attachtedto object where this script is
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Play") || other.CompareTag("Quit") || other.CompareTag("Credits")) && !isOpen)
        {
            animator.SetBool("IsOpen", true);  // Start opening animation
            isOpen = true;

            if (other.CompareTag("Play"))
            {
                playTriggered = true;
                Debug.Log("Play trigger touched");
            }
            else if (other.CompareTag("Quit"))
            {
                quitTriggered = true;
                Debug.Log("Quit trigger touched");
            }
            else if (other.CompareTag("Credits"))
            {
                creditsTriggered = true;
                Debug.Log("Credits trigger touched");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playTriggered && other.CompareTag("Play") || quitTriggered && other.CompareTag("Quit") || creditsTriggered && other.CompareTag("Credits") && isOpen)
        {
            Debug.Log("it is exiting");
            animator.SetBool("IsOpen", false);  // Start closing animation
            isOpen = false;
        }

        if (other.CompareTag("Play"))
            playTriggered = false;
        else if (other.CompareTag("Quit"))
            quitTriggered = false;
        else if (other.CompareTag("Credits"))
            creditsTriggered = false;
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
