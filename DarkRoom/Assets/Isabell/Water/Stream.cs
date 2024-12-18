using System.Collections;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private Coroutine pourRoutine = null;
    private Vector3 targetPos = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        MoveToPos(0, transform.position);
        MoveToPos(1, transform.position);
    }

    public void Begin()
    {
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPos = FindEndPoint();

            MoveToPos(0, transform.localPosition);
            AnimateToPos(1, targetPos - transform.parent.position);

            yield return null;
        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while (!HasReachedPos(0, targetPos))
        {
            AnimateToPos(0, targetPos);
            AnimateToPos(1, targetPos);
            yield return null;
        }
        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2f);

        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2);
        Debug.Log("Pouring on " + hit.transform.name);

        return endPoint;
    }

    private void MoveToPos(int index, Vector3 targetPos)
    {
        lineRenderer.SetPosition(index, targetPos);
    }

    private void AnimateToPos(int index, Vector3 targetPos)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPos, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPos);
    }

    private bool HasReachedPos(int index, Vector3 targetPos)
    {
        Vector3 currentPos = lineRenderer.GetPosition(index);

        return currentPos == targetPos;
    }
}
