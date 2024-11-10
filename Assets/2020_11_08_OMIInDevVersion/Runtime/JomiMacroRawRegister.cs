using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class JomiMacroRawRegister : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_target;
    public List<TimedCommandLines> m_macroRegistered= new List<TimedCommandLines>();
    
    public void Call(string callId) {
        //Debug.Log("Try to call: " + callId);
        TimedCommandLines lookedFor=null;
        for (int i = 0; i < m_macroRegistered.Count; i++)
        {
            if (m_macroRegistered[i].m_callId.Trim().ToLower() == callId.Trim().ToLower()) {
                lookedFor = m_macroRegistered[i];
                break;
            }
        }

        if (lookedFor != null)
        {
           // Debug.Log("Try to execute: " + callId);
            DateTime now = DateTime.Now;
            for (int i = 0; i < lookedFor.m_toSend.Count; i++)
            {
                DateTime whenToExecute = now;
                whenToExecute=whenToExecute.AddMilliseconds(lookedFor.m_toSend[i].m_timeInMs);
                foreach (var item in m_target.GetJavaOMISelected())
                {
                    string toSend = string.Format("t:{0}-{1}-{2}-{3}:{4}",
                        whenToExecute.Hour,
                        whenToExecute.Minute,
                        whenToExecute.Second,
                        whenToExecute.Millisecond,
                        lookedFor.m_toSend[i].m_commandLine);
                    item.SendRawCommand(toSend);

                  //  Debug.Log("Sent: " + toSend);
                }
            }
        }
    }

    public void Clear()
    {
        m_macroRegistered.Clear();
    }

    public void Add(TimedCommandLines macro)
    {
        if(macro!=null && !string.IsNullOrEmpty(macro.m_callId))
        m_macroRegistered.Add(macro);
    }
}


