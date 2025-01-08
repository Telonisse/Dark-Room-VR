using UnityEngine;
using Ja;

public class ArduinoLamp : SerialDataTransciever
{
    public void TurnOnLed()
    {
        SendData("turnonled");
    }

    public void TurnOffLed()
    {
        SendData("turnoffled");
    }
}
