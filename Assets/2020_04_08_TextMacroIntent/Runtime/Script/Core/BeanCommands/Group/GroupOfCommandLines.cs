using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfCommandLines : List<ICommandLine>, IGroupOfChonogicalCommandLine
{
    public GroupOfCommandLines(IEnumerable<ICommandLine> collection) : base(collection)
    {
        
    }

    public ICommandLine[] GetLines()
    {
        return this.ToArray();
    }
}
