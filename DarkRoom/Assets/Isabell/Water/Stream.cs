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
        MoveToPos(0, Vector3.zero);
        MoveToPos(1, Vector3.zero);
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
            targetPos = transform.InverseTransformPoint(targetPos);
            targetPos.x = 0;
            targetPos.z = 0;

            MoveToPos(0, Vector3.zero);
            AnimateToPos(1, targetPos);

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
        newPos.x = 0;
        newPos.z = 0;
        lineRenderer.SetPosition(index, newPos);
    }

    private bool HasReachedPos(int index, Vector3 targetPos)
    {
        Vector3 currentPos = lineRenderer.GetPosition(index);

        return currentPos == targetPos;
    }
}
