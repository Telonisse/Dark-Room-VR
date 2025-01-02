using UnityEngine;

public class Month : MonoBehaviour
{
    [SerializeField] int date;

    [Tooltip("Number of day in a week (ex, tuesday would be 2)")]
    [SerializeField] int startDay;
    [SerializeField] int amountOfDays;
    [SerializeField] GameObject circle;

    public int currentDay = 0;

    private int weeks = 5;
    private int days = 7;
    private bool first = false;

    private void Start()
    {
        //date = Random.Range(1, amountOfDays + 1);
        for (int i = 0; i < weeks; i++)
        {
            if (first == false)
            {
                for (int j = startDay; j < days; j++)
                {
                    currentDay += 1;
                    if (currentDay == date)
                    {
                        Debug.Log(i + " , " + j);
                        circle.transform.localPosition = new Vector3(circle.transform.localPosition.x - (0.22f * j), circle.transform.localPosition.y - (0.22f * i), circle.transform.localPosition.z);
                    }
                }
            }
            else
            {
                for (int j = 0; j < days; j++)
                {
                    currentDay += 1;
                    if (currentDay == date)
                    {
                        Debug.Log(i + " , " + j);
                        circle.transform.localPosition = new Vector3(circle.transform.localPosition.x - (0.22f * (j - 1)), circle.transform.localPosition.y - (0.22f * i), circle.transform.localPosition.z);
                    }
                }
            }
            first = true;
        }
    }
}
