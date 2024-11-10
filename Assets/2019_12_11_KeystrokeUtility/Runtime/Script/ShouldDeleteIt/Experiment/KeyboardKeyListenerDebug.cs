using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKeyListenerDebug : MonoBehaviour {

    [Header("Debug")]
    public InputField m_input;

    public bool m_useDebugLog;

    public void DebugDisplayDown(KeyboardTouch element)
    {
        DebugDisplay(element, true);
    }
    public void DebugDisplayUp(KeyboardTouch element)
    {
        DebugDisplay(element, false);
    }

    public void DebugDisplay(KeyboardTouch element, bool isOn)
    {
        string debug = string.Format("{0:0000000}({1}): {2}\n", ((int)(Time.time * 1000f)), isOn, element);
        if (m_input)
            m_input.text = debug + m_input.text;
        if(m_useDebugLog)
            Debug.Log(debug);
        
    }
}
