using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Calendar : MonoBehaviour
{
    [SerializeField] GameObject[] months;
    [SerializeField] XRSocketInteractor snap;

    private int chosenMonth;
    public int chosenDay;

    [SerializeField] string code;
    void Start()
    {
        chosenMonth = Random.Range(0, months.Length);
        for (int i = 0; i < months.Length; i++)
        {
            if (i == chosenMonth)
            {
                months[i].SetActive(true);
                snap.startingSelectedInteractable = months[i].GetComponent<XRBaseInteractable>();
            }
            else
            {
                months[i].SetActive(false);
            }
        }
        StartCoroutine(MakeCode());
    }

    IEnumerator MakeCode()
    {
        yield return new WaitForSeconds(1);
        chosenDay = months[chosenMonth].GetComponent<Month>().GetCode();
        chosenMonth++;
        if (chosenMonth.ToString().Length == 1)
        {
            code += "0" + chosenMonth.ToString();
        }
        else if (chosenMonth.ToString().Length > 1)
        {
            code += chosenMonth.ToString();
        }
        if (chosenDay.ToString().Length == 1)
        {
            code += "0" + chosenDay.ToString();
        }
        else if (chosenDay.ToString().Length > 1)
        {
            code += chosenDay.ToString();
        }
    }

    public string GetCode()
    {
        return code;
    }
}
