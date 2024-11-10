using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Interpreter_ExecutableFile : AbstractInterpreterMono
{
    public ExecutablePathManager m_executableInTheProject;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return IsValide(command);
    }

    private static bool IsValide(ICommandLine command)
    {
        return command.GetLine().ToLower().Trim().IndexOf("exe:") == 0;
    }

    public override string GetName()
    {
        return "Executable Interpreter";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        if (!IsValide(command)) {
            succedToExecute.StopWithError("Command is not valide");
                return ;
        }
        string cmd = command.GetLine().Trim().Substring("exe:".Length);
        string [] tokens =cmd.Split(':');
        if (tokens.Length == 0)
        {
            succedToExecute.StopWithError("Command is not valide");
            return;
        }
      

        List<string> args = tokens.ToList();
        if (args.Count == 1)
        {
            m_executableInTheProject.TryToLaunch(args[0], true);

        }
        if (args.Count >= 2)
        {
            string type = args[0].ToLower().Trim();
            string value = args[1].Trim();
            bool hide = Eloi.E_StringUtility.AreEquals(type, "hide") || Eloi.E_StringUtility.AreEquals(type, "h");

            if (args.Count == 2)
            { 
                m_executableInTheProject.TryToLaunch(value, hide );
            }
            else {

                args.RemoveAt(0);
                args.RemoveAt(0);
                m_executableInTheProject.TryToLaunch(value, hide, args);
            }

        }

    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Try to launch an executable file store near the project.";
    }

    
}
