using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandAuctionDistributorUIDebugger: MonoBehaviour
{
    public InputField m_commandToExecute;
    public InputField m_commandWithoutTaker;

    public void InterpreterFound(IInterpreter interpret, ICommandLine command)
    {
        if(m_commandToExecute != null)
        { 
            m_commandToExecute.text = command.GetLine()+"\n"+m_commandToExecute.text;
            if (m_commandToExecute.text.Length > 1000)
                m_commandToExecute.text = m_commandToExecute.text.Substring(0, 1000);
        }
    }

    public void InterpreterNotFound(ICommandLine command)
    {
        if (m_commandWithoutTaker != null)
            m_commandWithoutTaker.text = command.GetLine();

    }
}
