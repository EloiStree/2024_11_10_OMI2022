using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_WindowUtility :  AbstractInterpreterMono
{

    public WindowUtilityMono m_windowUtility;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {

        return StartWith(ref command, "winutility:", true);
    }

    public override string GetName()
    {
        return "Windows Manager Utility";
    }



    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim().ToLower();
        string[] tokens = command.GetLine().Trim().Split(':');

        //winutility:savewinpositionas:positionname
        if (cmd.IndexOf("winutility:savewinpositionas:") == 0 && tokens.Length == 3)
        {
            string name = tokens[2];
            m_windowUtility.SaveCurrentWindowPositionAs(name);
        }

        //winutility:savefocusas:windowname
        if (cmd.IndexOf("winutility:savefocusas:") == 0 && tokens.Length==3)
        {
            string name = tokens[2];
            m_windowUtility.SaveCurrentWindowAsFocusWith(name,false);
        }
        //winutility:permasavefocusas:windowname
        if (cmd.IndexOf("winutility:permasavefocusas:") == 0 && tokens.Length == 3)
        {
            string name = tokens[2];
            m_windowUtility.SaveCurrentWindowAsFocusWith(name, true);

        }
        //winutility:setfocuson:windowname
        if (cmd.IndexOf("winutility:setfocuson:") == 0 && tokens.Length == 3)
        {
            string name = tokens[2];
            m_windowUtility.SetFocusOn(name);

        }
        //winutility:movetoscreenlocation:windowname:screenlocation
        if (cmd.IndexOf("winutility:movetoscreenlocation:") == 0 && tokens.Length == 4)
        {
            string name = tokens[2];
            string screenlocation = tokens[3];
            m_windowUtility.MoveWindowAt(name, screenlocation);

        }//winutility:changetitle:windowname:title as text
        if (cmd.IndexOf("winutility:changetitle:") == 0 && tokens.Length == 4)
        {
            string name = tokens[2].ToLower();
            string title = tokens[3];
            m_windowUtility.ChangeTitle(name, title);

        }//winutility:changetitle:title as text
        if (cmd.IndexOf("winutility:changetitle:") == 0 && tokens.Length == 3)
        {
            string title = tokens[2];
            m_windowUtility.ChangeTitle( title);

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
