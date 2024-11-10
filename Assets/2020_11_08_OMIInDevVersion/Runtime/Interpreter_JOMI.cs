using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_JOMI : AbstractInterpreterMono
{
    public UI_ServerDropdownJavaOMI m_target;
    public JomiMacroRawRegister m_jomiMacro;
    public string m_lastReceived;
    public string m_lastSend;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        
        string cmd = command.GetLine().Trim();
        return IsShortcutCommand(cmd) || IsRawCommand(cmd) || IsJomiMacro(cmd);
    }

    private static bool IsRawCommand(string command)
    {
        return command.ToLower().Trim().StartsWith("jomiraw:");
    }

    private static bool IsShortcutCommand(string command)
    {
        return command.ToLower().Trim().StartsWith("jomi:");
    }
    private static bool IsJomiMacro(string command)
    {
        return command.ToLower().Trim().StartsWith("jomimacro:");
    }

    public override string GetName()
    {
        return "JOMI Sender";
    }

    
    
    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        m_lastReceived = cmd;
        foreach (var item in m_target.GetJavaOMISelected())
        {
            if (IsShortcutCommand(cmd)) {

                m_lastSend = cmd.Substring(5);
                item.SendShortcutCommands(cmd.Substring(5));
            }
            if (IsRawCommand(cmd))
            {
                m_lastSend = cmd.Substring(8);
                item.SendRawCommand(cmd.Substring(8));
            }
            if (IsJomiMacro(cmd))
            {
                m_lastSend = cmd.Substring(10);
                m_jomiMacro.Call(cmd.Substring(10));
            }
        }
        succedToExecute.SetAsFinished(true);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        //string cmd = command.GetLine().Trim();

        //    if (IsShortcutCommand(cmd))
        //       return "Send Shortcut to JOMI runner: "+ cmd.Substring(8);
        //    if (IsRawCommand(cmd))
        //        return "Send Raw command to JOMI runner: " + cmd.Substring(5);
        //return "Nothing because it don't seem to be command >> " + cmd;

        return "";
    }

}
