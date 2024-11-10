using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_TextToActions : AbstractInterpreterMono
{
    public TextListenerMono m_keyboardTextListener;
    public TextListenerMono m_speechRecognitionTextListener;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "textlistener:", true) || StartWith(ref command, "speechlistener:", true);
    }

    public override string GetName()
    {
        return "Text(s) Listener";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine();
        string cmdLow = cmd.ToLower();
        string[] tokens = cmd.Split(':');
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i] = tokens[i].Trim();
        }

        if (tokens.Length >= 2) { 
            string action = tokens[1];
            TextListenerMono textListener = null;
            if (tokens[0].ToLower() == "textlistener")
            {
                textListener = m_keyboardTextListener;
            }
            else if (tokens[0].ToLower() == "speechlistener")
            {

                textListener = m_speechRecognitionTextListener;
            }
            if (textListener != null) { 
                if (tokens[1].ToLower() == "start")
                {
                    textListener.StartListening();
                }
                if (tokens[1].ToLower() == "stop")
                {
                    textListener.StopListening();
                }
                if (tokens[1].ToLower() == "push")
                {
                    textListener.Push();
                }
                if (tokens[1].ToLower() == "clear")
                {
                    textListener.Clear();
                }
                if (tokens[1].ToLower() == "pushandclear")
                {
                    textListener.Push();
                    textListener.Clear();
                }
                if (tokens[1].ToLower() == "fullstop")
                {
                    textListener.StopListening();
                    textListener.PushAndClear();
                }

                if (tokens[1].ToLower() == "propose" 
                    && tokens.Length==3 )
                {
                    textListener.Set(tokens[2].Trim());
                    textListener.PushAndClear();
                }
            }
        }
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
