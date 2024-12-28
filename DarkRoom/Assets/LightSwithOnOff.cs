using System.Collections.Generic;
using UnityEngine;

public class LightSwithOnOff : MonoBehaviour
{
    [Tooltip("List of Lamps that will be Activated")]
    [SerializeField]
    public List<GameObject> listOfLamps;

    [Tooltip("The Animator from the where parameters will be effected")]
    [SerializeField]
    public Animator animator;

    [Tooltip("The Parameter bool that will be used for switching on or off")]
    [SerializeField]
    public string parameterName;
    [Tooltip("the collider that will trigger on")]
    [SerializeField]
    public Collider turnOnCollider;
    private bool onCollider = false;

    [Tooltip("the collide that will trigger off")]
    [SerializeField]
    public Collider turnOffCollider;
    //private bool offcollider = false;


    [Header("------------------------------------------------" + "\n--------------To Be Added | WIP ---------------" + "\n------------------------------------------------")]
    

    [Tooltip("If using Potentiometer and not a switch (WIP, not added)")]
    [SerializeField]
    public bool usingPotetiometer = false;

    [Tooltip("If its not a bool and have a float value")]
    [SerializeField]
    public float parameterfloatThreshOn = 0;

    [Tooltip("If its not a bool and have a Int value")]
    [SerializeField]
    public float parameterIntThreshOn = 0;

    [Tooltip("Max Value if using a potentiometer")]
    [SerializeField]
    public float maxValue = 10;

    [Tooltip("Min Value if using a potentiometer")]
    [SerializeField]
    public float minValue = 0;

    [Tooltip("If using Int and not Float to read value of potentiometer")]
    [SerializeField]
    public bool intBasedpotentiometer = false;

    [Tooltip("Tag of object that will interact")]
    [SerializeField]
    public string interactor;

    private bool activateAll = false;
    private int maxIntValue;
    private int minIntValue;

    private void Start()
    {
        FloatToInt();        
    }

    void Update()
    {
       // AnimatorParameterUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == this.turnOnCollider) 
        {
            onCollider = true; //offcollider = false;
            animator.SetBool(parameterName, true);
        }
        else if (other == this.turnOffCollider) 
        { 
            //offcollider = true; 
            onCollider = false;
            animator.SetBool(parameterName, false);
        }

        AnimatorParameterUpdate();
    }

    void AnimatorParameterUpdate()
    {

        if (onCollider && animator.GetBool(parameterName) == true )
        {
            activateAll = true;
            Invoke(nameof(AllLampsTurnOn), 0.5f);            
        }
        else
        {
            activateAll = false;
            Invoke(nameof(AllLampsTurnOff), 0.5f);
        }
    }

    void AllLampsTurnOn()
    {
        if (listOfLamps != null)
        {
            if (activateAll)
            { 
                int length = listOfLamps.Count;
                for (int i = 0; i < length; i++)
                {
                    listOfLamps[i].SetActive(true);
                }
            }
            else return;
        }
    }
    void AllLampsTurnOff()
    {
        if (listOfLamps != null)
        {
            if (!activateAll)
            {
                int length = listOfLamps.Count;
                for (int i = 0; i < length; i++)
                {
                    listOfLamps[i].SetActive(false);
                }
            }
            else return;
        }
    }

    void PotentiometerUpdate()
    {
        //TODO: add logic here for the potatiometer
    }

    void FloatToInt()
    {
        if (intBasedpotentiometer)
        {
            float maxTempValue = maxValue;
            float minTempValue = minValue;

            maxIntValue = Mathf.RoundToInt(maxTempValue);
            minIntValue = Mathf.RoundToInt(minTempValue);
        }
    }
}
