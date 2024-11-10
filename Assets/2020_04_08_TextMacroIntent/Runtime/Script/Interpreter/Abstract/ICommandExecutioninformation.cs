using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICommandExecutioninformation
{
    bool DoesItImpactTheThreadContinuum();
    bool DoesItRequireTimeToExecute();
    bool IsItLaunchingOtherCommands();
    bool IsUsingUnityThread();
}
public class CommandExecutionInformation : ICommandExecutioninformation
{
    public CommandExecutionInformation() : this(false, false, false, true)
    {
    }
    public CommandExecutionInformation(bool isTakingTimeToRun, bool isImpactingThread, bool isLauchingOtherCommand, bool runOnUnitythread)
    {

        m_requireTimeToExecute = isTakingTimeToRun;
        m_isImpactingThread = isImpactingThread;
        m_isLaunchingOtherCommands = isLauchingOtherCommand;
        m_doesItRunOnUnityThread = runOnUnitythread;
    }
    private bool m_requireTimeToExecute;
    private bool m_isImpactingThread;
    private bool m_isLaunchingOtherCommands;
    private bool m_doesItRunOnUnityThread;

    public bool DoesItRequireTimeToExecute()
    {
        return m_requireTimeToExecute;
    }
    public bool DoesItImpactTheThreadContinuum()
    {
        return m_isImpactingThread;
    }
    public bool IsItLaunchingOtherCommands()
    {
        return m_isLaunchingOtherCommands;
    }
    public bool IsUsingUnityThread()
    {
        return m_doesItRunOnUnityThread;
    }
}