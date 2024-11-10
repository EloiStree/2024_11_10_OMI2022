using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMacroBasedOnCharMono : MonoBehaviour
{
    public StringToCommandLinesRegister m_stringToCommands;
    public CommandLineEvent m_repushCommand;
    public void PushFromChar(char character) {
        m_stringToCommands.Get("" + character, out NamedListOfCommandLines commands);
        if (commands != null) { 
            foreach (var item in commands.GetCommands())
            {
                m_repushCommand.Invoke(item);
            }
        }
    }
}
