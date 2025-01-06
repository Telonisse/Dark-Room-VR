using System.Collections;
using UnityEngine;

public class PaperDampChange : MonoBehaviour
{
    private Cloth cloth;
    [Tooltip("Tag name of object that will effect Damp in objects Cloth component")]
    public string nameOfTag;
    public float setDamp = 1f;
    public float setStretch = 1f;
    public float dampeningTime = 0.5f;
    private float originalDamp;
    private float originalStretch;
    private Coroutine dampingCoroutine;

    void Start()
    {
        cloth = GetComponent<Cloth>();
        originalDamp = cloth.damping;
        originalStretch = cloth.stretchingStiffness;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cloth != null)
        {
            if (collision.collider.gameObject.tag == nameOfTag)
            {
                StartCoroutine(ChangeDamping(cloth.damping, setDamp, dampeningTime));
                //if (cloth.damping == setDamp)
                //{
                //    StopCoroutine(dampingCoroutine);
                //}
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (cloth != null)
        {
            if (collision.collider.gameObject.tag == nameOfTag)
            {
                cloth.damping = originalDamp;
                cloth.stretchingStiffness = originalStretch;
            } 
        }
    }

    private IEnumerator ChangeDamping(float fromDamp, float toDamp, float duration)
    {
        float elapsedTime = 0f;

        // Smooth transition from the current damping to the target damping
        while (elapsedTime < duration)
        {
            cloth.damping = Mathf.Lerp(fromDamp, toDamp, elapsedTime / duration);
            cloth.stretchingStiffness = Mathf.Lerp(cloth.stretchingStiffness, setStretch, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final damping is set exactly to the target
        cloth.damping = toDamp;
        cloth.stretchingStiffness = setStretch;
    }
}
