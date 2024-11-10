using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class Interpreter_SoundPlay : AbstractInterpreterMono
{
    public SoundFileRegister m_soundFiles;
    public SoundFilePlayer m_player;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return IsValide(command);
    }

    private static bool IsValide(ICommandLine command)
    {
        return command.GetLine().ToLower().Trim().IndexOf("sound:") == 0;
    }

    public override string GetName()
    {
        return "Play Sound Interpreter";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        if (!IsValide(command))
        {
            succedToExecute.SetAsFinished(false);
            return;
        }

        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');
        if (token.Length >= 3)
        {
            string soundName = token[2].Trim();
            //stack:remove:name  
            if (cmdLow.IndexOf("sound:shotplay:") == 0
                || cmdLow.IndexOf("sound:playonce:") == 0
                || cmdLow.IndexOf("sound:create:") == 0)
            {
                PlayOnceSound(soundName);
            }
            if (cmdLow.IndexOf("sound:play:") == 0)
            {
                PlaySound(soundName);
            }
            if (cmdLow.IndexOf("sound:pause:") == 0)
            {
                PauseSound(soundName);
            }
            if (cmdLow.IndexOf("sound:stop:") == 0)
            {
                StopSound(soundName);
            }
        }
        if (token.Length ==2)
        {
            PlayOnceSound(token[1].Trim());
            if (cmdLow.IndexOf("sound:play") == 0)
            {
                m_player.Play();
            }
            if (cmdLow.IndexOf("sound:pause") == 0)
            {
                m_player.Pause();
            }
            if (cmdLow.IndexOf("sound:stop") == 0)
            {
                m_player.Stop();
            }
            if (cmdLow.IndexOf("sound:switchpause") == 0)
            {
                m_player.SwitchPause();
            }
        }

        succedToExecute.SetAsFinished(true);

    }

    private void StopSound(string name)
    {
        ExecutableFile sound;
        if (m_soundFiles.GetFile(name, out sound))
        {
            m_player.Stop(sound);

        }
    }

    private void PauseSound(string name)
    {
        ExecutableFile sound;
        if (m_soundFiles.GetFile(name, out sound))
        {
            m_player.Pause(sound);

        }
    }

    private void PlaySound(string name)
    {
        ExecutableFile sound;
        if (m_soundFiles.GetFile(name, out sound))
        {
            m_player.Play(sound);

        }
    }

    private void PlayOnceSound(string name)
    {
        ExecutableFile sound;
        if (m_soundFiles.GetFile(name, out sound))
        {
            m_player.PlayOneShot(sound);

        }
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo  = new CommandExecutionInformation(true, true, false, true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "To write later";
    }

   
}
