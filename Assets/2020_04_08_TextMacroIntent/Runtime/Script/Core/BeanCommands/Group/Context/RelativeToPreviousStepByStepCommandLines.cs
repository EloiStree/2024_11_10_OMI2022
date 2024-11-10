using System;
using System.Collections.Generic;

/// <summary>
/// The role of this class is to store commands that need to be executed one after an other with delay but without waiting the answer of the previous.
/// </summary>
public class RelativeToPreviousStepByStepCommandLines : GroupOfChonogicalCommandLines
{

    public ListOfRelativeTimedCommandLines m_commands;


    public RelativeToPreviousStepByStepCommandLines(ListOfRelativeTimedCommandLines commands)
    {
        m_commands = commands;
    }

    public List<DateTimedCommandLine> GetCommandsToExecuteFrom(DateTime startPoint, bool sorted)
    {
        return m_commands.GetCommandesFromDateButRelativeToPrevious(startPoint, sorted);
    }

    public override ICommandLine[] GetLines()
    {
        return m_commands.GetLines();
    }
}