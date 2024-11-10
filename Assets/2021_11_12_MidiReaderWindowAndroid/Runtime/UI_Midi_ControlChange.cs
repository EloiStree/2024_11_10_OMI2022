using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Midi_ControlChange : MonoBehaviour
{

    public InputField m_displayAbsoluteTime;
    public InputField m_displayChannel;
    public InputField m_displayCommandCode;
    public InputField m_displayDeltaTime;
    public InputField m_displayGetAsShortMessage;
    public InputField m_displayController;
    public InputField m_displayControllerValue;
    public InputField m_displayType;

   
    public void PushIn(ControlChangeEvent value)
    {
        DisplayNote(value);
    }

    private void DisplayNote(ControlChangeEvent value)
    {
        if (value != null)
        {
            if (m_displayAbsoluteTime)
                m_displayAbsoluteTime.text = "" + value.AbsoluteTime;
            if (m_displayChannel)
                m_displayChannel.text = "" + value.Channel;
            if (m_displayCommandCode)
                m_displayCommandCode.text = "" + value.CommandCode;
            if (m_displayDeltaTime)
                m_displayDeltaTime.text = "" + value.DeltaTime;
            if (m_displayGetAsShortMessage)
                m_displayGetAsShortMessage.text = "" + value.GetAsShortMessage();
            if (m_displayController)
                m_displayController.text = "" + value.Controller;
            if (m_displayControllerValue)
                m_displayControllerValue.text = "" + value.ControllerValue;
            if (m_displayType)
                m_displayType.text = "" + value.GetType(); 
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
            if (m_displayController)
                m_displayController.text = "";
            if (m_displayControllerValue)
                m_displayControllerValue.text = "";
            if (m_displayType)
                m_displayType.text = "";

        }
    }
}
