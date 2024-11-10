using System;
using System.Collections.Generic;

/// <summary>
/// The role of this class is to store commands that need to be executed at precised timing from a start point.
/// </summary>
public class RelativeToStartDateStepByStepCommandLines : GroupOfChonogicalCommandLines
{

    public ListOfRelativeTimedCommandLines m_commands;
    public RelativeToStartDateStepByStepCommandLines(ListOfRelativeTimedCommandLines commands)
    {
        m_commands = commands;
    }

    public List<DateTimedCommandLine> GetCommandsToExecuteFrom(DateTime startPoint, bool sorted)
    {
        return m_commands.GetCommandesFromDateForEach(startPoint, sorted);
    }
    public override ICommandLine[] GetLines()
    {
        return m_commands.GetLines();
    }
}
