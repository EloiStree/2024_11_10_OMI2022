using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI2User32_BroadcastToProcessName : MonoBehaviour
{
    public InputField m_processId;
    public InputField m_whatToCast;

    public void Push()
    {



        User32BoardcastUtilityToThread.HeavyTryParseAndSendToProcesses(m_processId.text, m_whatToCast.text);
    }
}
