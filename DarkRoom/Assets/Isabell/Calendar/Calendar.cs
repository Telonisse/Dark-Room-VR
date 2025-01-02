using UnityEngine;

public class Calendar : MonoBehaviour
{
    [SerializeField] GameObject[] months;

    public int chosenMonth;
    void Start()
    {
        chosenMonth = Random.Range(0, months.Length);
        for (int i = 0; i < months.Length; i++)
        {
            if (i == chosenMonth)
            {
                months[i].SetActive(true);
            }
            else
            {
                months[i].SetActive(false);
            }
        }
    }
}
