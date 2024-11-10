using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListOfRelativeTimedCommandLines : GroupOfChonogicalCommandLines
{

    private List<RelativeTimedCommandLine> m_commandsToExecute = new List<RelativeTimedCommandLine>();
    public void Add(uint timeInMilliseconds, string commandLine)
    {
        m_commandsToExecute.Add(new RelativeTimedCommandLine(timeInMilliseconds, commandLine));
    }
    public void Add(RelativeTimedCommandLine command)
    {
        m_commandsToExecute.Add(command);
    }
    public List<RelativeTimedCommandLine> GetCommandesToExecute() { return m_commandsToExecute; }
    public List<DateTimedCommandLine> GetCommandesFromDateForEach(DateTime startTime, bool sorted)
    {

        List<DateTimedCommandLine> result = new List<DateTimedCommandLine>();
        for (int i = 0; i < m_commandsToExecute.Count; i++)
        {
            result.Add(new DateTimedCommandLine(startTime.AddMilliseconds(m_commandsToExecute[i].GetWantedExecuteTimeInMilliseconds()),
                m_commandsToExecute[i].GetCommandLine()));

        }
        if (sorted)
            return result.OrderBy(k => k.GetWantedExecuteTime()).ToList();
        else
            return result;

    }
    public List<DateTimedCommandLine> GetCommandesFromDateButRelativeToPrevious(DateTime startTime, bool sorted)
    {

        List<DateTimedCommandLine> result = new List<DateTimedCommandLine>();
        DateTime index = startTime;
        for (int i = 0; i < m_commandsToExecute.Count; i++)
        {
            index = index.AddMilliseconds(m_commandsToExecute[i].GetWantedExecuteTimeInMilliseconds());
            result.Add(new DateTimedCommandLine(index, m_commandsToExecute[i].GetCommandLine())); 

        }
        if (sorted)
            return result.OrderBy(k => k.GetWantedExecuteTime()).ToList();
        else
            return result;

    }

    public void Clear() { m_commandsToExecute.Clear(); }

    public override ICommandLine[] GetLines()
    {
        return m_commandsToExecute.ToArray();
    }

}


