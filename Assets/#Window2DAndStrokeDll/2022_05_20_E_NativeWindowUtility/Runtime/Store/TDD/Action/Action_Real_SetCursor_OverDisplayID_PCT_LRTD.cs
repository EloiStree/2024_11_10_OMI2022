using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_SetCursor_OverDisplayID_PCT_LRTD : IUser32Action
{

    public int m_displayID;
    public User32PercentPointRelative m_screenRelative;
}
[System.Serializable]
public struct Action_Real_SetCursor_OverDisplayName_PCT_LRTD : IUser32Action
{

    public string m_displayNameID;
    public User32PercentPointRelative m_screenRelative;
}
