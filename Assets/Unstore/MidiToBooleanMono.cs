using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiToBooleanMono : MonoBehaviour
{

    public List<MidiToBooleanObserved.NoteNotChannel> m_noteNoChannel = new List<MidiToBooleanObserved.NoteNotChannel>();
    public List<MidiToBooleanObserved.NoteVelocityNoChannel> m_noteNoChannelVelocity = new List<MidiToBooleanObserved.NoteVelocityNoChannel>();
    public List<MidiToBooleanObserved.ShortenId> m_shortenId = new List<MidiToBooleanObserved.ShortenId>();
    public List<MidiToBooleanObserved.ControlCommand> m_controlCommand = new List<MidiToBooleanObserved.ControlCommand>();


    public void Clear()
    {
        m_noteNoChannel.Clear();
        m_noteNoChannelVelocity.Clear();
        m_shortenId.Clear();
        m_controlCommand.Clear();
    }


    public void Append(MidiToBooleanObserved.NoteNotChannel value)
    {
        m_noteNoChannel.Add(value);
    }
    public void Append(MidiToBooleanObserved.NoteVelocityNoChannel value)
    {
        m_noteNoChannelVelocity.Add(value);
    }
    public void Append(MidiToBooleanObserved.ShortenId value)
    {
        m_shortenId.Add(value);
    }
    public void Append(MidiToBooleanObserved.ControlCommand value)
    {
        m_controlCommand.Add(value);
    }
}
