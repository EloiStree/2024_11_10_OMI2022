using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User32MouseManagerMono : MonoBehaviour
{
    
}

public interface IUser32CoordinateContextConverter
{
    public void GetAbsoluteFromRelativeX(int relativeX, out int absoluteX);
    public void GetAbsoluteFromRelativeY(int relativeY, out int absoluteY);
    public void GetRelativeFromAbsoluteX(int absoluteX, out int relativeX);
    public void GetRelativeFromAbsoluteY(int absoluteY, out int relativeY);
}
public class Converter_User32RectPadSource : IUser32CoordinateContextConverter {
    public DeductedInfoOfWindowSizeWithSource m_windowRectZone;

    public Converter_User32RectPadSource(DeductedInfoOfWindowSizeWithSource source)
    {
        m_windowRectZone = source;
    }

    public void GetAbsoluteFromRelativeX(int relativeX, out int absoluteX)
    {
        m_windowRectZone.m_frameSize.GetAbsoluteFromRelativePixelBot2Top(relativeX, out absoluteX);
    }

    public void GetAbsoluteFromRelativeY(int relativeY, out int absoluteY)
    {
        m_windowRectZone.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(relativeY, out absoluteY);
    }

    public void GetRelativeFromAbsoluteX(int absoluteX, out int relativeX)
    {
        m_windowRectZone.m_frameSize.GetTopToBottomPixelFor(absoluteX, out relativeX);
    }

    public void GetRelativeFromAbsoluteY(int absoluteY, out int relativeY)
    {
        m_windowRectZone.m_frameSize.GetLeftToRightPixelFor(absoluteY, out relativeY);
    }
}

public class User32MouseManager
{
  
    public static void LeftClick(IntPtrWrapGet p, int pixelLeft2RightRelative, int pixelTop2BotRelative, bool useForground = true)
    {
        PostMouseUtility.SendMouseLeftDownDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);
        PostMouseUtility.SendMouseLeftUpDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);
    }
    public static void LeftClick(int absoluteX, int absoluteY)
    {
//        MouseOperations.SetCursorPosition(absoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown, absoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp, absoluteX, absoluteY);
    }
  
    public static void RightClick(IntPtrWrapGet p, int pixelLeft2RightRelative, int pixelTop2BotRelative, bool useForground=true)
    {
        PostMouseUtility.SendMouseRightDownDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);
        PostMouseUtility.SendMouseRightUpDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);

    }
    public static void RightClick(int absoluteX, int absoluteY)
    {
        //MouseOperations.SetCursorPosition(abasoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown, absoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp, absoluteX, absoluteY);
    }

    public static void MiddleClick(IntPtrWrapGet p, int pixelLeft2RightRelative, int pixelTop2BotRelative, bool useForground = true)
    {
        PostMouseUtility.SendMouseMiddleDownDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);
        PostMouseUtility.SendMouseMiddleUpDirect(p, pixelLeft2RightRelative, pixelTop2BotRelative, useForground, true);

    }
    public static void MiddleClick(int absoluteX, int absoluteY)
    {
     //   MouseOperations.SetCursorPosition(abasoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleDown, absoluteX, absoluteY);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleUp, absoluteX, absoluteY);
    }

}


