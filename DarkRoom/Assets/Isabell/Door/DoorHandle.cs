using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorHandle : MonoBehaviour
{
    //Animation
    //[SerializeField] Animator animator;

    //private void Start()
    //{
    //    if (animator == null)
    //    {
    //        animator = GetComponent<Animator>();
    //    }
    //}
    //public void OnHandleGrabbed()
    //{
    //    animator.SetBool("Open", true);
    //}

    //public void OnHandleReleased()
    //{
    //    animator.SetBool("Open", false);
    //}


    [SerializeField] GameObject door;
    [SerializeField] float rotateCheck;

    [SerializeField] AudioSource soundLocked;
    [SerializeField] AudioSource soundUnlocked;
    private bool audioPlayed = false;

    private void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles.x);
        if (transform.rotation.eulerAngles.x < rotateCheck && transform.rotation.eulerAngles.x > 10 && door.GetComponentInChildren<Keyhole>().Unlocked() == true && GetComponent<HingeJoint>() != null)
        {
            Destroy(this.GetComponent<HingeJoint>());
            soundUnlocked.Play();
            Destroy(this.GetComponent<XRGrabInteractable>());
            Destroy(this.GetComponent<Rigidbody>());
            door.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            door.transform.GetComponent<XRGrabInteractable>().enabled = true;
        }
        if (transform.rotation.eulerAngles.x < rotateCheck && transform.rotation.eulerAngles.x > 10 && door.GetComponentInChildren<Keyhole>().Unlocked() == false && audioPlayed == false)
        {
            soundLocked.Play();
            audioPlayed = true;
        }
        else
        {
            audioPlayed = false;
        }
    }

    public void Grabbbed()
    {
        this.GetComponent<HingeJoint>().useSpring = false;
    }    
    public void Released()
    {
        this.GetComponent<HingeJoint>().useSpring = true;
    }

    public void Exit()
    {
        StartCoroutine(ExitScene());
    }

    IEnumerator ExitScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
