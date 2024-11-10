using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelativeTimedCommandLine : CommandLine
{
    public RelativeTimedCommandLine(float timeInSeconds, string command) : base(command)
    {
        m_timeInMilliseconds = (uint)(timeInSeconds * 1000f);
    }
    public RelativeTimedCommandLine(uint timeInMilliseconds, string command) : base(command)
    {
        m_timeInMilliseconds = timeInMilliseconds;
    }
    private uint m_timeInMilliseconds = 0;
    public float GetWantedExecuteTimeInSeconds() { return m_timeInMilliseconds / 1000; }
    public float GetWantedExecuteTimeInMilliseconds() { return m_timeInMilliseconds ; }

    internal string GetCommandLine()
    {
        throw new NotImplementedException();
    }
}