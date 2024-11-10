using System.Collections.Generic;
using System.Linq;

public class ListOfCommandLines
{

    private List<ICommandLine> m_lines = new List<ICommandLine>();
    
    public ListOfCommandLines() { }
    public ListOfCommandLines(IEnumerable<string> commands)
    {
        foreach (string item in commands)
        {
            AddAtEnd(new CommandLine(item));
        }

    }
    public ListOfCommandLines(IEnumerable<ICommandLine> commands)
    {
        m_lines = commands.ToList();

    }
    public void AddCommandsFromTextReturnLine(string text)
    {
        AddAtEnd(CommandLine.GetLinesFromCharSplite(text));
    }

    public void AddAtStart(ICommandLine cmd)
    {
        m_lines.Insert(0, cmd);
    }
    public void AddAtEnd(ICommandLine cmd)
    {
        int index = m_lines.Count - 1;
        if (index < 0)
            index = 0;
        m_lines.Insert(index, cmd);
    }
    public void AddAtStart(IEnumerable<ICommandLine> cmds)
    {
        m_lines.InsertRange(0, cmds);
    }
    public void AddAtEnd(IEnumerable<ICommandLine> cmds)
    {
        int index = m_lines.Count - 1;
        if (index < 0)
            index = 0;
        m_lines.InsertRange(index, cmds);
    }

    public List<ICommandLine> GetCommands()
    {
        return m_lines;
    }
}