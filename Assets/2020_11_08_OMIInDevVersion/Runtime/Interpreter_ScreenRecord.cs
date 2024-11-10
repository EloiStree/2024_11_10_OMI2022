using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_ScreenRecord : AbstractInterpreterMono
{
    public ScreenSaveAndRecorder m_screenRecord;
        public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return IsValide(command);
    }

    private static bool IsValide(ICommandLine command)
    {
        return command.GetLine().Trim().ToLower().IndexOf("screenrec:") == 0;
    }

    public override string GetName()
    {
        return "Screen Recorder Interpret";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim().ToLower();
        string[] tokens = cmd.Split(':');
        if (tokens.Length < 2) {

            succedToExecute.SetAsFinished(false);
            return;
        }
        //Debug.Log("TEST:" + cmd);
        string instruction = tokens.Length >1 ? tokens[1].Trim() :"";
        string name =tokens.Length>2? tokens[2].Trim():"Default";
        bool save = instruction == "save" || instruction == "s";
        bool recover = instruction == "recover" || instruction == "r";

        //Debug.Log("TESTD:" + instruction+" - "+name);

        if (save) {
            m_screenRecord.Save(name);
        }
        else if (recover) {
            m_screenRecord.Recover(name);
        }

        succedToExecute.SetAsFinished(true);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false); 
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Not defined";
    }

  
}
