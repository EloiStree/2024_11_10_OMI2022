using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_CopyPast : AbstractInterpreterMono
{

    public CopyPastFileRegisterMono m_copypasterRegister;
    public CommandLineEvent m_actionEmitted;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "copypast:", true);
    }

    public override string GetName()
    {
        return "Copy paster";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        if (CanInterpreterUnderstand(ref command))
        {
            string cmd = command.GetLine().Trim();
            string cmdLow = command.GetLine().ToLower();
            string[] token = cmd.Split(':');

            if (token.Length >= 3)
            {
                bool targetCurrentDevice = token[1].Trim().ToLower() == "device";
                string action = token[2].Trim().ToLower();


                if (token.Length == 3)
                {

                    if (action == "past" )
                    {
                        SendCommandline("sc:CTRL+V");
                    }
                    if (action == "copy" )
                    {
                        SendCommandline("sc:CTRL+C");
                    }
                    if (action == "cut")
                    {
                        SendCommandline("sc:CTRL+X");
                    }
                }
                else if (token.Length > 3)
                {
                    string info = token[3].Trim();
                    if (action == "file" && token.Length >= 4)
                    {
                        CopyPastFile file;
                        if (m_copypasterRegister.GetFile(info, out file))
                        {
                            string text = file.GetText();
                            if (targetCurrentDevice)
                            {
                                ClipboardUtility.Set(text);
                                SendCommandline("In 50|jomi:CTRL+V");
                            }
                            else { 
                                SendCommandline("jomi:[[" + text + "]]");
                            }
                        }
                    }
                    if (action == "url" && token.Length >= 4)
                    {
                          // Do later
                    }

                }
            }
        }
            succedToExecute.SetAsFinished(true);
    }

    private void SendCommandline(string cmd)
    {
        m_actionEmitted.Invoke(new CommandLine(cmd));
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
