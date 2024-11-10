using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFTTT_StringEvent : I_IFTTTStringEvent
{
    public string m_eventStringId;
    public void GetStringEventID(out string stringEventId)
    {
        stringEventId = m_eventStringId;
    }
}
