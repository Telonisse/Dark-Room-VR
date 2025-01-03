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

    private float timer;
    private bool timeout;

    private Cloth cloth;
    private float tempStrech = 0f;
    private float tempBendStiff = 0f;

    private ConstantForce force;

    private Coroutine timerCoroutine;

    private void Start()
    {
        force = GetComponent<ConstantForce>();
        cloth = GetComponent<Cloth>();
        timeout = false;        
        force.enabled = false;
        tempStrech = cloth.stretchingStiffness;
        tempBendStiff = cloth.bendingStiffness;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!tagMask.Contains(other.gameObject))
        {
            Debug.Log($"Collided with a tagged object: {other.gameObject.tag}");
        }
        else
        {
            FallingOver();
            Debug.Log($"Collided with a tagged object: {other.gameObject.tag}");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!tagMask.Contains(collision.gameObject))
        {
            Debug.Log($"Collided with a tagged object: {collision.gameObject.tag}");
        }
        else
        {
            FallingOver();
            Debug.Log($"Collided with a tagged object: {collision.gameObject.tag}");
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
