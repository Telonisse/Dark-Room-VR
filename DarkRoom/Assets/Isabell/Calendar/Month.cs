using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Month : MonoBehaviour
{
    [SerializeField] int date;

    [Tooltip("Number of day in a week (Sunday is 0, monday is 1...)")]
    [SerializeField] int startDay;
    [SerializeField] int daysInMonth;
    [SerializeField] GameObject circle;
    [SerializeField] float spacingX = 0.22f;
    [SerializeField] float spacingY = 0.22f;
    [SerializeField] Vector3 firstDatePos;
    public int columns = 7;

    private void Start()
    {
        date = Random.Range(1, daysInMonth + 1);

        int index = date + startDay - 1;
        int row = index / columns;
        int col = index % columns;

        Vector3 datePosition = firstDatePos + new Vector3(col * spacingX, -row * spacingY, 0);

        circle.transform.localPosition = datePosition;

        Vector3 start = circle.transform.position;
        circle.transform.SetParent(this.transform);
        StartCoroutine(CirclePos(start));
    }

    IEnumerator CirclePos(Vector3 pos)
    {
        yield return new WaitForSeconds(1);
        circle.transform.position = pos;
    }

    public int GetCode()
    {
        return date;
    }
}
