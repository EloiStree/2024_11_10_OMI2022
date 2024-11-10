using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DateTimedCommandLine : CommandLine
{
    public DateTimedCommandLine(DateTime whenToExecute, string command) : base(command)
    {
        m_whenToExecute = whenToExecute;
    }
    private DateTime m_whenToExecute = DateTime.Now;
    public DateTime GetWantedExecuteTime() { return m_whenToExecute; }
}