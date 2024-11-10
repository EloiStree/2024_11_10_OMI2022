using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_SetCursor_OverProcess_PX_LRTD : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32CursorPointRelative m_whereToMove;
}
