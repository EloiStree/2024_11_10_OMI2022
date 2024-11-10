using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_SetCursor_OverProcess_PCT_LRTD : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32PercentPointRelative m_whereToMove;
}

