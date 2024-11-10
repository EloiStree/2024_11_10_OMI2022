using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_ConnectMidiIn : MonoBehaviour
{
    public ManageMidiInListenersMono m_target;
    public string m_listenToMidiInName="MPK mini play";
    // Start is called before the first frame update
    void Start()
    {
        m_target.StartListeningTo(m_listenToMidiInName);
    }

}
