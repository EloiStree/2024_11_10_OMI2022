using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_UnicodeMacro : AbstractInterpreterMono
{
    public string m_unicodeToStartWith = "Ū";
    public StringToCommandLinesRegister m_unicodeToMacro;
    public CommandLineEvent m_commandFound;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return command.GetLine().IndexOf(m_unicodeToStartWith) == 0;
    }

    public override string GetName()
    {
        return "Unicode Macro";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string value = command.GetLine().Trim().Replace(m_unicodeToStartWith,"");
        NamedListOfCommandLines namedList;
        if (m_unicodeToMacro.Get(value, out namedList)) {
            foreach (var item in namedList.GetCommands())
            {
                m_commandFound.Invoke(item);
            }
        }
        succedToExecute.SetAsFinished(true);

    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, true, true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

}
