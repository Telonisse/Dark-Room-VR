using UnityEngine;

public class CodeLockButton : MonoBehaviour
{
    [Tooltip("Number of the button this script is added to!")]
    [SerializeField]
    public int buttonNumber= 0;

    [Tooltip("The codelock with the code script on it")]
    [SerializeField]
    public GameObject theCodeLock;

    private CodeLockOpen codeLock;

    [Tooltip("Maximum distance from the collider to consider this button for selection.")]
    public float maxSelectionDistance = 0.3f;

    private void Start()
    {
      codeLock =  theCodeLock.GetComponent<CodeLockOpen>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsClosestButton(other.transform.position))
        {
            WhenButtonSelected();
        }        
    }

    public void WhenButtonSelected()
    {
        AudioSource btnklicked = GetComponent<AudioSource>();
        if (btnklicked != null) { btnklicked.Play();}
        codeLock.CodeButtonPressedUpdater(buttonNumber);
    }

    private bool IsClosestButton(Vector3 referencePosition)
    {
        // Get all colliders within the specified radius
        Collider[] nearbyButtons = Physics.OverlapSphere(transform.position, maxSelectionDistance);

        Collider closestButton = null;
        float closestDistance = float.MaxValue;

        foreach (var button in nearbyButtons)
        {
            // Check if the collider has a CodeLockButton script
            if (button.GetComponent<CodeLockButton>() != null)
            {
                float distance = Vector3.Distance(referencePosition, button.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestButton = button;
                }
            }
        }

        // Return true if this button is the closest
        return closestButton != null && closestButton.gameObject == this.gameObject;
    }
}
