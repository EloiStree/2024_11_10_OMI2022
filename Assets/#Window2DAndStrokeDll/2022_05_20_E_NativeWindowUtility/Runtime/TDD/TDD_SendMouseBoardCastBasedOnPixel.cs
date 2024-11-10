using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class TDD_SendMouseBoardCastBasedOnPixel : MonoBehaviour
{

    public int m_processTarget;
    public int m_xLeftToRight;
    public int m_yTopToBottom;
    public int m_xLeftToRightAbsolute;
    public int m_yTopToBottomAbsolute;

    public bool m_use;

    public bool m_useForground;
    public bool m_usePost;

    public IEnumerator Start()
    {
        if (m_use)
        {
            yield return new WaitForSeconds(3);
            TryToSimulateClick();
        }    
        
    }

    [ContextMenu("Try to simulate click")]
    public void TryToSimulateClick()
    {
        StartCoroutine(CoroutineTryToSimulateClick());

    }

    public IEnumerator CoroutineTryToSimulateClick()
    {

       yield return PostMouseUtility.SendMouseLeftDown(m_processTarget,
                m_xLeftToRight, m_yTopToBottom);
        yield return PostMouseUtility.SendMouseLeftUp(m_processTarget,
                m_xLeftToRight, m_yTopToBottom);


    }

}


public class PostMouseUtility {

