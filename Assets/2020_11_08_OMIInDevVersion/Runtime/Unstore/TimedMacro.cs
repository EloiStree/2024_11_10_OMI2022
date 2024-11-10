
using System;
using System.Collections.Generic;

public class JomiTimedMacro
{
    public static List<string> GetCommandsFor(TimedCommandLines commands, DateTime now, int delayForStartInMs)
    {
        List<string> cmds = new List<string>();
        now = now.AddMilliseconds(delayForStartInMs);
        for (int i = 0; i < commands.m_toSend.Count; i++)
        {
            cmds.Add(
                GetRawCommand(
                    now.AddMilliseconds(
                        commands.m_toSend[i].m_timeInMs).TimeOfDay, commands.m_toSend[i].m_commandLine));

        }
        return cmds;
    }
    public static string GetRawCommand(uint hour, uint mm, uint second, uint millisecond, string cmd)
    {
        return string.Format("t:{0}-{1}-{2}-{3}:", hour, mm, second, millisecond) + cmd;
    }
    public static string GetRawCommand(TimeSpan when, string cmd)
    {
        return GetRawCommand((uint)when.TotalHours, (uint)when.Minutes, (uint)when.Seconds, (uint)when.Milliseconds, cmd);
    }
}