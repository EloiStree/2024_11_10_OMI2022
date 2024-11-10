using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageMidiInListenersMono : MonoBehaviour
{
    public Transform m_midiInParent;
    public GameObject m_prefabListener;
    public List<MidiInListenerObject> m_midiInListener= new List<MidiInListenerObject>();

    [System.Serializable]
    public class MidiInListenerObject {
        public string m_midiInIdName;
        public GameObject m_createdListener;
    }


    public void StartListeningTo(string midiName)
    {
        if (ContainsMidi(midiName)) {
            RemoveMidi(midiName);
        }
        CreateMidi(midiName);
    }

    public void Clear()
    {
        foreach (var item in m_midiInListener)
        {
            
            DestroyImmediate(item.m_createdListener.gameObject);
        }
        m_midiInListener.Clear();
    }

    private void CreateMidi(string midiName)
    {
        GameObject created = GameObject.Instantiate(m_prefabListener, m_midiInParent);
        created.transform.parent = m_midiInParent;

        m_midiInListener.Add(new MidiInListenerObject()
        {
            m_createdListener = created
           ,m_midiInIdName = midiName
        }) ;

        created.name = "Midi In: " + midiName;
        MidiInMono inMono = created.GetComponent<MidiInMono>();
        inMono.m_midiNameId = midiName;
        inMono.SetConnectionOff();
        inMono.SetConnectionOn();

        created.SetActive(true);

    }

    private void RemoveMidi(string midiName)
    {
        for (int i = m_midiInListener.Count-1; i >=0; i--)
        {
            if (Eloi.E_StringUtility.AreEquals(
           in m_midiInListener[i].m_midiInIdName,
           in midiName, true, true)) {
                m_midiInListener.RemoveAt(i);
               
            }
        }
    }

    public void StopListeningTo(string midiName)
    {
        RemoveMidi(midiName);
    }

    public bool ContainsMidi(string midiName) {
       return m_midiInListener.Where(k => Eloi.E_StringUtility.AreEquals(
           in k.m_midiInIdName,
           in midiName, true, true)).Count()>0;
    }
}
