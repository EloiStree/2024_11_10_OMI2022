using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_CharToActions : AbstractInterpreterMono
{

    public Eloi.PrimitiveUnityEvent_Char m_onCharEvent;
    public string m_lastFound;
    //public Eloi.PrimitiveUnityEvent_String m_onUnicodeCharEvent;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return command.GetLine().Length==1;
    }

    public override string GetName()
    {
        return "Char macro";
    }



    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmdText = command.GetLine();
        m_lastFound = cmdText;
        if (cmdText.Length == 1) {
            char c = cmdText[0];
            m_onCharEvent.Invoke(c);
        }

        succedToExecute.SetAsFinished(true);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {

        return "";
    }
}


