using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Midi;
public class MidiNoteToBooleanMono : MonoBehaviour
{
    public ObservedMidiNote_NameOnly[] m_observedMidiNote;


    public void PushIn(NoteEvent note) {
        if (note == null)
            return;
        int noteId = note.NoteNumber;
        bool isOn = note.CommandCode == MidiCommandCode.NoteOn;
        for (int i = 0; i < m_observedMidiNote.Length; i++)
        {
            if (noteId == m_observedMidiNote[i].m_noteId) {
                m_observedMidiNote[i].m_booleanStateListener.SetBoolean(isOn);
            }
        }
    }

}


[System.Serializable]
public class ObservedMidiNote_NameOnly
{
    public string m_reminder;
    public int m_noteId;
    public DefaultBooleanChangeListener m_booleanStateListener;

}
[System.Serializable]
public class ObservedMidiNote_NameAndChannel
{
    public int m_channel;
    public int m_noteId;
    public DefaultBooleanChangeListener m_booleanStateListener;

}
[System.Serializable]
public class ObservedMidiNote_NameVelocityAndChannel
{
    public int m_channel;
    public int m_noteId;
    public DefaultBooleanChangeListener m_booleanStateListener;

}

[System.Serializable]
public class ObservedMidiNote_NameVelocity
{
    public int m_noteId;
    public DefaultBooleanChangeListener m_booleanStateListener;

}
