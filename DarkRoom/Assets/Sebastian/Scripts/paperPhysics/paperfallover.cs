using System.Collections;
using UnityEngine;

public class paperfallover : MonoBehaviour
{
    [Tooltip("Selected tag will be ignored when colliding")]
    public TagMask tagMask;

    [SerializeField]
    public float strechStiff = 1f;
    public float bendStiff = 1f;
    public float timeOfStiff = 1f;

    [Tooltip("if left empty, will get gameobject components")]
    public ConstantForce force;
    public Cloth cloth;

    private float timer;
    private bool timeout;
        
    private float tempStrech = 0f;
    private float tempBendStiff = 0f;

    private Coroutine timerCoroutine;

    private void Start()
    {
        if (cloth == null) cloth = GetComponent<Cloth>();
        if (force == null) force = GetComponent<ConstantForce>();        
        timeout = false;        
        force.enabled = false;
        tempStrech = cloth.stretchingStiffness;
        tempBendStiff = cloth.bendingStiffness;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (tagMask.Contains(other.gameObject.tag))
        {
            Debug.Log($"Collided with a tagged object: {other.gameObject.tag}");
        }
        else if (!tagMask.Contains(other.gameObject.tag))
        {
            FallingOver();            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (tagMask.Contains(collision.gameObject.tag))
        {
            Debug.Log($"Collided with a tagged object: {collision.gameObject.tag}");
        }
        else if(!tagMask.Contains(collision.gameObject.tag))
        {
            FallingOver();            
        }
    }

    private void FallingOver()
    {
        StartTimer();
        if (timeOfStiff != timer && timeout)
        {
            Debug.Log("Falling over!");          

            force.enabled = true;
            cloth.bendingStiffness = bendStiff;
            cloth.stretchingStiffness = strechStiff;

        }

    }

    
    private IEnumerator TimerCountdown()
    {
        timer = timeOfStiff;
        timeout = false;

        while (timer > 0) 
        {
            timer -= Time.deltaTime; 
            yield return null;
        }

        timeout = true;
        OnTimerTimeout();
    }

    private void StartTimer()
    {       
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        timerCoroutine = StartCoroutine(TimerCountdown());
    }

    public void TimerReset()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); 
        }

        timeout = false;
        StartTimer();
    }
    private void OnTimerTimeout()
    {
        Debug.Log("Timer has timed out, reset cloth!");
        force.enabled = false;
        cloth.bendingStiffness = tempBendStiff;
        cloth.stretchingStiffness = tempStrech;
        StopCoroutine(timerCoroutine);
    }
}
