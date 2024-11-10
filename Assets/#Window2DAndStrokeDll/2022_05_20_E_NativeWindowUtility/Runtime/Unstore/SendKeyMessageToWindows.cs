
using System;
using System.Collections.Generic;

public class SendKeyMessageToWindows
{



    #region CONST
    private const Int32 WM_KEYDOWN = 0x0100;
    private const Int32 WM_KEYUP = 0x0101;
    #endregion



    public class Utility
    {

        public static void FindWindowParentProcess(string windowName, out
    IntPtrWrapGet found)
            => WindowIntPtrUtility.FindWindow(null, windowName, out found);
        public static void FindChildrenOf(
    IntPtrWrapGet pointer, out List<
    IntPtrWrapGet> found)
        {
            FindChildrenOf(pointer, out found);
        }
        public static void GetParentWithChildrensOf(
    IntPtrWrapGet parentPointer, out List<
    IntPtrWrapGet> found)
        {
            FindChildrenOf(parentPointer, out found);
            found.Add(parentPointer);
        }
    }

    /// <summary>
    /// YOu don't have time to check for point,just broadcast the shit on those
    /// </summary>


    public static void SendKeyDown(User32PostMessageKeyEnum Key, in IntPtrWrapGet target, in bool usePost)
    {
        if (usePost)
            WindowIntPtrUtility.PostMessage(target, WM_KEYDOWN, (int)Key, 0);
        else WindowIntPtrUtility.SendMessage(target, WM_KEYDOWN, (int)Key, 0);
    }

    public static void SendKeyUp(User32PostMessageKeyEnum Key, in IntPtrWrapGet target, in bool usePost)
    {
        if (usePost)
            WindowIntPtrUtility.PostMessage(target, WM_KEYUP, (int)Key, 0);
        else WindowIntPtrUtility.SendMessage(target, WM_KEYUP, (int)Key, 0);
    }


