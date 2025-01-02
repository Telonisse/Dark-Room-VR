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

        Vector3 datePosition = firstDatePos + new Vector3(-col * spacingX, -row * spacingY, 0);

        circle.transform.localPosition = datePosition;
    }

    public int GetCode()
    {
        return date;
    }
}
