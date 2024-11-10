using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_Looper : AbstractInterpreterMono
{
    public LooperRegisterAndHandlerMono m_loopRegister;
    public LooperRegisterAndHandlerMono m_loopFromFile;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "loop:", true) || StartWith(ref command, "tloop:", true);
    }

    public override string GetName()
    {
        return "Looper";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');
        bool wasManaged = false;


       
        //loop:name:bool
         if (cmdLow.IndexOf("loop:set:") == 0 && token.Length >= 4)
        {
            string value = token[3].Trim().ToLower();
            SetOnOff(m_loopFromFile, token[2].Trim(), value == "true" || value == "1");
        }
        //loop:on:name
        else if (cmdLow.IndexOf("loop:on:") == 0 && token.Length >= 3)
        {
            SetOnOff(m_loopFromFile, token[2].Trim(), true);
        }
        //loop:off:name
        else if (cmdLow.IndexOf("loop:off:") == 0 && token.Length >= 3)
        {
            SetOnOff(m_loopFromFile, token[2].Trim(), false);
        }
        //loop:restart:name
        else if (cmdLow.IndexOf("loop:restart:") == 0 && token.Length >= 3)
        {
            m_loopFromFile.ResetTime(token[2].Trim());
        }
        //loop:time:name:timevalue
        else if (cmdLow.IndexOf("loop:time:") == 0 && token.Length >= 4)
        {
            bool hasTime;
            double millisecond;
            TimeStringParser.GetMillisecond(token[3], out hasTime, out millisecond);
            // Debug.Log("?T??" + millisecond);
            if (hasTime)
            {
                m_loopFromFile.SetTime(token[2].Trim(), (uint)millisecond);

            }
        }
       



        //loop:remove:name
        if (cmdLow.IndexOf("tloop:remove") == 0 && token.Length >= 3)
        {
            m_loopRegister.Remove(token[2].Trim());
        }
        //loop:name:bool
        else if (cmdLow.IndexOf("tloop:set:") == 0 && token.Length >= 4)
        {
            string value = token[3].Trim().ToLower();
            SetOnOff(m_loopRegister, token[2].Trim(), value == "true" || value == "1");
        }
        //loop:on:name
        else if (cmdLow.IndexOf("tloop:on:") == 0 && token.Length >= 3)
        {
            SetOnOff(m_loopRegister, token[2].Trim(), true);
        }
        //loop:off:name
        else if (cmdLow.IndexOf("tloop:off:") == 0 && token.Length >= 3)
        {
            SetOnOff(m_loopRegister, token[2].Trim(), false);
        }
        //loop:restart:name
        else if (cmdLow.IndexOf("tloop:restart:") == 0 && token.Length >= 3)
        {
            m_loopRegister.ResetTime(token[2].Trim());
        }
        //loop:time:name:timevalue
        else if (cmdLow.IndexOf("tloop:time:") == 0 && token.Length >= 4)
        {
            bool hasTime;
            double millisecond;
            TimeStringParser.GetMillisecond(token[3], out hasTime, out millisecond);
            // Debug.Log("?T??" + millisecond);
            if (hasTime)
            {
                m_loopRegister.SetTime(token[2].Trim(), (uint)millisecond);

            }
        }
        //loop:create:name:time:commandline
        else if (cmdLow.IndexOf("tloop:create:") == 0 && token.Length >= 5)
        {
            CreateLoop(cmd, token);
        }
        //loop:createandstart:name:time:commandline
        else if (cmdLow.IndexOf("tloop:createandstart:") == 0 && token.Length >= 5)
        {

            CreateLoop(cmd, token);
            SetOnOff(m_loopRegister ,token[2].Trim(), true);
        }

        succedToExecute.SetAsFinished(wasManaged);
    }

    private void SetOnOff(LooperRegisterAndHandlerMono loopRegister, string name, bool value)
    {
        loopRegister.SetOnOff(name, value);
    }

    private void CreateLoop(string cmd, string[] token)
    {
        bool hasTime;
        double millisecond;
        // Debug.Log("?C??" + token[2].Trim());
        TimeStringParser.GetMillisecond(token[3], out hasTime, out millisecond);
        if (hasTime)
        {

            string action = StringSplitParser.GetTextAfterChar(cmd, ':', 4);
            m_loopRegister.Create(token[2].Trim(), (uint)millisecond, () => SendAction(action.Trim()));
        }
    }

    public CommandLineEvent m_commandToExecute;

    public void SendAction(string rest)
    {
        m_commandToExecute.Invoke(new CommandLine(rest));
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, true,true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

   
}
