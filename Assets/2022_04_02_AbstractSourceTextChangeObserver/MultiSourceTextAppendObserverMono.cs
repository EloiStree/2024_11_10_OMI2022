using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSourceTextAppendObserverMono : MonoBehaviour
{

    public RelayTextAppendChangeObserver m_relay = new RelayTextAppendChangeObserver();
    public Dictionary<string, TextAppendChangeObserver> m_sourceObserved = new Dictionary<string, TextAppendChangeObserver>();
    public List<TextAppendChangeObserver> m_debugList = new List<TextAppendChangeObserver>();

    public void PushNewText(string sourceStringId, string text, bool notifyEvent) {
        if (!m_sourceObserved.ContainsKey(sourceStringId)) {
            TextAppendChangeObserver observer = new TextAppendChangeObserver();
            observer.m_fullTextResetFound.AddListener(m_relay.m_fullTextResetFound.Invoke);
            observer.m_listenToAppendNewChars.AddListener(m_relay.m_listenToAppendNewChars.Invoke);
            observer.m_listenToAppendNewLines.AddListener(m_relay.m_listenToAppendNewLines.Invoke);
            observer.m_listenToAppendNewText.AddListener(m_relay.m_listenToAppendNewText.Invoke);
            m_sourceObserved.Add(sourceStringId, observer);
            m_debugList.Add(observer);
        }
        m_sourceObserved[sourceStringId].PushNewText(text,notifyEvent);
    }

    public void Remove(string sourceStringId) {
        if (m_sourceObserved.ContainsKey(sourceStringId)) {
            m_debugList.Remove(m_sourceObserved[sourceStringId]);
            m_sourceObserved.Remove(sourceStringId);
        }
    }
}
