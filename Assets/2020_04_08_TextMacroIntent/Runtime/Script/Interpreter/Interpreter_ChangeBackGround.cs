using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_ChangeBackGround : AbstractInterpreterMono
{
    public Camera m_linkedCamera;
    public CommandExecutionInformation m_executionQuote = new CommandExecutionInformation(true,false, false, true);
    public Color m_wantedColorForCamera;


    public override string GetName()
    {
        return "Change Backround Color";
    }

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return command.GetLine().ToLower().IndexOf("change camera background ") == 0;
    }


    private void ChangeCameraColorFromCommandLine(string command, out bool succedToExecute)
    {
        succedToExecute = false;
        string cmdvalue = GetValueOfCommand(command);

        Color color; 
        bool foundColor = GetColorFrom(cmdvalue, out color);

        if (foundColor) {

            succedToExecute = true;
            m_wantedColorForCamera = color;
        }


    }

    private string GetValueOfCommand(string command)
    {
        return command.ToLower().Replace("change camera background ", "").Trim(); 
    }

    private bool GetColorFrom(string cmdvalue, out Color color)
    {
        bool converted=false;
        color = new Color(1f, 1f, 1f, 1f);
        string[] tokens = cmdvalue.Split(' ');
        if (tokens.Length == 3)
        {
            float r, g, b;
            try
            {
           

                r = float.Parse(tokens[0]);
                g = float.Parse(tokens[1]);
                b = float.Parse(tokens[2]);

                if (r > 1f || g > 1f || b > 1f)
                { r /= 255f; g /= 255f; b /= 255f; }
                color.r = r; color.g = g; color.b = b;
                converted = true;


            }
            catch (Exception)
            {
                converted = false;
            }
        }
        else if (tokens.Length == 1)
        {
            throw new System.NotImplementedException("#FFFFFF should be implementd when I have time.");
        }
        return converted;
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        bool succed;
        ChangeCameraColorFromCommandLine(command.GetLine(), out succed);
        succedToExecute.SetAsFinished(succed);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = m_executionQuote;
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        Color c;
        if (GetColorFrom(GetValueOfCommand(command.GetLine()), out c))
            return string.Format("Change the color of the background camera to {0}:{1}:{2}", c.r, c.g, c.b);
        return "Nothing the color format is not recognize";
    }


    private void Awake()
    {
        m_wantedColorForCamera = m_linkedCamera.backgroundColor;
    }
    void Update()
    {
        m_linkedCamera.backgroundColor = Color.Lerp(m_linkedCamera.backgroundColor, m_wantedColorForCamera, Time.deltaTime);
    }
}
