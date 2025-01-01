using Unity.VisualScripting;
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

    private void Start()
    {
      codeLock =  theCodeLock.GetComponent<CodeLockOpen>();
    }

    private void OnTriggerEnter(Collider other)
    {
        WhenButtonSelected();
    }

    public void WhenButtonSelected()
    {
        codeLock.CodeButtonPressedUpdater(buttonNumber);
    }
}
