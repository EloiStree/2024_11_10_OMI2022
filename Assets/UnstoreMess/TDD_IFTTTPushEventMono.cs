using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_IFTTTPushEventMono : MonoBehaviour
{
    public IFTTTPushEventMono m_toTest;

    [Header("Trigger Event")]
    public string m_iftttEvent = "HelloEvent";
    [Header("JSON Event")]
    public string m_iftttJson = "DoTheThing";
    public string m_value0 = "debuglog Do the thing one";
    public string m_value1 = "debuglog Do the thing two";
    public string m_value2 = "debuglog Do the thing three";


    [ContextMenu("Push Event")]
    public void PushEvent()
    {
        m_toTest.SendEventAsWebRequest(m_iftttEvent);
    }
    [ContextMenu("Push Event Json")]
    public void PushEventJson()
    {
        m_toTest.SendEventAsWebRequestJson(m_iftttJson, m_value0, m_value1, m_value2);
    }

}
