using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CmdConvert_MockXbox : CmdConvertAbstract
{
    public MockXboxUI m_mockXbox;
    public override bool CanTakeResponsability(string command)
    {
        return Regex.IsMatch(command.ToLower(), "xbox\\s.*");
    }

    public override void DoTheThing(string command, SuccessChecker hasBeenConverted, FinishChecker hasFinish)
    {
        if(!CanTakeResponsability(command))
        {
            hasBeenConverted.SetAsFail("Do not respect the format accepted");
            hasFinish.SetAsFinished();
            return;
        }
        command = command.ToLower().Replace("xbox ", "").Trim();
        bool isOn = true;
        if (command.IndexOf("release") > -1 || command.IndexOf("0")>-1 || command.IndexOf("off") > -1)
            isOn = false;
        bool foundOne=false;
        foundOne |= SetOnIfFound(ref command, "jl"   , MockXboxUI.XboxButton.JL,    isOn);
        foundOne |= SetOnIfFound(ref command, "jr"   , MockXboxUI.XboxButton.JR,    isOn);
        foundOne |= SetOnIfFound(ref command, "rt"   , MockXboxUI.XboxButton.RT,    isOn);
        foundOne |= SetOnIfFound(ref command, "rb"   , MockXboxUI.XboxButton.RB,    isOn);
        foundOne |= SetOnIfFound(ref command, "lt"   , MockXboxUI.XboxButton.LT,    isOn);
        foundOne |= SetOnIfFound(ref command, "lb"   , MockXboxUI.XboxButton.LB,    isOn);
        foundOne |= SetOnIfFound(ref command, "x"    , MockXboxUI.XboxButton.X,     isOn);
        foundOne |= SetOnIfFound(ref command, "y"    , MockXboxUI.XboxButton.Y,     isOn);
        foundOne |= SetOnIfFound(ref command, "b"    , MockXboxUI.XboxButton.B,     isOn);
        foundOne |= SetOnIfFound(ref command, "a"    , MockXboxUI.XboxButton.A,     isOn);
        foundOne |= SetOnIfFound(ref command, "left" , MockXboxUI.XboxButton.Left,  isOn);
        foundOne |= SetOnIfFound(ref command, "right", MockXboxUI.XboxButton.Right, isOn);
        foundOne |= SetOnIfFound(ref command, "up"   , MockXboxUI.XboxButton.Up,    isOn);
        foundOne |= SetOnIfFound(ref command, "down" , MockXboxUI.XboxButton.Down,  isOn);
        if (foundOne)
            hasBeenConverted.SetAsDone();
        else hasBeenConverted.SetAsFail("Did not find what button has been ask to be press");
        hasFinish.SetAsFinished();


    }

    private bool SetOnIfFound(ref string command,string value, MockXboxUI.XboxButton button, bool isOn)
    {
        if (command.IndexOf(value) > -1) { 
            m_mockXbox.SetOn(button, isOn);
        }
        return false;
    }
}
