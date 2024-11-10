using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToUDPSender : MonoBehaviour
{
    public UDPThreadSender m_sender;
    public InputField m_messageAsString;
    public InputField m_targetAsString;
    public char m_spliter=',';
    public void TryToExecute()
    {
        if (m_targetAsString == null || m_targetAsString.text.Length <= 0)
            m_sender.AddMessageToSendToAll(m_messageAsString.text);
        else {
          string [] tokens =  m_targetAsString.text.Split(m_spliter);
            for (int i = 0; i < tokens.Length; i++)
            {
                m_sender.AddMessageToSendToAll(m_messageAsString.text);

            }
        }
    }
}
