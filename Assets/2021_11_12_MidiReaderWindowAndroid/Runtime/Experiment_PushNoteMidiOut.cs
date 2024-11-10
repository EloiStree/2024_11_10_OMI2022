using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_PushNoteMidiOut : MonoBehaviour
{
    public MidiOutMono m_connectionMono;

    public long m_abosluteTime=0;
    [Range(1,16)]
    public int m_channel = 1;
    [Range(0,127)]
    public int m_noteNumber = 50;
    public int m_duration= 500;
    [Range(0, 127)]
    public int m_velocity=100;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_connectionMono.IsUsable())
        {
            SendNote();
        }
    }

    public void SendNote()
    {
        if (m_connectionMono.IsUsable())
        {
            var noteOnEvent = new NoteOnEvent(0, m_channel, m_noteNumber, m_velocity, m_duration);
            m_connectionMono.SendCommandAsRawInt(noteOnEvent.GetAsShortMessage());
        }
    }
    public void SendNoteOnOff(bool isOn)
    {
        if (m_connectionMono.IsUsable())
        {
            MidiCommandCode cmd = isOn ? MidiCommandCode.NoteOn : MidiCommandCode.NoteOff;
            var noteOnEvent = new NoteEvent(0, m_channel, cmd, m_noteNumber, m_velocity);
            m_connectionMono.SendCommandAsRawInt(noteOnEvent.GetAsShortMessage());
        }
    }
}
