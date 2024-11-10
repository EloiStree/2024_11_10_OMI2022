using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Interpreter_Time : AbstractInterpreterMono
{

    [Header("Time Manager")]
    public CommandAuctionExecuter m_executer;
    public TimeThreadMono m_timeThread;

    public override string GetName()
    {
        return "Time relay";
    }


    public CommandExecutionInformation m_quoteRequirement = new CommandExecutionInformation(false, false, true,true);
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return ( command.GetLine().ToLower().Trim().IndexOf("in ") == 0 && command.GetLine().IndexOf("|") > 0 )
        || ( command.GetLine().ToLower().Trim().IndexOf("at ") == 0 && command.GetLine().IndexOf("|") > 0 );


    }

    public bool TryToConvertCommandToTime(string command, out CommandLine toCallOnTime, out double valueInMilliseconds) {
        toCallOnTime = null;
        valueInMilliseconds = 0;
        //if (command.ToLower().Trim() == "stop in waiting commands")
        //    m_macroTimeManager.StopInWaitingCommand();
        //EXIT CONDITION
        //Debug.Log("TTT: " );
        if ( command.ToLower().Trim().IndexOf("in") != 0) return false;
        int timeCommandSpliterIndex = command.IndexOf("|");
        if (timeCommandSpliterIndex<=0) { return false; }
        if (timeCommandSpliterIndex >= command.Length-1) { return false; }

        //Start analysing
        string cmdToCall = "";
        string timeAsString = "";
        timeAsString = GetNumbers(command.Substring(0, timeCommandSpliterIndex).ToLower().Substring(2).Replace(",",".").Trim());
        cmdToCall = command.Substring(timeCommandSpliterIndex+1).Trim();
        //Debug.Log("Time: " + timeAsString + "  Cmd: " + cmdToCall);
        toCallOnTime = new CommandLine(cmdToCall.Trim());
        bool inSecond = timeAsString.IndexOf(".")>=0 || timeAsString.IndexOf("s") > 0 || timeAsString.IndexOf("sec") > 0 || timeAsString.IndexOf("second") > 0;
        double value=0;

        if (double.TryParse(timeAsString, out value))
        {
            if (inSecond)
                valueInMilliseconds = value * 1000f;
            else valueInMilliseconds= value;

            //Debug.Log("Value: " + value + "  Second: " + inSecond);
            return true;
        }
        //Debug.Log("Fail: " + value + "  Second: " + inSecond);
        return false;
    }
    private static string GetNumbers(string input)
    {
       return Regex.Replace(input, "[^0-9\\.]", "");
    }

    private static char[] DECIMALSPLITER = new char[] { '.', ',' };
    private void DecimalStringToDoubleInterger(string decimalString, out int left, out int right)
    {
        int tmp=0;
        left = 0; right = 0;
        string[] tokens = decimalString.Split(DECIMALSPLITER);
        if (tokens.Length == 1)
        {
            if (int.TryParse(tokens[0], out tmp))
                left = tmp;
        }
        if (tokens.Length == 2)
        {
            if (int.TryParse(tokens[0], out tmp))
                left = tmp;
            if (int.TryParse(tokens[1], out tmp))
                right = tmp;
        }
    }

   

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        double millisecond =0;
        CommandLine cl;
        bool converted = TryToConvertCommandToTime(command.GetLine(), out cl, out millisecond);
        if (converted)
        {
            //Debug.Log("MS:" + millisecond);
            m_timeThread.AddFromNow(millisecond, () =>SendCmd(cl), PingThreadType.InUnityThread);
        }
        else succedToExecute.StopWithError("Did not succed to convert the command as delayed command.");
        succedToExecute.SetAsFinished(converted);
    }

    private void SendCmd(CommandLine cl)
    {
        m_executer.Execute(cl);
        //Debug.Log("WTF:" + cl.GetLine());
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo= m_quoteRequirement;
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "The command will be executed with delay depending of the given time.";
    }
}
