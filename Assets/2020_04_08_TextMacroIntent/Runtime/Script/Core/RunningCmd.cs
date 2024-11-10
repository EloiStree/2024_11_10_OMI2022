using UnityEngine;

public class RunningCmd
{
    private ICommandLine cmd;
    private ExecutionStatus status;
    private Coroutine coroutine;

    public RunningCmd(ICommandLine cmd, ExecutionStatus status, Coroutine coroutine)
    {
        this.cmd = cmd;
        this.status = status;
        this.coroutine = coroutine;
    }

    public bool IsFinish()
    {
        return status.HasFinish();
    }

    public Coroutine GetCoroutineToStop() { return coroutine; }
}