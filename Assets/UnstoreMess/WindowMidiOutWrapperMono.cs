using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMidiOutWrapperMono : MonoBehaviour
{
    public MidiOutMono m_midiOut;



    public void SendNoteRaw(int channel, int noteNumber, int velocity, int durationInMilliseconds, long time = 0)
    {
        if (m_midiOut.IsUsable())
        {
            var noteOnEvent = new NoteOnEvent(time, channel, noteNumber, velocity, durationInMilliseconds);
            m_midiOut.SendCommandAsRawInt(noteOnEvent.GetAsShortMessage());
        }
    }


    private void Reset()
    {
        m_midiOut = GetComponent<MidiOutMono>();
    }
}
