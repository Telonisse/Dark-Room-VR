using System.Collections.Generic;
using UnityEngine;

public class CodeLockOpen : MonoBehaviour
{
    [Tooltip("List of all the buttons")]
    [SerializeField]
    public List<GameObject> buttons = new List<GameObject>();

    [Header("The Code will be in order of c1 to c4")]

    [Tooltip("Code to unlock lock, remeber that 0 dont exist on this codelock")]
    [SerializeField]
    public int c1 = 0, c2 = 0, c3 = 0, c4 = 0;

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

    private bool isUnLocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
