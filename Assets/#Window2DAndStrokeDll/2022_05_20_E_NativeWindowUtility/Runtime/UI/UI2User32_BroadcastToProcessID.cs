using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI2User32_BroadcastToProcessID : MonoBehaviour
{
    public InputField m_processId;
    public InputField m_whatToCast;
    public void Push()
    {
        if (int.TryParse(m_processId.text, out int value)) { 
        User32BoardcastUtilityToThread.TryParseAndSendToProcess(IntPtrTemp.Int( value), m_whatToCast.text);
        
        }
    }
    public void PushAsChatText()
    {
        if (int.TryParse(m_processId.text, out int value))
        {
            User32BoardcastUtilityToThread.CopyPastChatText(IntPtrTemp.Int(value), m_whatToCast.text);

        }

    }
    public void PushChatText(string text) {
        if (int.TryParse(m_processId.text, out int value))
        {
            User32BoardcastUtilityToThread.CopyPastChatText(IntPtrTemp.Int(value), text);

        }

        

    }
}
