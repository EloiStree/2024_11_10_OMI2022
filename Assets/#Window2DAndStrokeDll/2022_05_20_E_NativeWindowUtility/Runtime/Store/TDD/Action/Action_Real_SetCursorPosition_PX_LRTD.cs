using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_SetCursorPosition_PX_LRTD : IUser32Action
{
    public User32CursorPointRelative m_whereToMove;
}
