using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToCommandLines : MonoBehaviour
{
    public StringToCommandLinesRegister m_register;
    public CommandLineEvent m_commandsFound;

    
    public void TryToPush(string textId)
    {
        NamedListOfCommandLines commands;
        if (m_register.Get(textId, out commands )) {

            foreach (var item in commands.GetCommands())
            {
                m_commandsFound.Invoke(item);
            }
        }
        
    }
}