    public static float m_timeToSetForground=0.05f;
    public static float m_timeBetweenClick = 0.001f;
    public static IEnumerator SendMouseLeftDown(int processId, int x, int y, bool useForground=true, bool usePost=true)
    {
        if (useForground) { 
            WindowIntPtrUtility.SetForegroundWindow(processId);
            yield return new WaitForSeconds (m_timeToSetForground);
        }
        if (usePost)
            PostMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));
        else SendMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));

        yield return new WaitForSeconds(m_timeBetweenClick);

        if (usePost)
            PostMessage(processId, WM_LBUTTONDOWN, 0, MakeLParam(x, y));
        else SendMessage(processId, WM_LBUTTONDOWN, 0, MakeLParam(x, y));
    }
    public static IEnumerator SendMouseLeftUp(int processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        if (useForground)
        {
            WindowIntPtrUtility.SetForegroundWindow(processId);
            yield return new WaitForSeconds(m_timeToSetForground);
        }
        if (usePost)
            PostMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));
        else SendMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));
        yield return new WaitForSeconds(m_timeBetweenClick);
        if (usePost)
            PostMessage(processId, WM_LBUTTONUP, 0, MakeLParam(x, y));
        else SendMessage(processId, WM_LBUTTONUP, 0, MakeLParam(x, y));
    
    }

    public static void SendMouseLeftDownDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost, 
            Eloi.E_EnumUtility.LeftRightMidEnum.Left, Eloi.E_EnumUtility.PressionTypeEnum.Press);
    }
    internal static void MoveTo(IntPtrWrapGet processId, int x, int y, bool useForground, bool usePost)
    {
        if (useForground)
        {
            WindowIntPtrUtility.SetForegroundWindow(processId);
        }
        if (usePost)
            WindowIntPtrUtility.PostMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));
        else WindowIntPtrUtility.SendMessage(processId, WM_MOUSEMOVE, 0, MakeLParam(x, y));
    }
    private static void SendMouseActionDirect(IntPtrWrapGet processId, int x, int y, bool useForground, bool usePost,
        Eloi.E_EnumUtility.LeftRightMidEnum mouseType,
        Eloi.E_EnumUtility.PressionTypeEnum pressType)
    {
        int mouseTypeId = 0;
        switch (mouseType)
        {
            case Eloi.E_EnumUtility.LeftRightMidEnum.Left:
                mouseTypeId = pressType==Eloi.E_EnumUtility.PressionTypeEnum.Press?
                    WM_LBUTTONDOWN: WM_LBUTTONUP;
                break;
            case Eloi.E_EnumUtility.LeftRightMidEnum.Middle:
                mouseTypeId = pressType == Eloi.E_EnumUtility.PressionTypeEnum.Press ?
                    WM_MBUTTONDOWN : WM_MBUTTONUP;
                break;
            case Eloi.E_EnumUtility.LeftRightMidEnum.Right:
                mouseTypeId = pressType == Eloi.E_EnumUtility.PressionTypeEnum.Press ?
                    WM_RBUTTONDOWN : WM_RBUTTONUP;
                break;
            default:
                break;
        }
        if (useForground)
        {
            WindowIntPtrUtility.SetForegroundWindow(processId);
        }

        //ShowCursor(false);
        //Thread.Sleep(100);
        //WindowIntPtrUtility.Point pt = new WindowIntPtrUtility.Point();
        //WindowIntPtrUtility.Point ptc = new WindowIntPtrUtility.Point();
        ////Test
        //WindowIntPtrUtility.GetCursorPos(ref pt);
        //ClientToScreen((IntPtr)processId, ref ptc);
        // WindowIntPtrUtility.SetCursorPos(ptc.x, ptc.y);
        int zero = 0;
        if (usePost)
           WindowIntPtrUtility. PostMessage(processId, WM_MOUSEMOVE, zero, MakeLParam(x, y));
        else WindowIntPtrUtility.SendMessage(processId, WM_MOUSEMOVE, zero, MakeLParam(x, y));

        if (usePost)
            WindowIntPtrUtility.PostMessage(processId,(uint) ( mouseTypeId), zero, MakeLParam(x, y));
        else WindowIntPtrUtility.SendMessage(processId, (uint)( mouseTypeId), zero, MakeLParam(x, y));
        // WindowIntPtrUtility.SetCursorPos(pt.x, pt.y);

        //Thread.Sleep(100);
        //ShowCursor(true);
        //{ 
        //TargetChildrenProcessIntPtr p = new TargetChildrenProcessIntPtr();
        //p.SetAsInt(processId);
        //User32KeyStrokeManager.SendKeyPostMessage(p,
        //    User32PostMessageKeyEnum.VK_LBUTTON,
        //    pressType==Eloi.E_EnumUtility.PressionTypeEnum.Press?
        //    User32PressionType.Press: User32PressionType.Release,
        //    true);
        //}
    }
    [DllImport("user32.dll")]
    static extern int ShowCursor(bool bShow);
    [DllImport("user32.dll")]
    static extern bool ClientToScreen(IntPtr hWnd, ref WindowIntPtrUtility.Point lpPoint);

    internal static void SendMouseMiddleUpDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost,
               Eloi.E_EnumUtility.LeftRightMidEnum.Middle, Eloi.E_EnumUtility.PressionTypeEnum.Release);
    }

    internal static void SendMouseMiddleDownDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost,
               Eloi.E_EnumUtility.LeftRightMidEnum.Middle, Eloi.E_EnumUtility.PressionTypeEnum.Press);
    }

    internal static void SendMouseRightUpDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost,
               Eloi.E_EnumUtility.LeftRightMidEnum.Right, Eloi.E_EnumUtility.PressionTypeEnum.Release);
    }

    internal static void SendMouseRightDownDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost,
               Eloi.E_EnumUtility.LeftRightMidEnum.Right, Eloi.E_EnumUtility.PressionTypeEnum.Press);
    }

    public static void SendMouseLeftUpDirect(IntPtrWrapGet processId, int x, int y, bool useForground = true, bool usePost = true)
    {
        SendMouseActionDirect(processId, x, y, useForground, usePost,
               Eloi.E_EnumUtility.LeftRightMidEnum.Left, Eloi.E_EnumUtility.PressionTypeEnum.Release);
    }

    public const int WM_MOUSEMOVE = 0x200; //Left mousebutton down
    public const int WM_LBUTTONDOWN = 0x201; //Left mousebutton down
    public const int WM_LBUTTONUP = 0x202;   //Left mousebutton up
    public const int WM_LBUTTONDBLCLK = 0x203; //Left mousebutton doubleclick
    public const int WM_RBUTTONDOWN = 0x204; //Right mousebutton down
    public const int WM_RBUTTONUP = 0x205;   //Right mousebutton up
    public const int WM_RBUTTONDBLCLK = 0x206; //Right mousebutton do

    public const int WM_MBUTTONDOWN = 0x207; //Mid mousebutton down
    public const int WM_MBUTTONUP = 0x208;   //Mid mousebutton up

    private static IntPtr CreateLParam(int LoWord, int HiWord)
    {
        return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
    }
    public static int MakeLParam(int x, int y) => (y << 16) | (x & 0xFFFF);
    private static int GetLPARAMS(int x, int y)
    {
        //https://stackoverflow.com/a/11161632/13305320
        //https://stackoverflow.com/questions/46306860/i-want-to-send-mouse-click-with-sendmessage-but-its-not-working-what-wrong-wit
        return ((y << 16) | (x & 0xFFFF));
    }

   

    [DllImport("User32.dll")]
    public static extern Int32 SendMessage(
          int hWnd,               // handle to destination window
          int Msg,                // message
          int wParam,             // first message parameter
          [MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

    [DllImport("User32.dll")]
    public static extern Int32 SendMessage(
        int hWnd,               // handle to destination window
        int Msg,                // message
        int wParam,             // first message parameter
        int lParam);            // second message parameter

    [DllImport("User32.dll")]
    public static extern Int32 SendMessage(
    int hWnd,
    int Msg,
    int wParam,
    IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern bool PostMessage(int hWnd, int Msg, int wParam, int lParam);


}