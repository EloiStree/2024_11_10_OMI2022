using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interpreter_CallMacro_Legacy : AbstractInterpreterMono
{
    public CommandAuctionExecuter m_executer;
    public MacroCoroutineExecution m_macroExecuter;
    public MacroListRegisterMono m_register;
    public TimedMacroRegister [] m_timedMacro;
    public string m_debugLastTranslate;

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        bool succed =TryToTranslate(command);
        
        succedToExecute.SetAsFinished(succed);
    }

    private bool TryToTranslate(ICommandLine command)
    {

        string cmd = command.GetLine();
        int spliterIndex = cmd.IndexOf(":");
        if (spliterIndex <= 0) { return false; }
        if (spliterIndex >= cmd.Length - 1) { return false; }

        string macroInfo = cmd.Substring(0, spliterIndex).ToLower().Replace("macro", "").Trim();
        string macroName = cmd.Substring(
            spliterIndex + 1,
            cmd.Length - (spliterIndex + 1)).Trim();

        m_debugLastTranslate = macroInfo + " > " + macroName;
        TimedCommandLines t;
        if (HasTimedMacro(macroName, out t))
        {
           // Debug.Log("Test:" + macroName);
            Execute(t);
        }
        else {
            NamedListOfCommandLines s2c;
            RegexLinkedToCommandLines r2c;
            List<ICommandLine> cmds = null;
            if (HasStringToCommandMacro(macroName, out s2c))
            {
                cmds = s2c.GetCommands();

            }
            else if (HasRegexToCommandMacro(macroName, out r2c))
            {

                cmds = r2c.GetCommandLines().ToList();
            }
            if (cmds != null && cmds.Count>0) {

                bool stepbystep = macroInfo.ToLower().IndexOf("stepbystep") >= 0;
                if (macroInfo.ToLower().IndexOf("reverse") >= 0)
                {

                    CommandLine.Reverse(ref cmds);

                }
                else if (macroInfo.ToLower().IndexOf("random") >= 0)
                {
                    CommandLine.Shuffle(ref cmds);
                }

                if (stepbystep)
                    StartCoroutine(m_executer.ExecuteStepByStep(cmds));
                else Execute(cmds);
            }

        }
        return true;
    }

    private void Execute(TimedCommandLines timedMacro)
    {
       Execute( InTimedMacro.GetCommandsFor(timedMacro));
      //  Debug.Log("TTT:" + timedMacro.m_toSend.Count);
    }

    private void Execute(List<ICommandLine> cmds)
    {
        m_executer.Execute(new ParallelsExecutionCommandLines(cmds, ParallelsExecutionCommandLines.ThreadTriggerType.StartToEnd));
    }

    public bool HasStringToCommandMacro(string callId, out NamedListOfCommandLines stringInfo) {
        return m_nameToCommandLines.Get(callId, out stringInfo);
    }
    public bool HasRegexToCommandMacro(string callId, out RegexLinkedToCommandLines  regexInfo) {
        return m_regexToCommandLines.Get(callId, out regexInfo);
    }
    public bool HasTimedMacro(string callId, out TimedCommandLines timedInfo) {

        bool found;
        for (int i = 0; i < m_timedMacro.Length; i++)
        {
            m_timedMacro[i].Get(callId, out found,  out timedInfo);
            if (found)
                return true;
        }

        timedInfo = null;
        return false;
    }

    private List<ICommandLine> GetCommandLines(string macroName)
    {
     
        List<ICommandLine> cmds;
        if(m_nameToCommandLines.GetRegister().GetCommandLinesOf(macroName, out cmds))
           return cmds;
         if(m_regexToCommandLines.GetRegister().GetCommandLinesOf(macroName, out cmds))
            return cmds;
         return new List<ICommandLine>();
    }

    public StringToCommandLinesRegister m_nameToCommandLines;
    public RegexToCommandsRegisterMono m_regexToCommandLines;

    public override string GetName()
    {
        return "Macro Call";
    }
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        string cmd = command.GetLine().ToLower().Trim();
        // return command.GetLine().ToLower().IndexOf("call macro ") == 0;
        return cmd.IndexOf("macro") == 0;// && cmd.IndexOf(":") > 0;
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = m_executionQuote;
    }
    public CommandExecutionInformation m_executionQuote = new CommandExecutionInformation( false, false, true, true);

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Try to find a stored macro, group of actions, in the register and execute it.";
    }

}

public class InTimedMacro
{
    public static List<ICommandLine> GetCommandsFor(TimedCommandLines commands)
    {
        List<ICommandLine> cmds = new List<ICommandLine>();
        for (int i = 0; i < commands.m_toSend.Count; i++)
        {
            cmds.Add(
                new CommandLine(
                    "In " + commands.m_toSend[i].m_timeInMs + "ms |" + commands.m_toSend[i].m_commandLine));
        }
        return cmds;
    }

}