
using System;
using System.Runtime.InteropServices;

public class MouseOperationsUtility {


    public static MouseOperations.MouseEventFlags GetTypeEventFrom(User32MouseClassicButton button, User32PressionType pressType) {

        if (button == User32MouseClassicButton.Left && pressType == User32PressionType.Press)
            return MouseOperations.MouseEventFlags.LeftDown;
        if (button == User32MouseClassicButton.Left && pressType == User32PressionType.Release)
            return MouseOperations.MouseEventFlags.LeftUp;
        if (button == User32MouseClassicButton.Right && pressType == User32PressionType.Press)
            return MouseOperations.MouseEventFlags.RightDown;
        if (button == User32MouseClassicButton.Right && pressType == User32PressionType.Release)
            return MouseOperations.MouseEventFlags.RightUp;
        if (button == User32MouseClassicButton.Middle && pressType == User32PressionType.Press)
            return MouseOperations.MouseEventFlags.MiddleDown;
        if (button == User32MouseClassicButton.Middle && pressType == User32PressionType.Release)
            return MouseOperations.MouseEventFlags.MiddleUp;
        return MouseOperations.MouseEventFlags.LeftDown; 
    }
}
public class MouseOperations
{
    // Source and credit to
    //httpsstackoverflow.comquestions2416748how-do-you-simulate-mouse-click-in-c

    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);


    public static void SetCursorPosition(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    private static MousePoint m_previousPosition;
    public static MousePoint GetCursorPosition(bool exceptionIfNotCursor = true)
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint)
        {
            if (exceptionIfNotCursor)
                throw new Exception("Humm");
            currentMousePoint = new MousePoint(0, 0);
        }
        return currentMousePoint;
    }

    internal static void MouseEventWithCurrentPosition(MouseEventFlags eventType)
    {
        MousePoint mp = GetCursorPosition();
        MouseEvent(eventType, mp);
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        SetCursorPos(position.X, position.Y);
        mouse_event
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;
    }
    public static void MouseEvent(MouseEventFlags value, MousePoint point)
    {

        SetCursorPos(point.X, point.Y);
        mouse_event
            ((int)value,
             point.X,
             point.Y,
             0,
             0)
            ;
    }
    public static void MouseEvent(MouseEventFlags value, int x, int y)
    {

        SetCursorPos(x, y);
        mouse_event
            ((int)value,
            x,
           y,
             0,
             0)
            ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static void MoveCursorPosition(int xPixelLeft2Right, int yPixelTop2Down)
    {
        GetCursorPos(out MousePoint pt);
        SetCursorPos(pt.X + xPixelLeft2Right, pt.Y + yPixelTop2Down);
    }
}