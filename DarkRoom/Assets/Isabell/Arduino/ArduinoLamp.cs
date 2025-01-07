using UnityEngine;
using Ja;

public class ArduinoLamp : SerialDataTransciever
{
    public void TurnOnLed()
    {
        SendData("TurnOnLed");
    }

    public void TurnOffLed()
    {
        SendData("TurnOffLed");
    }
}
