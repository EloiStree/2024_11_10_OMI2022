using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFTTTPushEventMono : MonoBehaviour
{

    public AbstractImportantStringMono m_hideIftttKey;


    [ContextMenu("Where to find ID")]
    public void OpenIFTTTWebpage_WhereToFindAPI_ID() => IFTTTUtility.OpenWebPageWhereToFindAPIKey();
    [ContextMenu("Open IFTTT")]
    public void OpenIFTTTWebpage() => IFTTTUtility.OpenOfficialWebsite();
    [ContextMenu("Open Applets")]
    public void OpenIFTTTWebpage_UserApplets() => IFTTTUtility.OpenUserAppletsOnOfficialWebsite();

    public void SendEventAsWebRequest(string iftttEventName)
    {
        if (Application.isPlaying)
            IFTTTUtility.CSharp.PostEvent(m_hideIftttKey.GetStringValue(), in iftttEventName);
        else
            StartCoroutine(IFTTTUtility.Coroutine.PostEvent(m_hideIftttKey.GetStringValue(), iftttEventName));
    }
    [ContextMenu("Send Json Request")]
    public void SendEventAsWebRequestJson(string iftttEventName, string value0 = "", string value1="", string value2 = "")
    {
        if (Application.isPlaying)
            IFTTTUtility.CSharp.PostJsonEvent(m_hideIftttKey.GetStringValue(), in iftttEventName,in value0, in value1, in value2);
        else
            StartCoroutine(IFTTTUtility.Coroutine.PostJsonEvent(m_hideIftttKey.GetStringValue(), iftttEventName, value0, value1, value2));
    }

    

}
