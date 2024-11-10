using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AbstractMidi_DebugLine_IMidiEventNamedElementGet : MonoBehaviour
{

    public InputField m_idDisplay;
    public InputField m_nameDisplay;

    public void PushIn(IMidiEventNamedElementGet value)
    {
        if (value == null) {
            Flush();
            return;
        }

        value.GetGivenName(out string givenName);
        if (m_idDisplay != null)
            m_idDisplay.text = givenName;
        value.GetNameAsInteger(out int nameId);
        if (m_nameDisplay != null)
            m_nameDisplay.text =""+ nameId;
    }

    public void Flush()
    {
        if (m_idDisplay != null)
            m_idDisplay.text = "";
        if (m_nameDisplay != null)
            m_nameDisplay.text = "";
    }
}
