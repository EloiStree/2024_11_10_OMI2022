using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMIAbstraction {

    public interface IMidiConnectionIn {
        void GetMidiConnectionInIdName(out string idName);
    }
    [System.Serializable]

    public class MidiConnectionIn : IMidiConnectionIn
    {
        public string m_connectionMidiIdName;

        public MidiConnectionIn(string connectionMidiIdName)
        {
            m_connectionMidiIdName = connectionMidiIdName;
        }

        public void GetMidiConnectionInIdName(out string idName)
        {
            idName= m_connectionMidiIdName ;
        }
    }


}
