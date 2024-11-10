using Eloi;
using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_PushStuffs : MonoBehaviour
{
    public MidiOutMono m_connectionMono;

    public int m_tempo = 100;

    public StringClampHistory m_sentHistory;




    public void SendTempoEvent()
    {
        if (m_connectionMono.IsUsable())
        {
            MidiOut midiOut = m_connectionMono.GetMidiOut();
            var tempoEVent = new TempoEvent(m_tempo, 0);
            midiOut.Send(tempoEVent.GetAsShortMessage());
            m_sentHistory.PushIn("" + tempoEVent.GetAsShortMessage());
        }
    }
    public void SendMidiEvent()
    {
        if (m_connectionMono.IsUsable())
        {
            MidiOut midiOut = m_connectionMono.GetMidiOut();
            var tempoEVent = new TempoEvent(m_tempo, 0);
            midiOut.Send(tempoEVent.GetAsShortMessage());
            m_sentHistory.PushIn("" + tempoEVent.GetAsShortMessage());
        }
    }
    public void SendMidiCommandEvent()
    {

    }
    public void SendMetaEvent()
    {

    }
    public void SendTextEvent()
    {

    }
}
