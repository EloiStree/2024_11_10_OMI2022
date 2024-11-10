using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiListenToShortMessagesMono : MonoBehaviour
{

    public GroupOfShortenIdsToAction[] m_shortenListeners;

    public void PushIn(int shortMidi) {

        for (int i = 0; i < m_shortenListeners.Length; i++)
        {
            m_shortenListeners[i].PushIn(shortMidi);
        }
    }
}

[System.Serializable]
public class GroupOfShortenIdsToAction {

    public UnityEvent m_actionToDo;
    public GroupOfMidiShortIds m_shortenIds;

    public void PushIn(in int shortMidi)
    {

        foreach (int id in m_shortenIds.GetOwnShortIds())
        {
            if (shortMidi == id)
            {
                m_actionToDo.Invoke();
                return;
            }
        }
    }
}