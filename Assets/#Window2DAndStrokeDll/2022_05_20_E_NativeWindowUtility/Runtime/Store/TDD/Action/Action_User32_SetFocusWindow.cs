using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_User32_SetFocusWindow : IUser32Action
{
    public IntPtrProcessId m_processId;
}
