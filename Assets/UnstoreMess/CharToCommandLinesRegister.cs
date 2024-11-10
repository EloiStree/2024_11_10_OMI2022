using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharToCommandLinesRegister : MonoBehaviour
{
    public CharListOfCommandLinesRegister m_charToCommands = new CharListOfCommandLinesRegister();

    public int m_count;
    public List<DebugS2C> m_cmds = new List<DebugS2C>();

    public void AddToCommands(char startWith, string text, char spliter)
    {
        AddToCommands(startWith, text.Split(spliter));
    }
    public void AddToCommands(char startWith, string[] lines)
    {
        AddToCommands(new NamedListOfCommandLines(""+startWith, lines));

    }
    private void Refresh()
    {
        m_count = m_charToCommands.GetCount();
        CharToCommands [] lines = m_charToCommands.GetNameRegisteredFull();
        m_cmds.Clear();
        for (int i = 0; i < lines.Length; i++)
        {
            m_cmds.Add(new DebugS2C(""+lines[i].m_valueId, lines[i].GetCommands().Select(k => k.GetLine()).ToArray()));

        }
    }

    public void Clear()
    {
        m_charToCommands.Clear();
    }

    public void AddToCommands(NamedListOfCommandLines rc)
    {
        if (rc.GetIdentifiantName().Length > 0) {
            char c = rc.GetIdentifiantName()[0];
            m_charToCommands.AddList(c,new CharToCommands( c, rc.GetCommands().Select(k=>k.GetLine() ).ToArray() ) );
            Refresh();
        }
    }

    public CharListOfCommandLinesRegister GetRegister()
    {
        return m_charToCommands;
    }
    public void AddToCommands(CharToCommands rc)
    {
        AddToCommands( rc.m_valueId,  rc.m_commands.Select(k=>k.ToString()).ToArray());
    }
    public void AddToCommands(StringToCommands rc)
    {
        AddToCommands(new NamedListOfCommandLines(rc.m_startMatch, rc.m_commands));
    }
    [System.Serializable]
    public class DebugS2C
    {
        public string id;
        public string[] cmds;

        public DebugS2C(string id, string[] cmds)
        {
            this.id = id;
            this.cmds = cmds;
        }
    }

    public bool Get(char callId, out List<ICommandLine> stringInfo)
    {
        return m_charToCommands.GetCommandLinesOf(callId, out stringInfo);
    }
}

