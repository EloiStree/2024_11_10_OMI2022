using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ButtonToStringAction : MonoBehaviour
{
    public string m_stringToSendStored="";
    public UnityStringEvent m_stringToSendEvent;

    public void SendAction(string action) {
        m_stringToSendEvent.Invoke(action);
    }
    public void SendStoredAction() {
        m_stringToSendEvent.Invoke(m_stringToSendStored);
    }
}
