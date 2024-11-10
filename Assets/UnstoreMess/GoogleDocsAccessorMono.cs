using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleDocsAccessorMono : MonoBehaviour
{
    public Eloi.PrimitiveUnityEvent_String m_listenToDownloaded = new Eloi.PrimitiveUnityEvent_String();
    public Eloi.PrimitiveUnityEvent_DoubleString m_listenToGoogleIdDownloaded = new Eloi.PrimitiveUnityEvent_DoubleString();

    public void Clear()
    {
        m_dynamicAccessors.Clear();
    }

    public List<string> m_googleIdToListenTo= new List<string>();
    public List<GoogleDocObserverAccessor> m_dynamicAccessors = new List<GoogleDocObserverAccessor>();
    public List<GoogleDocObserverAccessor> m_editorAccessors = new List<GoogleDocObserverAccessor>();



    public void Awake()
    {
        foreach (GoogleDocObserverAccessor item in m_editorAccessors)
        {
            HookEvents(item);
        }
        foreach (string id in m_googleIdToListenTo)
        {
            AddGoogleDocToObserve(id,false);
        }
    }

    private void HookEvents( GoogleDocObserverAccessor accessor)
    {
        accessor.m_listenToDownloaded.AddListener(m_listenToDownloaded.Invoke);
        accessor.m_listenToDownloaded.AddListener(
            (string text) => { m_listenToGoogleIdDownloaded.Invoke(accessor.m_googleDocId, text); }
            );
    }

    public void AddGoogleDocToObserve(string googleDocId, bool loadAtStart)
    {
        RemoveGoogleDocToObserve(googleDocId);
        GoogleDocObserverAccessor accessor = new GoogleDocObserverAccessor(googleDocId);
        HookEvents(accessor);
        m_dynamicAccessors.Add(accessor);
        if (loadAtStart)
          StartCoroutine(  accessor.FetchGoogleDocWithCoroutine(false) );

    }
    public void RemoveGoogleDocToObserve(string googleDocId)
    {
        for (int i = m_dynamicAccessors.Count-1; i>=0 ; i--)
        {
            if (Eloi.E_StringUtility.AreEquals(googleDocId, m_dynamicAccessors[i].m_googleDocId, false, true))
            {
                m_dynamicAccessors.RemoveAt(i);
            }
        }
    }



    [ContextMenu("Refresh with no notification")]
    public void RefreshAllWithCoroutineAndDontNotification()
    {
        RefreshAllWithCoroutine(false);
    }
    [ContextMenu("Refresh with  notification")]
    public void RefreshAllWithCoroutineAndNotifyNotification()
    {
        RefreshAllWithCoroutine(true);
    }
    public void RefreshAllWithCoroutine(bool notifyWhenRefresh) {

        StartCoroutine(RefreshAll(notifyWhenRefresh));
    }

    public IEnumerator RefreshAll(bool notifyWhenRefresh) {

        foreach (GoogleDocObserverAccessor item in m_dynamicAccessors)
        {
            yield return item.FetchGoogleDocWithCoroutine(notifyWhenRefresh);
        }
        foreach (GoogleDocObserverAccessor item in m_editorAccessors)
        {
            yield return item.FetchGoogleDocWithCoroutine(notifyWhenRefresh);
        }
    }
}
