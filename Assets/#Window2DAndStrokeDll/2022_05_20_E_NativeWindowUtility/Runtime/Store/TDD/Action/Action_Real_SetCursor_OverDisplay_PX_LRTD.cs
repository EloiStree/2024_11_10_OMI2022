using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_SetCursor_OverDisplayID_PX_LRTD : IUser32Action
{

    public int m_displayID;
    public User32CursorPointRelative m_screenRelative;
}
[System.Serializable]
public struct Action_Real_SetCursor_OverDisplayName_PX_LRTD : IUser32Action
{

    public string m_displayNameID;
    public User32CursorPointRelative m_screenRelative;
}


