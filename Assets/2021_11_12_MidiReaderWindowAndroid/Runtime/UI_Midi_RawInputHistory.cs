using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Midi_RawInputHistory : MonoBehaviour
{

    public InputField [] m_history;
    public void AppendNewValue(MidiEvent value)
    {
        AppendNewValue(value.GetAsShortMessage());

    }
    public void AppendNewValue(IMidiShortWithSourceGet value)
    {
        value.GetShortId(out int midishort);
        value.GetSource(out IMidiInSourceGet source);
        source.GetSourceDeviceName(out string deviceName);
        AppendNewValue(midishort+"|"+deviceName );

    }
    public void AppendNewValue(string value)
    {
        for (int i = m_history.Length - 2; i >= 0; i--)
        {
            m_history[i + 1].text = m_history[i].text;
        }
        m_history[0].text = "" + value;

    }
    public void AppendNewValue(int value)
    {
        for (int i = m_history.Length-2; i >=0; i--)
        {
            m_history[i + 1].text = m_history[i].text;
        }
        m_history[0].text = "" + value;

    }
    public void Clear()
    {
        for (int i = 0; i < m_history.Length ; i++)
        {
            m_history[i].text = "";
        }

    }


}
