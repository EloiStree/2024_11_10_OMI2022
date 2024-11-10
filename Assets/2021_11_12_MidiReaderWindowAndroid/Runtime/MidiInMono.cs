using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiInMono : MonoBehaviour
{
    public string m_midiNameId = "";
    public bool m_found;
    public int m_foundId;
    public bool m_connected;
    public MidiIn m_midiIn;

    [Header("Listen")]
    public UnityMidiEventArgsEvent m_onUnityThreadEvent;
    public UnityMidiEventArgsEvent m_onListenerThreadEvent;

    public void GetSourceName(out string givenName)
    {
        givenName = m_midiNameId;
    }

    public Queue<MidiInMessageEventArgs> m_pushOnUnityThread= new Queue<MidiInMessageEventArgs>();

    [Header("Debug")]
    public Eloi.StringClampHistory m_historyDebug;
    public Eloi.StringClampHistory m_errorHistoryDebug;

    public void SetConnectionOn()
    {

        E_MidiUtility.GetMidiInIdOf(in m_midiNameId, out m_found, out m_foundId);
        if (m_found)
        {
            try
            {
                m_midiIn = new MidiIn(m_foundId);
                m_midiIn.MessageReceived += HandleValideMessage;
                m_midiIn.ErrorReceived += HandleErrorMessage;
                m_midiIn.Start();
                m_connected = true;
            }
            catch
            {

            }
        }
    }

    public MidiIn GetMidiIn()
    {
        return m_midiIn;
    }

    public bool IsUsable()
    {
        return m_found && m_connected && m_midiIn != null;
    }

    public void SetConnectionOff()
    {
        if (m_found && m_connected && m_midiIn != null)
        {
            m_midiIn.MessageReceived -= HandleValideMessage;
            m_midiIn.ErrorReceived -= HandleErrorMessage;
            m_midiIn.Stop();
            m_midiIn.Close();
            m_midiIn.Dispose();

        }
    }


    public void Update()
    {
        PushFromUnityThreadWaitingMidiEvent();
    }

    public void PushFromUnityThreadWaitingMidiEvent()
    {
        while (m_pushOnUnityThread.Count > 0)
        {
            m_onUnityThreadEvent.Invoke(m_pushOnUnityThread.Dequeue());
        }
    }

    void HandleErrorMessage(object sender, MidiInMessageEventArgs e)

    {
        m_errorHistoryDebug.PushIn(String.Format("Error: Time {0} Message 0x{1:X8} Event {2}",
           e.Timestamp, e.RawMessage, e.MidiEvent));



    }

    void HandleValideMessage(object sender, MidiInMessageEventArgs e)
    {
        m_onListenerThreadEvent.Invoke(e);
        m_pushOnUnityThread.Enqueue(e);
        MidiOneLinerDebug.GetFrom( e.MidiEvent, out string gline);
        m_historyDebug.PushIn(gline);
    }


}
