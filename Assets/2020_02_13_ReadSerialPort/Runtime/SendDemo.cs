using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDemo : MonoBehaviour
{
    public OpenSerialPortAtStart serial;
    public string sendOn = "1";
    public string sendOff = "0";
    public string toSend = "";
    public bool m_switchLoop;
    private bool m_switchState;
    IEnumerator Start()
    {
        while (true) {
            yield return new WaitForSeconds(1);
           
                m_switchState = !m_switchState; 
            if (serial && toSend.Length > 0)
            {
                toSend = m_switchState ? sendOff : sendOn;
                serial.m_communication.AddToSendMessage(toSend);
            }
            
        }

    }


}
