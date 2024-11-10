using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Midi_PitchWheel : MonoBehaviour
{

    public InputField m_displayAbsoluteTime;
    public InputField m_displayChannel;
    public InputField m_displayCommandCode;
    public InputField m_displayDeltaTime;
    public InputField m_displayGetAsShortMessage;
    public InputField m_displayPitch;
    public InputField m_displayType;
    public Slider m_pitchState;
    public int m_maxPitch= 16383;

    public void PushIn(MidiInMessageEventArgs value)
    {
        PushIn(value.MidiEvent);
    }
    public void PushIn(MidiEvent value)
    {
        PitchWheelChangeEvent note = value as PitchWheelChangeEvent;
        DisplayNote(note);
    }

    private void DisplayNote(PitchWheelChangeEvent note)
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
            if (m_displayPitch)
                m_displayPitch.text = "" + note.Pitch;
            if (m_displayType)
                m_displayType.text = "" + note.GetType();

            if (m_pitchState) {
                m_pitchState.value = note.Pitch /(float) m_maxPitch;
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
            if (m_displayPitch)
                m_displayPitch.text = "";
            if (m_pitchState)
            {
                m_pitchState.value = 0;
            }
        }
    }
}

