using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This bean request the command line to be executed one after an other but with a delay when the command finish.
/// </summary>
public class WaitEndOfPreviousWithDelayCommandLines : GroupOfChonogicalCommandLines
{
    private List<RelativeTimedCommandLine> m_commandsToExecuteOfAfterAnOther = new List<RelativeTimedCommandLine>();
    public WaitEndOfPreviousWithDelayCommandLines()
    {
    }
    public WaitEndOfPreviousWithDelayCommandLines(List<ICommandLine> commands)
    {
        for (int i = 0; i < commands.Count; i++)
        {
            Add(0, commands[i].GetLine());

        }
    }

    public void Add(uint timeInMilliseconds, string commandLine)
    {
        m_commandsToExecuteOfAfterAnOther.Add(new RelativeTimedCommandLine(timeInMilliseconds, commandLine));
    }
    public void Add(RelativeTimedCommandLine command)
    {
        m_commandsToExecuteOfAfterAnOther.Add(command);
    }
    public List<RelativeTimedCommandLine> GetCommandesToExecute() { return m_commandsToExecuteOfAfterAnOther; }

    public void Clear() { m_commandsToExecuteOfAfterAnOther.Clear(); }

    public override ICommandLine[] GetLines()
    {
        return m_commandsToExecuteOfAfterAnOther.ToArray();
    }
}