using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_User32Windows : AbstractInterpreterMono
{
    public Eloi.PrimitiveUnityEvent_String m_womiShortCutToParse;
    // 🗔🪟
    public override bool CanInterpreterUnderstand(ref ICommandLine icommand)
    {

        string command = icommand.GetLine().Trim();
        return command.ToLower().Trim().StartsWith("user32:") ||
        command.ToLower().Trim().StartsWith("w32:") ||
        command.ToLower().Trim().StartsWith("womi:");
    }


    public override string GetName()
    {
        return "Window DLL commands for win form and keystroke";
    }



    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        //In 1000|w32:p:key:3868908:VK_A↕
        //In 1000|w32:r:key:KEY_A↕
        //     User32ActionAbstractCatchToExecuteMono
        string cmd = command.GetLine().Trim();
        string[] tokens = cmd.Split(':');

        if (Eloi.E_StringUtility.AreEquals(tokens[0], "womi")) {

            PushToParseOMISentence(tokens[1]);

            succedToExecute.SetAsFinished(true);
            return;
        }

        if ( tokens.Length == 2) {

            string value = tokens[1];
            if (Eloi.E_StringUtility.AreEquals(value, "Hide"))
                User32ActionAbstractCatchToExecute.PushAction(
                new Action_HideProcessId(WindowIntPtrUtility.GetCurrentProcessId()));
            if (Eloi.E_StringUtility.AreEquals(value, "Show"))
            User32ActionAbstractCatchToExecute.PushAction(
            new Action_ShowProcessId(WindowIntPtrUtility.GetCurrentProcessId()));
        }

        if (tokens.Length >3 ) {

            string isPost = tokens[1];
            string label = tokens[2];
            if (tokens.Length == 4 && (Eloi.E_StringUtility.AreEquals(isPost, "real")||
                Eloi.E_StringUtility.AreEquals(isPost, "r")))
            {
                string value = tokens[3];
                if (Eloi.E_StringUtility.AreEquals(label, "key"))
                {
                    TryToParseAsRealKey(value);
                }
                else if (Eloi.E_StringUtility.AreEquals(label, "mouse"))
                {
                    TryToParseAsRealMouse(value);
                }
            }
            else if (tokens.Length == 5 &&
                (Eloi.E_StringUtility.AreEquals(isPost, "post") ||
                Eloi.E_StringUtility.AreEquals(isPost, "p")) )
            {

                string target = tokens[3];
                string value = tokens[4];
                if (Eloi.E_StringUtility.AreEquals(label, "key"))
                {

                    TryToParseAsPostKey(target,value);
                    Debug.Log(">>test:" + value);
                }
                else if (Eloi.E_StringUtility.AreEquals(label, "mouse"))
                {
                    TryToParseAsPostMouse(target, value);
                }
            }

        }


        succedToExecute.SetAsFinished(true);
    }

    public bool ContainPress(in string text) { return text.IndexOf("↓") > -1 || ContainStroke(in text); }
    public bool ContainRelease(in string text) { return text.IndexOf("↑") > -1 || ContainStroke(in text); }
    public bool ContainStroke(in string text) { return text.IndexOf("↕") > -1; }


    private void TryToParseAsPostMouse(string target, string value)
    {
        throw new NotImplementedException();
    }

    private void TryToParseAsPostKey(string target, string value)
    {
        if (int.TryParse(target, out int processId)
            && TryToParseAsPostKey(value, out User32PostMessageKeyEnum key))
        {
            bool press = ContainPress(in value);
            bool release = ContainRelease(in value);
            if (press)
                User32ActionAbstractCatchToExecute.PushAction(
                new Action_Post_KeyInteraction(processId, key, User32PressionType.Press));

            if (release)
                User32ActionAbstractCatchToExecute.PushActionIn(5,
                new Action_Post_KeyInteraction(processId, key, User32PressionType.Release));
        }
    }

    private void TryToParseAsRealKey( string value)
    {
        if (TryToParseAsKeyStroke(value, out User32KeyboardStrokeCodeEnum key))
        {
            bool press = ContainPress(in value);
            bool release = ContainRelease(in value);
            if (press)
                User32ActionAbstractCatchToExecute.PushAction(
                new Action_Real_KeyInteraction( key, User32PressionType.Press));

            if (release)
                User32ActionAbstractCatchToExecute.PushAction(
                new Action_Real_KeyInteraction( key, User32PressionType.Release));
        }
    }
    private bool TryToParseAsPostKey(string value, out User32PostMessageKeyEnum key)
    {
        value = RemoveArrows(value);
        return Enum.TryParse(value, out key);
    }
    private bool TryToParseAsKeyStroke(string value, out User32KeyboardStrokeCodeEnum key)
    {
        value = RemoveArrows(value);
        return Enum.TryParse(value, out key);
    }

    private static string RemoveArrows(string value)
    {
        value = value.Replace("↓", "").Replace("↑", "").Replace("↕", "").Trim();
        return value;
    }

    private void TryToParseAsRealMouse(string value)
    {
        throw new NotImplementedException();
    }

  
    private void PushToParseOMISentence(string sentenceShortToParse)
    {
        m_womiShortCutToParse.Invoke(sentenceShortToParse);
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        //string cmd = command.GetLine().Trim();

        //    if (IsShortcutCommand(cmd))
        //       return "Send Shortcut to JOMI runner: "+ cmd.Substring(8);
        //    if (IsRawCommand(cmd))
        //        return "Send Raw command to JOMI runner: " + cmd.Substring(5);
        //return "Nothing because it don't seem to be command >> " + cmd;

        return "";
    }

}
