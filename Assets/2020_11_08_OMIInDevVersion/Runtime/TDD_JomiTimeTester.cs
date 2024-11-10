using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_JomiTimeTester : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_target;

    string[] m_commandToSend = new string[] {
        "tms:1000:sc: 1↓ 1↑",
        "tms:2000:sc: 2↓ 2↑",
        "tms:3000:sc: 3↓ 3↑",
        "tms:4000:sc: 4↓ 4↑",
        "tms:5000:sc: 5↓ 5↑",};

    public float m_timeBetweenCmds = 10;
    void Start()
    {
        InvokeRepeating("SendCommands", 0, m_timeBetweenCmds);
    }

    public void SendCommands()
    {

        foreach (var item in m_target.GetJavaOMISelected())
        {
            for (int i = 0; i < m_commandToSend.Length; i++)
            {
                item.SendRawCommand(m_commandToSend[i]);
            }
        }
        SendTimePlus();
    }
    public void SendTimePlus()
    {

        foreach (var item in m_target.GetJavaOMISelected())
        {
           
                DateTime now = DateTime.Now;
            now =now.AddSeconds(6);
                item.SendRawCommand(
                    string.Format("t:{0}-{1}-{2}-{3}:sc:[[Hello there]]", now.Hour, now.Minute, now.Second, now.Millisecond)
                ) ;
            
        }
    }


}
