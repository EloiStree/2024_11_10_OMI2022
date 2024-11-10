using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AbstractMidi_DebugLine_IMidiEventBasicGet : MonoBehaviour
{
    public InputField m_absoluteTimeDisplay;
    public InputField m_shortenDisplay;
    public InputField m_deviceNameDisplay;
    public InputField m_usedChannelDisplay;
    public InputField m_receivedTimeDisplay;

    public string m_dateFormat = "hh:mm:ss mmm";

    public void PushIn(IMidiEventBasicGet value) {

        if (value == null)
        {
            Flush();
            return;
        }
        value.GetAbsoluteTime(out long absoluteTime);
        if (m_absoluteTimeDisplay != null)
            m_absoluteTimeDisplay.text = "" + absoluteTime;
        value.GetShortenId(out int shortenId); 
        if (m_shortenDisplay != null)
            m_shortenDisplay.text = ""+shortenId;
        value.GetSourceDeviceName(out string deviceName);
        if (m_deviceNameDisplay != null)
            m_deviceNameDisplay.text = deviceName;
        value.GetUsedChannel(out int channel);
        if (m_usedChannelDisplay != null)
            m_usedChannelDisplay.text = ""+ channel;
        value.GetWhenReceived(out DateTime whenTime);
        if (m_receivedTimeDisplay != null)
            m_receivedTimeDisplay.text = whenTime.ToString(m_dateFormat);
    }
   

    public void Flush()
    {
        if (m_absoluteTimeDisplay != null)
            m_absoluteTimeDisplay.text = "";
        if (m_shortenDisplay != null)
            m_shortenDisplay.text = "";
        if (m_deviceNameDisplay != null)
            m_deviceNameDisplay.text = "";
        if (m_usedChannelDisplay != null)
            m_usedChannelDisplay.text = "";
        if (m_receivedTimeDisplay != null)
            m_receivedTimeDisplay.text = "";
    }
}
