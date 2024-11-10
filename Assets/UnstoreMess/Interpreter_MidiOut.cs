
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_MidiOut : AbstractInterpreterMono
{

    public WindowMidiOutWrapperMono m_midiWindowOut;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {

        return StartWith(ref command, "midi:", true) || StartWith(ref command, "midiraw:", true);
    }

    public override string GetName()
    {
        return "Midi Interpretor";
    }





    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim().ToLower();

        long abosluteTime = 0;
        long channel = 1;
        long noteNumber = 1;
        long duration = 500;
        long velocity = 100;

        if (cmd.IndexOf("midi:") == 0)
        {
            string toFilter = cmd.Substring("midi:".Length);
            string[] p = toFilter.Split(' ');
            long value = 0;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].Trim().Length > 0)
                {
                    if (CharIntegerToValue(p[i], 'c', out value))
                        channel = value;
                    if (CharIntegerToValue(p[i], 'n', out value))
                        noteNumber = value;
                    if (CharIntegerToValue(p[i], 'd', out value))
                        duration = value;
                    if (CharIntegerToValue(p[i], 'v', out value))
                        velocity = value;
                    if (CharIntegerToValue(p[i], 't', out value))
                        abosluteTime = value;

                    try
                    {
                        if (m_midiWindowOut != null)
                            m_midiWindowOut.SendNoteRaw((int)channel, (int)noteNumber, (int)velocity, (int)duration, abosluteTime);
                    }
                    catch (Exception)
                    {

                    }
                }

            }

        }
        if (cmd.IndexOf("midiraw:") == 0)
        {

            //  m_midiWindowOut.se
        }


        succedToExecute.SetAsFinished(true);
    }

    private static bool CharIntegerToValue(string text, in char c, out long value)
    {
        value = 0;
        if (text.IndexOf("" + c) > -1)
        {
            text = text.Replace("" + c, "");
            if (long.TryParse(text, out value))
            {
                return true;
            }
        }
        return false;
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

