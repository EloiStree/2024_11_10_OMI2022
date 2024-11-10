using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMIAbstraction
{

    public interface IMidiConnectionOut
    {
        void GetMidiConnectionOutIdName(out string idName);
    }
    [System.Serializable]
    public class MidiConnectionOut : IMidiConnectionOut
    {
        public string m_connectionMidiIdName;

        public MidiConnectionOut(string connectionMidiIdName)
        {
            m_connectionMidiIdName = connectionMidiIdName;
        }

        public void GetMidiConnectionOutIdName(out string idName)
        {
            idName = m_connectionMidiIdName;
        }
    }


}