using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMacroRegister : MonoBehaviour
{
    public Dictionary<string, TimedCommandLines> m_registered = new Dictionary<string, TimedCommandLines>();
    public List<TimedCommandLines> m_debug = new List<TimedCommandLines>();
    public void Clear()
    {
        m_registered.Clear();
    }

    public void Add(TimedCommandLines macro) {

        if (m_registered.ContainsKey(macro.m_callId)) { 
            m_registered[macro.m_callId] = macro;
        }
        else { 
            m_registered.Add(macro.m_callId, macro);
            m_debug.Add(macro);
        }
        

    }


    public void Get(string name, out bool found, out TimedCommandLines foundCommandLines) {
        found = false;
        foundCommandLines = null;
        if (m_registered.ContainsKey(name)) {
            found = true;
            foundCommandLines = m_registered[name];
        }

    }

    public bool HasMacro(string callId)
    {
        return m_registered.ContainsKey(callId);
    }

    public bool Get(string callId, out TimedCommandLines timedInfo)
    {
        bool found;
        Get(callId, out found, out timedInfo);
        return found;

    }
}
