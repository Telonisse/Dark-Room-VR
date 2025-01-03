using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeLockOpen : MonoBehaviour
{
    [Tooltip("List of all the buttons")]
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();

    [Tooltip("calender that have the code")]
    [SerializeField]
    public GameObject calender;

    private int c1 = 0, c2 = 0, c3 = 0, c4 = 0;
    private Calendar calendcode;
    private string code;

    [Tooltip("Dev mode to unlock and test code")]
    [SerializeField]
    public bool code1 = false;
    public bool code2 = false;
    public bool code3 = false;
    public bool code4 = false;

    [Tooltip("Sound For Correct Code")]
    [SerializeField]
    public AudioClip correctSound;

    [Tooltip("Sound For wrong Code")]
    [SerializeField]
    public AudioClip wrongSound;

    [Tooltip("Animation to play to unlock")]
    [SerializeField]
    public Animator unlockAnimation;

    [Tooltip("The Hock to remove collistions")]
    [SerializeField]
    public GameObject lockHock;

    private bool isUnLocked = false;
    
    void Start()
    {
        calendcode = calender.GetComponent<Calendar>();
        code = calendcode.GetCode();
        splitStringCode(code);
    }

    public void CodeButtonPressedUpdater(int number)
    {
        if (number == c1) { code1 = true; }
        if (number == c2) { code2 = true; }
        if (number == c3) { code3 = true; }
        if (number == c4) { code4 = true; }
        if (number != c1 || number != c2 || number != c3 || number != c4) { return; }
    }

    private bool correctCodeCheck()
    {
        if (code1 == true)
        {
            if (code2 == true)
            {
                if (code3 == true)
                {
                    if (code4 == true)
                    {
                        isUnLocked = true;
                        PlayCorrectCodeSound();
                        return true;
                    }
                    else { code1 = false; code2 = false; code3 = false; code4 = false; PlayWrongCodeSound(); return false; }
                }
                else { code1 = false; code2 = false; code3 = false; code4 = false; PlayWrongCodeSound(); return false; }
            }
            else { code1 = false; code2 = false; code3 = false; code4 = false; PlayWrongCodeSound(); return false; }
        }        
        return false; 
    }

    private void PlayWrongCodeSound()
    {
        
    }
    private void PlayCorrectCodeSound()
    {

    }

    private void splitStringCode(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length < 4)
        { return; }
        if (code.Length > 4)
        {
            char c1 = code[0];
            char c2 = code[1];
            char c3 = code[2];
            char c4 = code[3];
        }
    }
}
