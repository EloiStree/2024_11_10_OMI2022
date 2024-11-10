
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface ICommandLine
{
     string GetLine();
}

[System.Serializable]
public class CommandLine : ICommandLine
{
    public CommandLine(string cmd)
    {
        if (cmd == null)
            cmd = "";
        m_commandAsText = cmd;
    }
    [SerializeField] string m_commandAsText = "";
    public string GetLine() { return m_commandAsText; }

    public static List<ICommandLine> GetLinesFromCharSplite(string text, char spliter = '\n')
    {
        List<ICommandLine> lines = new List<ICommandLine>();
        string[] tokens = text.Split(spliter);
        for (int i = 0; i < tokens.Length; i++)
        {
            lines.Add(new CommandLine(tokens[i]));
        }
        return lines;
    }


    internal static void Shuffle(ref List<ICommandLine> cmds)
    {
        throw new NotImplementedException();
    }

    internal static void Reverse(ref List<ICommandLine> cmds)
    {
        throw new NotImplementedException();
    }
}

public class ICommandLineUtility
{


}





public class TimedMacro : GroupOfChonogicalCommandLines
{
    public List<RelativeTimedCommandLine> m_commands = new List<RelativeTimedCommandLine>();
    public void Add(float time, CommandLine command)
    {
        m_commands.Add(new RelativeTimedCommandLine(time,  command.GetLine()));
    }
    public void Add(RelativeTimedCommandLine command)
    {
        m_commands.Add(command);
    }
    public void Clear() { m_commands.Clear(); }

    public override ICommandLine[] GetLines()
    {
        return m_commands.OrderBy(k=>k.GetWantedExecuteTimeInSeconds()).ToArray<CommandLine>();
    }
}
