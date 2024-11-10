using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StringSplitParser {
    public static int GetIndexOf(string text, char c, int iterationStartAtOne) {
        int foundCount = 0;
        char[] t = text.ToCharArray();
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] == c) { 
                foundCount++;
                if (foundCount == iterationStartAtOne)
                    return i;
            }
        }
        return -1;

    }

    public static string GetTextAfterChar(string text, char c, int interation) {
        int i = GetIndexOf(text, c, interation);
        if (i < 0)
            return "";
        if (i + 1 >= text.Length)
            return "";
        return text.Substring(i+1);
             
    }

}

public class Interpreter_Stacker : AbstractInterpreterMono
{
    public StackerRegisterMono m_stacker;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "stack:", true);
    }

    public override string GetName()
    {
        return "Stacker";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        //Debug.Log("Manage loop: " + command.GetLine());
        string[] token = cmd.Split(':');
        bool wasManaged = false;
        if (token.Length >= 3) { 
        string stackName = token[2].Trim();
        //stack:remove:name  
            if (cmdLow.IndexOf("stack:remove:") == 0 && token.Length >= 3)
            {
                m_stacker.Remove(stackName);
            }

            //stack:append:command 
            else if (cmdLow.IndexOf("stack:append:") == 0 && token.Length >= 3)
            {
                string action = StringSplitParser.GetTextAfterChar(cmd, ':', 3);
                m_stacker.Append(stackName, action.Trim());
            }

            //stack:next:name
            else if (cmdLow.IndexOf("stack:clear:") == 0 && token.Length >= 3)
            {

                m_stacker.Clear(stackName);
            }
            else if (cmdLow.IndexOf("stack:gonext:") == 0 && token.Length >= 3)
            {

                m_stacker.GoNext(stackName, 1);
            }
            else if (cmdLow.IndexOf("stack:goprevious:") == 0 && token.Length >= 3)
            {

                m_stacker.GoNext(stackName,-1);
            }
            else if (cmdLow.IndexOf("stack:do:") == 0 && token.Length >= 3)
            {

                SendCommand(m_stacker.GetCurrent(stackName));
            }
            else if (cmdLow.IndexOf("stack:doandgonext:") == 0 && token.Length >= 3)
            {

                SendCommand(m_stacker.GetCurrent(stackName));
                m_stacker.GoNext(stackName, 1);
            }
            else if (cmdLow.IndexOf("stack:doandgoprevious:") == 0 && token.Length >= 3)
            {

                SendCommand(m_stacker.GetCurrent(stackName));
                m_stacker.GoNext(stackName, -1);
            }
            

            //stack:moveat:name:5
            else if ((cmdLow.IndexOf("stack:moveat:") == 0 
                || cmdLow.IndexOf("stack:index:") == 0)
                && token.Length >= 4)
            {
                int index = 0;
                if (int.TryParse(token[3].Trim(), out index))
                {
                    m_stacker.SetIndex(stackName, index);
                }
            }
            //stack:movetostart:name
            else if (cmdLow.IndexOf("stack:movetostart:") == 0 && token.Length >= 3)
            {

                m_stacker.MoveAtStart(stackName);
            }
            //stack:movetoend:name
            else if (cmdLow.IndexOf("stack:movetoend:") == 0 && token.Length >= 3)
            {

                m_stacker.MoveAtEnd(stackName);
            }
            //stack:docurrent:name
            else if (cmdLow.IndexOf("stack:docurrent:") == 0 && token.Length >= 3)
            {

                SendCommand( m_stacker.GetCurrent(stackName));
            }



        }

        succedToExecute.SetAsFinished(wasManaged);
    }

    private void SendCommand(List<ICommandLine> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            SendCommand(list[i]);
        }
    }

    public CommandLineEvent m_commandFound;
    private void SendCommand(ICommandLine cmd)
    {
        m_commandFound.Invoke(cmd);
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
