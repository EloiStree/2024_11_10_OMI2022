using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandTimeRelayInterpreterMono : AbstractInterpreterMono {
    public TimeThreadMono m_timer;
    public CommandLineEvent m_commandToExecute;

    public void SendAction(string rest)
    {
        m_commandToExecute.Invoke(new CommandLine(rest));
    }
    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, true, true);
    }

}

public class Interpreter_Ricochet : CommandTimeRelayInterpreterMono
{
   
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "ricochet ", true) && command.GetLine().IndexOf(":")>0;
    }

    public override string GetName()
    {
        return "Ricochet Interpret";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        bool has;
        string prefix, rest;
        ExtractPrefix(ref command, "ricochet [^\\:]*\\:", out has, out prefix, out rest);
        if (has)
        {
            string prefixValue = prefix.Substring(8, prefix.Length - 9).Trim();
         
            string[] tokens = prefixValue.Split(' ');
            bool hasfound;
            double ms;
            Debug.Log("prefix:" + prefix + " value:" + prefix.Substring(8, prefix.Length - 9));
            Debug.Log("rest:" + rest);
            for (int i = 0; i < tokens.Length; i++)
            {
                TimeStringParser.GetMillisecond(tokens[i],out hasfound, out ms);
                Debug.Log("token:" + tokens[i] + "  "+ms+"ms  ");
                if (hasfound)
                {
                    
                    m_timer.AddFromNow(ms, () => base.SendAction(rest), PingThreadType.InUnityThread);
                }
            }

        }
        succedToExecute.SetAsFinished(has);
    }

   


    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Not information";
    }
}
