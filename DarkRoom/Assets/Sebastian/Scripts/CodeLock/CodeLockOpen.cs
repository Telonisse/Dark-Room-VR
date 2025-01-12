using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CodeLockOpen : MonoBehaviour
{
    [Tooltip("Contains the name filter for child objects to be added to List of buttons")]
    [SerializeField]
    public string childButtonName;

    [Tooltip("Calendar that has the code")]
    [SerializeField]
    public GameObject calendar;

    private int c1 = 0, c2 = 0, c3 = 0, c4 = 0;
    private Calendar calendarCode;
    private string code;

    [Tooltip("Delay time before gravity and collider enable after animation")]
    [SerializeField]
    public float delayTime = 1f;

    [Tooltip("Sound for correct code")]
    [SerializeField]
    public AudioClip correctSound;

    [Tooltip("Sound for wrong code")]
    [SerializeField]
    public AudioClip wrongSound;

    [Tooltip("Animation to play to unlock")]
    [SerializeField]
    public Animator unlockAnimation;

    [Tooltip("The hook to remove collisions")]
    [SerializeField]
    public GameObject lockHook;

    [Tooltip("List of XR Grab Interactables to enable after unlocking")]
    [SerializeField]
    public List<XRGrabInteractable> doorNHingeGrab = new List<XRGrabInteractable>();

    [Tooltip("List of doors to enable movement after unlocking")]
    [SerializeField]
    public List<GameObject> doorsToUnfreeze = new List<GameObject>();

    public bool creativeUnlock = false;

    private bool isUnlocked = false;
    private int currentCodeIndex = 0; // Tracks the progress of the entered code
    private List<GameObject> buttons = new List<GameObject>();
    private float timer = 0f; // Timer counter    
    private bool hasExecuted = false;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!hasExecuted)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                hasExecuted = true;
                ExecuteAfterDelay();
            }
        }
        if (isUnlocked)
        {
            Invoke(nameof(EnableRigidbodyAndCollider), delayTime);
            foreach (XRGrabInteractable grab in doorNHingeGrab)
            {
                grab.enabled = true;
            }
        }
        if (creativeUnlock)
        {
            ToggleCreativeLock();
            creativeUnlock = false;
        }
    }

    public void CodeButtonPressedUpdater(int number)
    {
        // Check if the button pressed matches the next code value
        if (currentCodeIndex < 4 && number == GetCodeDigit(currentCodeIndex))
        {
            currentCodeIndex++;
            if (currentCodeIndex == 4) // Code is fully entered correctly
            {
                animationUnlock();
            }
        }
        else
        {
            // Reset on incorrect input
            PlayWrongCodeSound();
            ResetCode();
        }
    }

    private int GetCodeDigit(int index)
    {
        return index switch
        {
            0 => c1,
            1 => c2,
            2 => c3,
            3 => c4,
            _ => -1
        };
    }

    private void ResetCode()
    {
        currentCodeIndex = 0;
    }

    private void PlayWrongCodeSound()
    {
        if (wrongSound != null)
        {
            AudioSource.PlayClipAtPoint(wrongSound, transform.position);
        }
    }

    private void PlayCorrectCodeSound()
    {
        if (correctSound != null)
        {
            AudioSource.PlayClipAtPoint(correctSound, transform.position);
        }
    }

    private void animationUnlock()
    {
        isUnlocked = true;

        if (unlockAnimation != null)
        {
            unlockAnimation.SetTrigger("Unlocked");
        }

        Invoke("Unlock", 2f);
    }


    private void Unlock()
    {
        if (lockHook != null)
        {
            foreach (BoxCollider coll in lockHook.GetComponentsInChildren<BoxCollider>())
            {
                coll.enabled = false;
            }

            rb.useGravity = true;

            foreach (GameObject door in doorsToUnfreeze)
            {
                Rigidbody rb = door.GetComponent<Rigidbody>();
                if (rb != null)
                {                    
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
            DeActivateButtons();
        }
        GetComponent<CapsuleCollider>().enabled = true;
        //GetComponent<Animator>().enabled = false;
        PlayCorrectCodeSound();
    }

    private void splitStringCode(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length < 4)
        {
            Debug.LogError("Code is invalid or too short.");
            return;
        }

        // Assign values to class-level variables
        c1 = code[0] - '0';
        c2 = code[1] - '0';
        c3 = code[2] - '0';
        c4 = code[3] - '0';
    }

    private void addButtonsToList(string refName)
    {
        // Ensure the GameObject has children
        foreach (Transform child in transform)
        {
            if (child.name.Contains(refName)) // Check if the child's name matches the reference name
            {
                buttons.Add(child.gameObject); // Add the child GameObject to the buttons list
            }
        }
    }

    private void EnableRigidbodyAndCollider()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }

        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = true;
        }
    }

    void ExecuteAfterDelay()
    {
        calendarCode = calendar.GetComponent<Calendar>();
        code = calendarCode.GetCode();
        addButtonsToList(childButtonName);
        splitStringCode(code);
        timer = 0f;
    }

    void ToggleCreativeLock()
    {
        animationUnlock();
    }

    void DeActivateButtons()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider box in boxColliders)
        {
            if (box.gameObject.name.Contains("Button"))
            {
                box.enabled = false;
            }
        }
    }
}
