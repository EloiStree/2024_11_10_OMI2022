using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroListRegisterMono : MonoBehaviour
{
    public NameListOfCommandLinesRegister m_macros = new NameListOfCommandLinesRegister();

    public List<TextLinkedToId> m_macroByDefault = new List<TextLinkedToId>();

    public void Awake()
    {
        for (int i = 0; i < m_macroByDefault.Count; i++)
        {
            string id= m_macroByDefault[i].m_nameId,
                macro= m_macroByDefault[i].m_text;
            m_macros.AddList(id, new NamedListOfCommandLines(id, CommandLine.GetLinesFromCharSplite(macro)));

        }
    }

    public NameListOfCommandLinesRegister GetRegister() {

        return m_macros;
    }

    private void Reset()
    {
        m_macroByDefault.Add(new TextLinkedToId("Ping", "Debug Log Pong"));
    }

}
