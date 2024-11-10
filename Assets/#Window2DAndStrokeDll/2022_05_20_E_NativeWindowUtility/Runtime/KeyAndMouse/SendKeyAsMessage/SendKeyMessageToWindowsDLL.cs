
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class SendKeyMessageToWindowsDLL
{
    #region USER32
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);
    #endregion



    #region CONST
    private const Int32 WM_KEYDOWN = 0x0100;
    private const Int32 WM_KEYUP = 0x0101;
    #endregion



    public static void SendKeyDown(in User32PostMessageKeyEnum Key, in IntPtrWrapGet target, in bool usePost)
    {
        SendKeyDown((int)Key, target, usePost);
    }
    public static void SendKeyUp(in User32PostMessageKeyEnum Key, in IntPtrWrapGet target, in bool usePost)
    {
        SendKeyUp((int)Key, target, usePost);
    }
    private static void SendKeyDown(int Key, in IntPtrWrapGet target, in bool usePost)
    {
        try
        {
            if (usePost)
                PostMessage(target.GetAsIntPtr(), WM_KEYDOWN, Key, 0);
            else SendMessage(target.GetAsIntPtr(), WM_KEYDOWN, Key, 0);
        }
        catch (Exception)
        {
            Eloi.E_CodeTag.DirtyCode.DirtyCatch();
        }
    }

    private static void SendKeyUp(int Key, in IntPtrWrapGet target, in bool usePost)
    {
        try
        {
            if (usePost)
                PostMessage(target.GetAsIntPtr(), WM_KEYUP, Key, 0);
            else SendMessage(target.GetAsIntPtr(), WM_KEYUP, Key, 0);
        }
        catch (Exception)
        {
            Eloi.E_CodeTag.DirtyCode.DirtyCatch();
        }
    }


    internal static void SendKeyDownToProcessChildren(User32PostMessageKeyEnum Key, IntPtrWrapGet processId, in bool usePost = true)
    {
        try
        {
            IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
            if (prs.Length <= 0) return;
            IntPtrWrapGet pr = prs[0];
            foreach (IntPtrWrapGet p in prs)
            {
                SendKeyDown(Key, in processId, in usePost);
            }
        }
        catch (Exception)
        {
            Eloi.E_CodeTag.DirtyCode.DirtyCatch();
        }
    }
    internal static void SendKeyUpToProcessChildren(User32PostMessageKeyEnum Key, IntPtrWrapGet processId, in bool usePost = true)
    {
        try
        {
            IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(processId);
            if (prs.Length <= 0) return;
            IntPtrWrapGet pr = prs[0];
            foreach (IntPtrWrapGet p in prs)
            {
                SendKeyUp(Key, p, in usePost);
            }
        }
        catch (Exception)
        {
            Eloi.E_CodeTag.DirtyCode.DirtyCatch();
        }
    }
    public static void SendKeyClick(User32PostMessageKeyEnum key, IntPtrWrapGet processId, bool usePost)
    {
        SendKeyDown(key, processId, usePost);
        SendKeyUp(key, processId, usePost);
    }
}
