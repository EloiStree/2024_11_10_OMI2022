using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interpreter_DebugLog : AbstractInterpreterMono
{
    public CommandExecutionInformation m_executionQuote = new CommandExecutionInformation(true, false, false, true);
    public Text m_uiDebugConsole;
    public Text m_uiDebugLastLine;

    public bool m_useUnityDebugConsole;
    public override string GetName()
    {
        return "Debug Log";
    }
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return command.GetLine().ToLower().Trim().IndexOf("debug log ") == 0 || command.GetLine().Trim().ToLower().IndexOf("debuglog ") == 0;

    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        bool goodFormat = CanInterpreterUnderstand(ref command);
        if (goodFormat) {
            string msg = command.GetLine();
            if (msg.ToLower().Trim().IndexOf("debug log ") == 0)
                msg = msg.Substring("Debug Log ".Length);
            if (msg.ToLower().Trim().IndexOf("debuglog ") == 0)
                msg = msg.Substring("DebugLog ".Length);

            if (m_uiDebugLastLine != null)
                m_uiDebugLastLine.text = msg;
            if (m_uiDebugConsole != null)
                m_uiDebugConsole.text = msg+"\n"+ m_uiDebugConsole.text;
            if (m_uiDebugConsole.text.Length > 500)
                m_uiDebugConsole.text = m_uiDebugConsole.text.Substring(0, 500);
         if(m_useUnityDebugConsole)
            Debug.Log(msg);
        }
        succedToExecute.SetAsFinished(goodFormat);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = m_executionQuote;
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Display the given message to Unity editor console";
    }
}
