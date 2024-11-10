using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_ResizeCurrentWindowToSquareZone_PX_LRDT : IUser32Action
{
    public User32RelativePixelPointLRTB m_downLeftCorner;
    public User32RelativePixelPointLRTB m_topRightCorner;
}
public struct Action_ResizeCurrentWindowToNative_PX_LRTD : IUser32Action
{
    public User32RelativePixelPointLRTB m_startPoint;
    public int m_width;
    public int m_height;
}
public struct Action_ResizeCurrentWindowAroundPoint_PX_LRTD : IUser32Action
{
    public User32RelativePixelPointLRTB m_centerPoint;
    public int m_width;
    public int m_height;
}

public struct Action_ResizeProcessWindowToSquareZone_PX_LRDT : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32RelativePixelPointLRTB m_downLeftCorner;
    public User32RelativePixelPointLRTB m_topRightCorner;
}
public struct Action_ResizeProcessWindowToNative_PX_LRTD : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32RelativePixelPointLRTB m_startPoint;
    public int m_width;
    public int m_height;
}
public struct Action_ResizeProcessWindowAroundPoint_PX_LRTD : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32RelativePixelPointLRTB m_centerPoint;
    public int m_width;
    public int m_height;
}
