using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class VHSTrigger : MonoBehaviour
{
    private Animator animator;

    private bool playTriggered = false;
    private bool quitTriggered = false;
    private bool creditsTriggered = false;

    private bool isOpen = false;
    private bool isSnapped = false;

    public GameObject showToPressPlay;
    private void Start()
    { 
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSnapped == true)
        {
            return;
        }
        if ((other.CompareTag("Play") || other.CompareTag("Quit") || other.CompareTag("Credits")) && !isOpen)
        {
            animator.SetBool("IsOpen", true);  
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
        if (isSnapped == true)
        {
            return;
        }
        if (playTriggered && other.CompareTag("Play") || quitTriggered && other.CompareTag("Quit") || creditsTriggered && other.CompareTag("Credits") && isOpen)
        {
            Debug.Log("it is exiting");
            animator.SetBool("IsOpen", false);  
            isOpen = false;
        }
        

        if (other.CompareTag("Play"))
            playTriggered = false;
        else if (other.CompareTag("Quit"))
            quitTriggered = false;
        else if (other.CompareTag("Credits"))
            creditsTriggered = false;
        
    }

    public void snapping()
    {
        animator.SetBool("IsOpen", false);
        showToPressPlay.SetActive(true);
        isSnapped = true;
    }

    public void unSnapping()
    {
        isSnapped = false;
        showToPressPlay.SetActive(false);
    }

    public void CalculateWhatScene()
    {
        if (playTriggered == true)
        {
            Debug.Log("Starting Game");
            SceneManager.LoadSceneAsync(1);
        }
        if (quitTriggered == true)
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }
        if (creditsTriggered == true)
        {
            Debug.Log("Starting credits scene");
            SceneManager.LoadSceneAsync(2);
        }
        else return;
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
