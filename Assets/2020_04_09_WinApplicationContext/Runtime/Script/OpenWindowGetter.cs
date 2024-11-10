///SOURCE: https://www.tcx.be/blog/2006/list-open-windows/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Diagnostics;
using System;
using System.Runtime.InteropServices;
using System.Text;
using HWND = System.IntPtr;
using System.Linq;
public static class OpenWindowGetter
{

    public static IDictionary<HWND, string> GetOpenWindows()
    {
        HWND shellWindow = GetShellWindow();
        Dictionary<HWND, string> windows = new Dictionary<HWND, string>();
      

        EnumWindows(delegate (HWND hWnd, int lParam)
        {
            if (hWnd == shellWindow) return true;
            if (!IsWindowVisible(hWnd)) return true;

            int length = GetWindowTextLength(hWnd);
            if (length == 0) return true;

            StringBuilder builder = new StringBuilder(length);
            GetWindowText(hWnd, builder, length + 1);

            windows[hWnd] = builder.ToString();
            return true;

        }, 0);

        return windows;
    }

    private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

    [DllImport("USER32.DLL")]
    private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    [DllImport("USER32.DLL")]
    private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("USER32.DLL")]
    private static extern int GetWindowTextLength(HWND hWnd);

    [DllImport("USER32.DLL")]
    private static extern bool IsWindowVisible(HWND hWnd);

    [DllImport("USER32.DLL")]
    private static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    public static void GetFocusWindow(out string nameOFocusWindow, out List<string> windowsAvailable) {
        IDictionary<HWND, string> m_listOfWindowLinkedToPtr = OpenWindowGetter.GetOpenWindows();
        IntPtr m_focusWindowPtr  = OpenWindowGetter.GetForegroundWindow();
        if (m_listOfWindowLinkedToPtr.ContainsKey(m_focusWindowPtr))
            nameOFocusWindow = m_listOfWindowLinkedToPtr[m_focusWindowPtr];
        else nameOFocusWindow = "";
        windowsAvailable = m_listOfWindowLinkedToPtr.Values.ToList();
    }
}
