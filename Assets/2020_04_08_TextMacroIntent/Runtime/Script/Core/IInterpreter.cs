using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public interface IInterpreter
{
    bool CanInterpreterUnderstand(ref ICommandLine command);
    string WhatWillYouDoWith(ref ICommandLine command);
    string ExpectedCommandFormat();
    void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo);
    void TranslateToActions(ref ICommandLine command);
    void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute);
    string GetName();
    string[] GetExamplesOfGoodCommand();
    string[] GetExamplesOfBadCommand();
    string GetUrlOfDocumetationStartPoint();
    string GetCreatorOfTheInterpreterContactInfo();
}


public abstract class AbstractInterpreterMono : MonoBehaviour, IInterpreter
{
    /// <summary>
    /// Help to know if the interpreter can deal with the given command line.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public abstract bool CanInterpreterUnderstand(ref ICommandLine command);
    /// <summary>
    /// Help to know in what condition the command will be executed.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="executionInfo"></param>
    public abstract void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo);
    /// <summary>
    /// Explain what action should be done when send to the executer.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="executionInfo"></param>
    public abstract string WhatWillYouDoWith(ref ICommandLine command);
    /// <summary>
    /// Must return a text with several example of what command line format the interpreter is expecting for the user to know how to deal with it.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="executionInfo"></param>
    public string ExpectedCommandFormat() { return m_expectedFormat; }

    private ExecutionStatus dontCareOfResult = new ExecutionStatus();

    /// <summary>
    /// It will parse the commmand line to actions but won't return how it was handled.
    /// </summary>
    /// <param name="command"></param>
    public void TranslateToActions(ref ICommandLine command) {
        TranslateToActionsWithStatus(ref command, ref dontCareOfResult); 
    }
    /// <summary>
    /// It will parase the command line to actions and will give  you a tracker to know it execution state in case you want to wait the actions to be finished.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="succedToExecute"></param>
  public abstract void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute);

    public static bool StartWith(ref ICommandLine command, string text, bool ignoreCase) {
        string cmd = command.GetLine().Trim();
        if (ignoreCase)
           cmd = cmd.ToLower();
        if (ignoreCase)
            text = text.ToLower();
        return cmd.IndexOf(text) == 0;
    }

    public static void ExtractPrefix(ref ICommandLine command, string regex, out bool hasBeenFound, out string prefix, out string rest) {
        string cmd = command.GetLine();
        Match m = Regex.Match(cmd, regex);
        hasBeenFound = m.Success;
        if (!hasBeenFound)
        {
            prefix = "";
            rest = cmd;
        }
        else {
            prefix = m.Value;
            rest = SubstringReverse(cmd,m.Index, m.Length);
        }

    }
    public static string SubstringReverse(string str, int index, int length)
    {
        //KiloStrée - 5 - 4 
        return str.Substring(0, index)+ str.Substring(index+length);
    }


    [TextArea(0, 3)]
    [SerializeField] string m_expectedFormat;
    [Header("Documentation by examples")]
    [TextArea(6, 10)]
    [SerializeField] string m_goodCommands;
    [Header("Documentation by showing boundary")]
    [TextArea(6, 10)]
    [SerializeField] string m_badCommands;

    [SerializeField] string m_documentationUrl;

    [TextArea(6, 10)]
    [SerializeField] string m_creatorContactInfo;



    private void OnValidate()
    {
        m_expectedFormat = ExpectedCommandFormat();
    }

    public abstract string GetName();
    public string[] GetExamplesOfGoodCommand() {
        List<string> cmds = new List<string>();
        cmds.AddRange(m_goodCommands.Split('\n'));
        return cmds.ToArray();
    }
  
    public string[] GetExamplesOfBadCommand()
    {
        List<string> cmds = new List<string>();
        cmds.AddRange(m_badCommands.Split('\n'));
        return cmds.ToArray();
    }

    public string GetUrlOfDocumetationStartPoint()
    {
        return m_documentationUrl;
    }

    public string GetCreatorOfTheInterpreterContactInfo()
    {
        return m_creatorContactInfo;
    }
}


