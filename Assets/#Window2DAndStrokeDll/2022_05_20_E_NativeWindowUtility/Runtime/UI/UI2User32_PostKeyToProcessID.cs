using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI2User32_PostKeyToProcessID : MonoBehaviour
{
    public InputField m_processId;
    public bool m_useString;
    public string m_postKeyToParse;
    public bool m_useEnum;
    public User32PostMessageKeyEnum m_postAsEnum;

    public void Push()
    {
        if (int.TryParse(m_processId.text, out int value))
        {
            if (m_useString)
                User32BoardcastUtilityToThread.TryParseAndSendToProcess(IntPtrTemp.Int(value), m_postKeyToParse);
            if (m_useEnum)
                User32BoardcastUtilityToThread.SendKey(IntPtrTemp.Int(value), m_postAsEnum);
        }
        else {
            if (m_useString )
                User32BoardcastUtilityToThread.HeavyTryParseAndSendToProcesses(m_processId.text, m_postKeyToParse);
            if (m_useEnum)
                User32BoardcastUtilityToThread.HeavyTryParseAndSendToProcesses(m_processId.text, m_postAsEnum);
        }
    }
}
