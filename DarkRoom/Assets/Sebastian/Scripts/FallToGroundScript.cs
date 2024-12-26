using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FallToGroundScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [Tooltip("Delay time when calling TriggerGravity")]
    [SerializeField]
    float delayTriggerGravity = 2f;

    [SerializeField]
    List<AudioClip> audioClips = new List<AudioClip>();

    [Tooltip("The list of object that can trigger Fall on gound sound")]
    [SerializeField]
    List<GameObject> objectsToCollideWith = new List<GameObject>();

    [SerializeField]
    bool tweekSound = false;

    [Tooltip("If in need of custom value for example damage or points")]
    [SerializeField]
    public float customValue = 0; // if needed just call "gameObject.GetComponent<FallToGroundScript>().customValue" to get it

    public bool gravityIsTrigged = false; // Bool that will be true when Gravity is on. added if in need of debug or other logics in script or other places.
    private bool collidesWithSomthing = false;

    private AudioSource audioSource;

    private int randomSound;
   
    void Start()
    {
       rb = GetComponent<Rigidbody>();       
       audioSource = GetComponent<AudioSource>();

       if (audioSource == null)
       {
           audioSource = gameObject.AddComponent<AudioSource>(); // Adding the AudioSource if missing
       }
    }

    // Update is called once per frame
    void Update()
    {
        if (collidesWithSomthing)
        {
            if (audioClips.Count > 0)
            {
                randomSound = Random.Range(0, audioClips.Count); // gets a random sound from the list to play
                PlaySound(randomSound);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        if (objectsToCollideWith.Contains(collision.gameObject))
        {
            collidesWithSomthing = true;            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision == null) return;
    }

    private void PlaySound(int randomSound)
    {
        TweekSounds();
        audioSource.Stop();
        audioSource.Play();
    }       

    private void TweekSounds()
    {
        if (tweekSound)
        {
            //Add Tweeking of sound if needed.
        }
        else
        {
            return;
        }
    }

    public void TriggerGravity() // call for this and will have a delay.
    {
        if (!gravityIsTrigged)
        {
            gravityIsTrigged = true;
            Invoke(nameof(GravityTrueTurnOn), delayTriggerGravity);
        }
        else Debug.LogWarning("already been triggered!");
    }

    private void GravityTrueTurnOn()
    {        
        rb.useGravity = true;
    }

}
