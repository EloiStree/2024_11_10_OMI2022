using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This code is so urgly... Oh my god...
public class GoogleDocsLoopAccessorMono : MonoBehaviour
{

    public Eloi.PrimitiveUnityEvent_String m_listenToDownloaded = new Eloi.PrimitiveUnityEvent_String();

    public List<GoogleDocsLoopObserverTemp> m_observeDoc = new List<GoogleDocsLoopObserverTemp>();

    public void Clear() {
        m_observeDoc.Clear();
    }

    public void Add(string id, float timeInSeconds) {

        Remove(id);
        GoogleDocsLoopObserverTemp tmp = new GoogleDocsLoopObserverTemp(id);
        tmp.m_secondsBetweenCheck = timeInSeconds;
        tmp.m_leftTimeBetweenNextCheck = timeInSeconds;
        m_observeDoc.Add(tmp);
        tmp.m_googleDoc.m_listenToDownloaded.AddListener(m_listenToDownloaded.Invoke);

        StartCoroutine(tmp.m_googleDoc.FetchGoogleDocWithCoroutine(false));
    }

    private void Remove(string id)
    {
        for (int i = m_observeDoc.Count-1; i > -1; i--)
        {
            if (Eloi.E_StringUtility.AreEquals(m_observeDoc[i].m_googleDocId , id , false , true ))
            m_observeDoc.RemoveAt(i);
        }
    }


    public void Update()
    {
        float t = Time.deltaTime; 
        for (int i = 0; i < m_observeDoc.Count; i++)
        {
            m_observeDoc[i].m_leftTimeBetweenNextCheck -= t;
            if (m_observeDoc[i].m_leftTimeBetweenNextCheck < 0f)
            {
                m_observeDoc[i].m_leftTimeBetweenNextCheck = m_observeDoc[i].m_secondsBetweenCheck;
               StartCoroutine( m_observeDoc[i].m_googleDoc.FetchGoogleDocWithCoroutine(true));
            }
        }
    }
}

[System.Serializable]
public class GoogleDocsLoopObserver
{
    public string m_googleDocId;
    public float m_secondsBetweenCheck;
}
[System.Serializable]
public class GoogleDocsLoopObserverTemp : GoogleDocsLoopObserver
{
    public float m_leftTimeBetweenNextCheck;
    public GoogleDocObserverAccessor m_googleDoc;

    public GoogleDocsLoopObserverTemp(string id) {
        m_googleDoc = new GoogleDocObserverAccessor(id);
        m_googleDocId= id;
    }
}
