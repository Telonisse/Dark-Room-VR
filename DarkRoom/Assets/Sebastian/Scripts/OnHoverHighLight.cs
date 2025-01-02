using UnityEngine;

public class OnHoverHighLight : MonoBehaviour
{
    [Tooltip("The color that will high light the object")]
    [SerializeField]
    public Color highLightColor;

    private Color startColour;

    private void Start()
    {
        startColour = GetComponent<Renderer>().material.color;
    }
    public void HighLightOver()
    {
        GetComponent<Renderer>().material.color = highLightColor;
    }

    public void HighLightExit()
    {
        GetComponent<Renderer>().material.color = startColour;
    }
}
