using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StringToCommandLinesRegister : MonoBehaviour
{
    public NameListOfCommandLinesRegister m_stringToCommands = new NameListOfCommandLinesRegister();

    public int m_count;
    public List<DebugS2C> m_cmds= new List<DebugS2C>();

    public void AddToCommands(string startWith, string text, char spliter)
    {
        AddToCommands(startWith, text.Split(spliter));
    }
    public void AddToCommands(string startWith, string[] lines)
    {
        AddToCommands(new NamedListOfCommandLines(startWith, lines));

    }
    private void Refresh()
    {
        m_count = m_stringToCommands.GetCount() ;
        NamedListOfCommandLines[] lines = m_stringToCommands.GetNameRegisteredFull();
        m_cmds.Clear();
        for (int i = 0; i < lines.Length; i++)
        {
            m_cmds.Add(new DebugS2C(lines[i].GetIdentifiantName(), lines[i].GetCommands().Select(k => k.GetLine()).ToArray()));

        }
    }

    public void Clear()
    {
        m_stringToCommands.Clear();
    }

    public void AddToCommands(NamedListOfCommandLines rc)
    {

        m_stringToCommands.AddList(rc.GetIdentifiantName(),rc);
        Refresh();
    }

    public NameListOfCommandLinesRegister GetRegister()
    {
        return m_stringToCommands;
    }

    public void AddToCommands(StringToCommands rc)
    {
        AddToCommands( new NamedListOfCommandLines(rc.m_startMatch, rc.m_commands));
    }
    [System.Serializable]
    public class DebugS2C {
        public string id;
        public string[] cmds;

        public DebugS2C(string id, string[] cmds)
        {
            this.id = id;
            this.cmds = cmds;
        }
    }

    public bool Get(string callId, out NamedListOfCommandLines stringInfo)
    {
        return m_stringToCommands.GetCommandLinesOf(callId, out  stringInfo);
    }
}

