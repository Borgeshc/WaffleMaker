using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoSerial : MonoBehaviour
{
    public string comPort;
    SerialPort stream;
    string serialString;
    public static bool isPressed;

    void Start()
    {

        comPort = "COM" + comPort;
        try
        {
            stream = new SerialPort(comPort, 9600);
            stream.Open();
            Debug.Log("Stream Open on " + comPort);
        }
        catch
        {
            Debug.Log("No Microcontroller on " + comPort);
        }
    }
    void Update()
    {
        UpdateCurrentValues();
    }
    void OnDestroy()
    {

    }
    void UpdateCurrentValues()
    {
        if (stream.IsOpen)
        {
            serialString = "";
            bool stringNotComplete = true;
            while (stringNotComplete)
            {
                char inChar = (char)stream.ReadByte();
                if (inChar == '\n')
                {
                    stringNotComplete = false;
                    MessageParse();
                }
                serialString += inChar;

            }
        }
    }
    public void MessageParse()
    {  //Do stuff with message here. 
       //print(serialString +"   " +serialString.Length );
        if (serialString.Length > 1)
        {
            string[] input = serialString.Split(',');

            //------player 1 shooting------------
            int p1ButtonValue = int.Parse(input[1]);

            if (p1ButtonValue == 1)
                isPressed = true;
            else
                isPressed = false;

            print(p1ButtonValue);

        }
    }
}
