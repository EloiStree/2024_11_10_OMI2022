using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_WindowNativeAction : AbstractInterpreterMono
{
    public WindowNative_FocusWindow m_windowFocus;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {

        return StartWith(ref command, "windownative:", true);
    }

    public override string GetName()
    {
        return "Window Native Interpreter";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string s = command.GetLine().Trim();

        string[] token = s.Split(':');

        if (token.Length==3 &&  token[0].ToLower().Trim() == "windownative" && token[1].ToLower().Trim() == "focus") {

            try
            {
                m_windowFocus.SetFocusToExternalApp(token[2].Trim(),0.5f);

            }
            catch (Exception e) {
                Debug.LogWarning("Humm"+e.StackTrace);
            }

        }
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(true, true, false, true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

}
