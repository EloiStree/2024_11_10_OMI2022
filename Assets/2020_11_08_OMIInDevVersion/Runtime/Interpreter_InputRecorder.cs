using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class Interpreter_InputRecorder : AbstractInterpreterMono
{
    public KeyboardRecorderTool m_keyboardRecorder;
    public MouseDeltaRecorderTool m_mouseInformation;
    public string m_folderName="SaveMacro";
    public Dictionary<string, TimedCommandLines> m_macroJomi= new Dictionary<string, TimedCommandLines>();
    public UI_ServerDropdownJavaOMI m_target;//= new UI_ServerDropdownJavaOMI();
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "keyrecord:", true);
    }

    public override string GetName()
    {
        return "Record and replay";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');
        bool wasManaged = false;

        Debug.Log("Hello World!");

        //keyrecord:start
        if (cmdLow.IndexOf("keyrecord:start") == 0 )
        {
            m_keyboardRecorder.StartRecording();
            wasManaged = true;
        }
        //keyrecord:pause
        else if (cmdLow.IndexOf("keyrecord:pause") == 0)
        {
            m_keyboardRecorder.PauseRecording();
            wasManaged = true;
        }//keyrecord:resume
        else if (cmdLow.IndexOf("keyrecord:resume") == 0)
        {
            m_keyboardRecorder.ResumeRecording();
            wasManaged = true;
        }//keyrecord:stop
        else if (cmdLow.IndexOf("keyrecord:stop") == 0)
        {
            m_keyboardRecorder.StopRecording();
            wasManaged = true;
        }//keyrecord:save:name
        else if (cmdLow.IndexOf("keyrecord:save:") == 0 && token.Length >= 3)
        {
            AddCurrentKeyboardRecordAs(token[2].Trim());
            wasManaged = true;
        }
        //keyrecord:playback
        else if (cmdLow.IndexOf("keyrecord:playback") == 0 )
        {
            AddCurrentKeyboardRecordAs("CurrentKeyRecord");
            PlayJomiMacro("CurrentKeyRecord"); 
            wasManaged = true;
        }
        //keyrecord:play:name  
        else if (cmdLow.IndexOf("keyrecord:play:") == 0 && token.Length >= 3)
        { 
            PlayJomiMacro("CurrentKeyRecord");
            wasManaged = true;
        }
        //keyrecord:saveasfile:filename
        else if (cmdLow.IndexOf("keyrecord:saveasfile:") == 0 && token.Length >= 3)
        {
            AddCurrentKeyboardRecordAs("CurrentKeyRecord");
            SaveAsFile(token[2].Trim());
            wasManaged = true;
        }

        //mouserecord:start
        //mouserecord:pause
        //mouserecord:resume
        //mouserecord:stop
        //mouserecord:save:name
        //mouserecord:playback
        //mouserecord:play:name
        //mouserecord:saveasfile:filename


        succedToExecute.SetAsFinished(wasManaged);
    }

    private void SaveAsFile(string fileName)
    {
        TimedCommandLines current =  m_keyboardRecorder.GetCurrent();
        current.m_name = fileName;
        current.m_callId = fileName;
        string txt = TimedCommandLines.ConvertToFile(current);
        UnityDirectoryStorage.SaveFile(m_folderName, fileName + ".jomimacro", txt, true);
    }

    private void PlayJomiMacro(string name)
    {
        if (!m_macroJomi.ContainsKey(name))
            return;
        TimedCommandLines t = m_macroJomi[name];
       List<string> cmds= JomiTimedMacro. GetCommandsFor(t, DateTime.Now, 100);
        foreach (var item in cmds)
        {
            foreach (var target in m_target.GetJavaOMISelected())
            {
                target.SendRawCommand(item);
            }
        }
    }



    private void AddCurrentKeyboardRecordAs(string name)
    {
        if (!m_macroJomi.ContainsKey(name))
            m_macroJomi.Add(name, m_keyboardRecorder.GetCurrent());
        else    m_macroJomi[name]= m_keyboardRecorder.GetCurrent();
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, true, true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Record and playback key and mouse actions";
    }
}

