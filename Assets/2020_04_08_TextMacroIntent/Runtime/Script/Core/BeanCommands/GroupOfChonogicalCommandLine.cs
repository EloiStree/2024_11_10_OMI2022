using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGroupOfChonogicalCommandLine
{

    ICommandLine[] GetLines();
}

public abstract class GroupOfChonogicalCommandLines : IGroupOfChonogicalCommandLine
{
    public abstract ICommandLine[] GetLines();
}