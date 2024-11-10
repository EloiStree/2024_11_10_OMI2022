using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Midi_InOutList : MonoBehaviour
{
    public string[] midiInList;
    public string[] midiOutList;
    
    public Dropdown m_inDevice;
    public InputField m_inDeviceCopyPastable;
    
    public Dropdown m_outDevice;
    public InputField m_outDeviceCopyPastable;

    public void RefreshUI() {

        E_MidiUtility.GetListOfMidiInDevices(out midiInList);
        E_MidiUtility.GetListOfMidiInDevices(ref m_inDevice);
        E_MidiUtility.GetListOfMidiOutDevices(out midiOutList);
        E_MidiUtility.GetListOfMidiOutDevices(ref m_outDevice);
        if(m_inDeviceCopyPastable)
            m_inDeviceCopyPastable.text = string.Join(" , ", midiInList);
        if (m_outDeviceCopyPastable)
            m_outDeviceCopyPastable.text = string.Join(" , ", midiInList);

    }
}