    internal static void SendKeyDownToProcessChildren(User32PostMessageKeyEnum Key, IntPtrWrapGet processId, in bool usePost = true)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
        if (prs.Length <= 0) return;
        IntPtrWrapGet pr = prs[0];
        foreach (IntPtrWrapGet p in prs)
        {
            SendKeyDown(Key, in processId, in usePost);
        }


    }
    internal static void SendKeyUpToProcessChildren(User32PostMessageKeyEnum Key, IntPtrWrapGet processId, in bool usePost = true)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
        if (prs.Length <= 0) return;
        IntPtrWrapGet pr = prs[0];
        foreach (IntPtrWrapGet p in prs)
        {
            SendKeyUp(Key, in processId, in usePost);
        }
    }

    //public static Process FindProcess(int Handle)
    //{
    //    foreach (Process p in Process.GetProcesses())
    //    {
    //        if (p.Id == Handle)
    //        {
    //            return p;
    //        }
    //    }

    //    return null;
    //}


    public static void SendMouseLeftUp(IntPtrWrapGet processId, float l2rPct, float b2tPct)
    {
        int width = 900;
        int height = 900;
        int x = (int)(height * (1f - b2tPct));
        int y = (int)(width * (l2rPct));

        SendMouseLeftUp(processId, x, y);
    }

    //[DllImport("user32.dll")]
    //static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);


    private static int GetLPARAMS(int x, int y)
    {
        //https://stackoverflow.com/a/11161632/13305320
        //https://stackoverflow.com/questions/46306860/i-want-to-send-mouse-click-with-sendmessage-but-its-not-working-what-wrong-wit
        return ((y << 16) | (x & 0xFFFF));
    }

    public const uint WM_LBUTTONDOWN = 0x201; //Left mousebutton down
    public const uint WM_LBUTTONUP = 0x202;   //Left mousebutton up
    public const uint WM_LBUTTONDBLCLK = 0x203; //Left mousebutton doubleclick
    public const uint WM_RBUTTONDOWN = 0x204; //Right mousebutton down
    public const uint WM_RBUTTONUP = 0x205;   //Right mousebutton up
    public const uint WM_RBUTTONDBLCLK = 0x206; //Right mousebutton do
    //
    public static void SendMouseLeftDown(IntPtrWrapGet processId, float l2rPct, float b2tPct)
    {
        //https://docs.microsoft.com/en-us/answers/questions/81213/press-a-button-on-a-calculator-with-sendmessage-wi.html
        //WM_LBUTTONDOWN use the coordinate is relative to the upper-left corner of the client area.
        int width = 900;
        int height = 900;
        int x = (int)(height * (1f - b2tPct));
        int y = (int)(width * (l2rPct));

        SendMouseLeftDown(processId, x, y);
    }

    public static void SendMouseLeftDown(IntPtrWrapGet processId, int x, int y)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);

        if (prs.Length <= 0) return;
        foreach (IntPtrWrapGet p in prs)
        {
            IntPtr lParam = (IntPtr)((y << 16) | x);
            IntPtr wParam = IntPtr.Zero;
            WindowIntPtrUtility.PostMessage(p, (uint)WM_LBUTTONDOWN, wParam, lParam);
            //UnityEngine.Debug.Log("PR:" + processId + " - " + (int)p + "l" + lParam +"x"+x+"y"+y );
        }
    }
    public static void SendMouseLeftUp(IntPtrWrapGet processId, int x, int y)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
        //uint wParam = 0x0001; // Additional parameters for the click (e.g. Ctrl)
        if (prs.Length <= 0) return;
        foreach (IntPtrWrapGet p in prs)
        {
            IntPtr lParam = (IntPtr)((y << 16) | x);
            IntPtr wParam = IntPtr.Zero;
            WindowIntPtrUtility.PostMessage(p, (uint)WM_LBUTTONUP, wParam, lParam);
        }
    }

    public static void SendMouseRightDown(IntPtrWrapGet processId, int x, int y)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);

        if (prs.Length <= 0) return;
        foreach (IntPtrWrapGet p in prs)
        {
            // GetLPARAMS(x,y)
            // IntPtr lParam = (IntPtr)((y << 16) | x);
            IntPtr lParam = (IntPtr)GetLPARAMS(x, y);
            IntPtr wParam = IntPtr.Zero;
            WindowIntPtrUtility.PostMessage(p, (uint)WM_LBUTTONDOWN, wParam, lParam);
            UnityEngine.Debug.Log("PR:" + processId + " - " + p.GetAsInt() + "l" + lParam + "x" + x + "y" + y);
        }
    }
    public static void SendMouseRightUp(IntPtrWrapGet processId, int x, int y)
    {
        IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
        //uint wParam = 0x0001; // Additional parameters for the click (e.g. Ctrl)
        if (prs.Length <= 0) return;
        foreach (IntPtrWrapGet p in prs)
        {
            // GetLPARAMS(x,y)
            IntPtr lParam = (IntPtr)GetLPARAMS(x, y);
            IntPtr wParam = IntPtr.Zero;
            WindowIntPtrUtility.PostMessage(p, (uint)WM_LBUTTONUP, wParam, lParam);
        }
    }


    internal static void RequestPastActionBroadcast(IntPtrWrapGet processId, bool andRemoveV = true)
    {
        SendKeyDown(User32PostMessageKeyEnum.VK_LCONTROL, processId, true);
        SendKeyDown(User32PostMessageKeyEnum.VK_V, processId, true);
        SendKeyUp(User32PostMessageKeyEnum.VK_V, processId, true);
        SendKeyUp(User32PostMessageKeyEnum.VK_LCONTROL, processId, true);
        if (andRemoveV)
        {
            SendKeyDown(User32PostMessageKeyEnum.VK_BACK, processId, true);
            SendKeyUp(User32PostMessageKeyEnum.VK_BACK, processId, true);
            SendKeyDown(User32PostMessageKeyEnum.VK_BACK, processId, true);
            SendKeyUp(User32PostMessageKeyEnum.VK_BACK, processId, true);
        }
    }
    //public static void RequestPastAction(int processId)
    //{
    //        SendMessage((IntPtr)processId, WM_PASTE, 0,0);
    //}

    public static void SendKeyClick(User32PostMessageKeyEnum key, IntPtrWrapGet processId, bool usePost)
    {
        SendKeyDown(key, processId, usePost);
        SendKeyUp(key, processId, usePost);
    }

    public const int WM_CUT = 0x300;
    public const int WM_COPY = 0x301;
    public const int WM_PASTE = 0x302;
    public const int WM_CLEAR = 0x303;
}

