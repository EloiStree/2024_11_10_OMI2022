using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;



public class Experiment_PushIFTTTEventWithUnity : MonoBehaviour
{

    public AbstractImportantStringMono m_hideIftttKey;
    public string m_iftttEvent = "HelloEvent";
    public string m_iftttJson = "DoTheThing";
    public string m_value0 = "debuglog Do the thing one";
    public string m_value1 = "debuglog Do the thing two";
    public string m_value2 = "debuglog Do the thing three";

   
    [ContextMenu("Where to find ID")]
    public void WhereToFindAPI_ID() => IFTTTUtility.OpenWebPageWhereToFindAPIKey();

    [ContextMenu("Send  Request ")]
    public void SendAsWebRequest()
    {
        if (Application.isPlaying)
            IFTTTUtility.CSharp.PostEvent(m_hideIftttKey.GetStringValue(), m_iftttEvent) ;
        else
            StartCoroutine( IFTTTUtility.Coroutine.PostEvent(m_hideIftttKey.GetStringValue(), m_iftttEvent));
    }
    [ContextMenu("Send Json Request")]
    public void SendAsWebRequestJson()
    {
        if (Application.isPlaying)
            IFTTTUtility.CSharp.PostJsonEvent(m_hideIftttKey.GetStringValue(), m_iftttJson, m_value0, m_value1, m_value2);
        else
            StartCoroutine(IFTTTUtility.Coroutine.PostJsonEvent(m_hideIftttKey.GetStringValue(), m_iftttJson, m_value0, m_value1, m_value2));
    }



}
