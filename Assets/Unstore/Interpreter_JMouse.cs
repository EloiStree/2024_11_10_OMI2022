using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_JMouse : AbstractInterpreterMono
{
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "jmouse:", true);
    }

    public override string GetName()
    {
        return "Java Mouse Adaptateur";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim().ToLower();
        string[] tokens = cmd.Split(':');
        

        succedToExecute.SetAsFinished(true);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation();
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

 
}
