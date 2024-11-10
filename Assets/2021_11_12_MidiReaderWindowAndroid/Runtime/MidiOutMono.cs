using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiOutMono : MonoBehaviour
{
    public string m_midiNameId="";
    public bool m_found;
    public int m_foundId;
    public bool m_connected;
    public MidiOut m_midiOut;

    public Eloi.StringClampHistory m_historyDebug;

    public void SetConnectionOn() {

        E_MidiUtility.GetMidiOutIdOf(in m_midiNameId, out m_found,out  m_foundId);
        if (m_found)
        {
            try
            {
                m_midiOut = new MidiOut(m_foundId);
                m_connected = true;
            }
            catch { 
            
            }
        }
    }

    public MidiOut GetMidiOut()
    {
        return m_midiOut;
    }

    public bool IsUsable()
    {
        return m_found && m_connected && m_midiOut != null;

    }

    public void SendCommandAsRawInt(int cmdRaw)
    {
        if (IsUsable()) {

            m_historyDebug.PushIn("" + cmdRaw);
            m_midiOut.Send(cmdRaw);
        }
    }

    public void SetConnectionOff() {
        if (m_found && m_connected && m_midiOut != null) {
            m_midiOut.Close();
            m_midiOut.Dispose();

        }
    }

}

