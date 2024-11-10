using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_MoveCursor_PX_LRTD : IUser32Action
{
    public int m_xPixelLeft2Right;
    public int m_yPixelTop2Down;
}

[System.Serializable]
public struct Action_Real_MoveCursor_PX_LRDT : IUser32Action
{
    public int m_xPixelLeft2Right;
    public int m_yPixelDown2Top;
}
