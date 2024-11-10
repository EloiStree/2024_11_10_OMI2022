using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_X360Simulator : AbstractInterpreterMono
{
    public Send2XOMI m_xomi;
    public UDPThreadSender m_udpSender;



    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "x360:", true) || StartWith(ref command, "xbox:", true); ;
    }

    public override string GetName()
    {
        return "Xbox Simulator";
    }

    private void Awake()
    {
        m_udpSender.ResetTheTargetFromAlias();
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {

        string cmd = command.GetLine().Trim();
        int index = cmd.IndexOf(":");
        if (index > -1  && index+1 < cmd.Length) {
            string action = cmd.Substring(index+1).Trim().ToLower();
            m_udpSender.AddMessageToSendToAll(action);
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