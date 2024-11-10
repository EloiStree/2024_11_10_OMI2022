using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;

public class CommandAuctionExecuter : MonoBehaviour
{
    public CommandAuctionDistributor m_auction;
    public TimeThreadMono m_timeThread;
    public CommandAuctionCoroutineExecuter m_coroutineCmdManager;

    
    public void ExecuteStringCommand(string text)
    {
        Execute(new CommandLine(text));
    }

    public void ExecuteAllDirectly(GroupOfCommandLines toExecute)
    {
        Execute(new ParallelsExecutionCommandLines(toExecute, ParallelsExecutionCommandLines.ThreadTriggerType.StartToEnd));
    }


    //Context
    public void Execute(ParallelsExecutionCommandLines toExecute) {

        foreach (ICommandLine item in toExecute.GetCommandLines())
        {
            Execute(item);
        }
    }
    public void Execute(RelativeToPreviousStepByStepCommandLines toExecute, PingThreadType threadType) {
        DateTime now = DateTime.Now;
        List<DateTimedCommandLine> commandToExecute = toExecute.GetCommandsToExecuteFrom(now, true);
        for (int i = 0; i < commandToExecute.Count; i++)
        {
            Execute(commandToExecute[i], threadType);
        }
    }
    public void Execute(RelativeToStartDateStepByStepCommandLines toExecute, PingThreadType threadType)
    {
        DateTime now = DateTime.Now;
        List<DateTimedCommandLine> commandToExecute = toExecute.GetCommandsToExecuteFrom(now, true);
        for (int i = 0; i < commandToExecute.Count; i++)
        {
            Execute(commandToExecute[i]); 
        }
    }
    public void ExecuteDirectly(DateTimedCommandLine toExecute) { Execute(toExecute, PingThreadType.InTimeThread); }
    public void ExecuteAsapForUnity(DateTimedCommandLine toExecute) { Execute(toExecute, PingThreadType.InUnityThread); }
    public void Execute( DateTimedCommandLine toExecute, PingThreadType threadType)
    {
        ICommandLine cl = toExecute;
        InWaitingToBeCall capsule = InWaitingToBeCall.ClaimWaitingContainer(cl, this);
        m_timeThread.Add(toExecute.GetWantedExecuteTime(), capsule.DoTheThing, threadType);
    }

    public void Execute(ICommandLine command)
    {
        if (command == null || command.GetLine()==null)
            return;
        IInterpreter interpret;
        if (m_auction.SeekForFirstTaker(command.GetLine(), out interpret))
            interpret.TranslateToActions(ref command);
    }
    public void Execute(char text)
    {
        Execute(new CommandLine("" + text));
    }
    public void Execute(string command)
    {

        Execute(new CommandLine(command));
    }
    public void Execute(params string[] commands)
    {
        for (int i = 0; i < commands.Length; i++)
        {
            Execute(new CommandLine(commands[i]));
        }
    }
    public void Execute(IEnumerable<string> commands)
    {
        foreach (string item in commands)
        {
            Execute(new CommandLine(item));
        }
    }


    //Simple
    public void Execute(CommandLine toExecute) {
        ICommandLine r = toExecute;
        Execute(r);
    }
    public void ExecuteFromNow(RelativeTimedCommandLine toExecute, PingThreadType threadType) { ExecuteFrom(toExecute, DateTime.Now, threadType); }
    public void ExecuteFrom(RelativeTimedCommandLine toExecute, DateTime date, PingThreadType threadType) {
       DateTime newTime = date.AddMilliseconds(toExecute.GetWantedExecuteTimeInMilliseconds());
        DateTimedCommandLine dt = new DateTimedCommandLine(newTime, toExecute.GetLine());
        Execute(dt, threadType);
    }


   
    #region TO CODE
    public void Execute(WaitEndOfPreviousWithDelayCommandLines toExecute) {
        StartCoroutine(ExecuteStepByStep(toExecute));

    }
    public IEnumerator ExecuteStepByStep(List<CommandLine> commands)
    {
        yield return ExecuteStepByStep(commands.Select(k=>k).ToList<ICommandLine>()) ;
    }
    public IEnumerator ExecuteStepByStep(List<ICommandLine> commands)
    {
        WaitEndOfPreviousWithDelayCommandLines tmp = new WaitEndOfPreviousWithDelayCommandLines(commands);
        yield return ExecuteStepByStep(tmp);

    }
    private IEnumerator ExecuteStepByStep(WaitEndOfPreviousWithDelayCommandLines commands)
    {
       List<RelativeTimedCommandLine> toExecute = commands.GetCommandesToExecute();
        ExecutionStatus status = null;

        for (int i = 0; i < toExecute.Count; i++)
        {
            yield return new WaitForSeconds(toExecute[i].GetWantedExecuteTimeInSeconds());
            m_coroutineCmdManager.TryToExecutre(toExecute[i], out status);
            yield return new WaitWhile(() => !status.HasFinish());
        }
    }
    #endregion
}

public struct InWaitingToBeCall
{
    private InWaitingToBeCall(ICommandLine cmd, CommandAuctionExecuter executer)
    {
        m_command = cmd;
        m_executer = executer;
    }
    public ICommandLine m_command;
    public CommandAuctionExecuter m_executer;

    public void DoTheThing()
    {
        m_executer.Execute(m_command);
        m_waiting.Remove(this);
        m_command = null;
        m_executer = null;
        m_toRecyle.Enqueue(this);

    }
    public static InWaitingToBeCall ClaimWaitingContainer(ICommandLine cmd, CommandAuctionExecuter executer)
    {
        InWaitingToBeCall capsule;
        if (m_toRecyle.Count == 0)
        {
            capsule = new InWaitingToBeCall(cmd, executer);
        }
        else
        {
            capsule = m_toRecyle.Dequeue();
            capsule.m_command = cmd;
            capsule.m_executer = executer;
        }
        m_waiting.Add(capsule);
        return capsule;
    }


    public static List<InWaitingToBeCall> m_waiting = new List<InWaitingToBeCall>();
    public static Queue<InWaitingToBeCall> m_toRecyle = new Queue<InWaitingToBeCall>();
}
