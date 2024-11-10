using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio;
using NAudio.Midi;
using UnityEngine.UI;

public class UI_Midi_NoteOnOff : MonoBehaviour
{

    public InputField m_displayAbsoluteTime;
    public InputField m_displayChannel;
    public InputField m_displayCommandCode;
    public InputField m_displayDeltaTime;
    public InputField m_displayGetAsShortMessage;
    public InputField m_displayNoteLength;
    public InputField m_displayNoteName;
    public InputField m_displayNoteNumber;
    public InputField m_displayVelocity;
    public InputField m_displayType;
    public Toggle m_toggleState;


    public void PushIn(MidiInMessageEventArgs value)
    {
        PushIn(value.MidiEvent);
    }
    public void PushIn(MidiEvent value)
    {
        NoteEvent note = value as NoteEvent;
        DisplayNote( note);

    }

    private void DisplayNote( NoteEvent note)
    {
        if (note != null)
        {
            if (m_displayAbsoluteTime)
                m_displayAbsoluteTime.text = "" + note.AbsoluteTime;
            if (m_displayChannel)
                m_displayChannel.text = "" + note.Channel;
            if (m_displayCommandCode)
                m_displayCommandCode.text = "" + note.CommandCode;
            if (m_displayDeltaTime)
                m_displayDeltaTime.text = "" + note.DeltaTime;
            if (m_displayGetAsShortMessage)
                m_displayGetAsShortMessage.text = "" + note.GetAsShortMessage();
            if (m_displayNoteName)
                m_displayNoteName.text = "" + note.NoteName;
            if (m_displayNoteNumber)
                m_displayNoteNumber.text = "" + note.NoteNumber;
            if (m_displayVelocity)
                m_displayVelocity.text = "" + note.Velocity;
            if (m_displayType)
                m_displayType.text = "" + note.GetType();

            if (m_toggleState)
            {
                m_toggleState.isOn = note.CommandCode == MidiCommandCode.NoteOn;
            }

            NoteOnEvent noteOn = note as NoteOnEvent;
            if (noteOn != null)
            {
                if (m_displayNoteLength)
                    m_displayNoteLength.text = "" + noteOn.NoteLength;
            }
            else { 
                if (m_displayNoteLength)
                m_displayNoteLength.text = "" ;
            }
        }
        else
        {

            if (m_displayAbsoluteTime)
                m_displayAbsoluteTime.text = "";
            if (m_displayChannel)
                m_displayChannel.text = "";
            if (m_displayCommandCode)
                m_displayCommandCode.text = "";
            if (m_displayDeltaTime)
                m_displayDeltaTime.text = "";
            if (m_displayGetAsShortMessage)
                m_displayGetAsShortMessage.text = "";
            if (m_displayNoteLength)
                m_displayNoteLength.text = "";
            if (m_displayNoteName)
                m_displayNoteName.text = "";
            if (m_displayNoteNumber)
                m_displayNoteNumber.text = "";
            if (m_displayVelocity)
                m_displayVelocity.text = "";
            if (m_displayNoteLength)
                m_displayNoteLength.text = "" ;
            if (m_toggleState)
            {
                m_toggleState.interactable = false;
                m_toggleState.isOn = false;
            }

        }
    }
}
