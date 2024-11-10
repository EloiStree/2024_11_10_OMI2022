using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_WindowProcessBoardcast : AbstractInterpreterMono
{

    public object m_broadCastPusher;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {

        return StartWith(ref command, "castkey:", true);
    }

    public override string GetName()
    {
        return "Broad cast key to process";
    }



    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim().ToLower();
        string[] tokens = command.GetLine().Trim().Split(':');

        ////winutility:savewinpositionas:positionname
        //if (cmd.IndexOf("castkey:") == 0 && tokens.Length == 2)
        //{
        //    m_broadCastPusher.SendEventAsWebRequest(tokens[1].Trim());
        //}
        //if (cmd.IndexOf("castkey:") == 0 && tokens.Length > 2)
        //{
        //    string eventName = tokens[1].Trim();
        //    string v0 = "";
        //    string v1 = "";
        //    string v2 = "";
        //    if (tokens.Length > 2)
        //        v0 = tokens[2].Trim();
        //    if (tokens.Length > 3)
        //        v1 = tokens[3].Trim();
        //    if (tokens.Length > 4)
        //        v2 = tokens[4].Trim();

        //    m_iftttPusher.SendEventAsWebRequestJson(eventName, v0, v1, v2);
        //}

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

