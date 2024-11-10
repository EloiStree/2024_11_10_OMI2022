using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoogleDocObserverAccessor
{

    public GoogleDocObserverAccessor()
    {
        m_googleDocId = "";
    }
    public GoogleDocObserverAccessor(string docId)
    {
        m_googleDocId = docId;
    }

    [Header("Set")]
    public string m_googleDocId = "";

    [Header("Listeners")]
    public Eloi.PrimitiveUnityEvent_String m_listenToDownloaded = new Eloi.PrimitiveUnityEvent_String();

    [Header("Downloaded")]
    [TextArea(0, 10)]
    public string m_googleDocContentCurrent = "";
    public double m_timeToFetchTheDoc = 0;


    [ContextMenu("Refresh")]
    public void FetchGoogleDocWithCSharp()
    {
        FetchGoogleDocWithCSharp(true);
    }

    public void FetchGoogleDocWithCSharp(bool notifyChange)
    {
        DateTime t = DateTime.Now;
        GoogleDocAndSheetUtility.GetTextInGoogleDoc(in m_googleDocId,
             out m_googleDocContentCurrent);
        DateTime e = DateTime.Now;
        m_timeToFetchTheDoc = (e - t).TotalSeconds;
        if (notifyChange )
        {
            CheckIfNeedToPushNewConcent( m_googleDocContentCurrent);
        }
    }
    public IEnumerator FetchGoogleDocWithCoroutine(bool notifyChange)
    {
        DateTime t = DateTime.Now;
        GoogleDocAndSheetUtility.StringOutputRef callback = new GoogleDocAndSheetUtility.StringOutputRef();
        yield return GoogleDocAndSheetUtility.GetTextInGoogleDoc(m_googleDocId, callback);
        DateTime e = DateTime.Now;
        m_timeToFetchTheDoc = (e - t).TotalSeconds;
        m_googleDocContentCurrent= callback.m_value;
       
        if (notifyChange )
        {
            CheckIfNeedToPushNewConcent( m_googleDocContentCurrent);
        }
    }
    private void CheckIfNeedToPushNewConcent( string foundNexText)
    {
            m_listenToDownloaded.Invoke(foundNexText);
    }
}
