using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CmdConvert_Music : CmdConvertAbstract
{
    public MockMusicUI m_mockMusic;
    public override bool CanTakeResponsability(string command)
    {
        return Regex.IsMatch(command.ToLower(), "music\\s.*");
    }

    public override void DoTheThing(string command, SuccessChecker hasBeenConverted, FinishChecker hasFinish)
    {
        if (!CanTakeResponsability(command))
        {
            hasBeenConverted.SetAsFail("Do not respect the format accepted");
            hasFinish.SetAsFinished();
            return;
        }
        command = command.ToLower().Replace("music ", "").Trim();
       
        bool foundOne = false;
        foundOne |= SetOnIfFound(ref command, "do", MockMusicUI.Note.Do);
        foundOne |= SetOnIfFound(ref command, "re", MockMusicUI.Note.Re);
        foundOne |= SetOnIfFound(ref command, "mi", MockMusicUI.Note.Mi);
        foundOne |= SetOnIfFound(ref command, "fa", MockMusicUI.Note.Fa);
        foundOne |= SetOnIfFound(ref command, "sol", MockMusicUI.Note.Sol);
        foundOne |= SetOnIfFound(ref command, "la", MockMusicUI.Note.La);
        foundOne |= SetOnIfFound(ref command, "si", MockMusicUI.Note.Si);
        if (foundOne)
            hasBeenConverted.SetAsDone();
        else hasBeenConverted.SetAsFail("Did not find a note to be play");
        hasFinish.SetAsFinished();


    }

    private bool SetOnIfFound(ref string command, string value, MockMusicUI.Note button)
    {
        if (command.IndexOf(value) > -1)
        {
            m_mockMusic.Tap(button);
            return true;
        }
        return false;
    }
}