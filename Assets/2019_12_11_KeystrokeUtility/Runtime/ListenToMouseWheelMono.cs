using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ListenToMouseWheelMono : MonoBehaviour
{
    public MouseWheelUnityEvent m_wheelEvent = new MouseWheelUnityEvent();
    public MouseWheelDelegateEvent m_wheelDelegateEvent;

    void Awake()
    {
        InterceptMouse.StartApp();
    }
    private void OnDestroy()
    {

        InterceptMouse.StopApp();
    }

    public List<MouseWheelEvent> m_history = new List<MouseWheelEvent>();
    public Queue<MouseWheelEvent> m_pushOnUnityThread = new Queue<MouseWheelEvent>();
    public Queue<MouseWheelEvent> m_pushOnDelegateThread = new Queue<MouseWheelEvent>();

    public void RecoverWheelEvent()
    {
        while (InterceptMouse.m_mouseWheelHistory.Count > 0)
        {
            MouseWheelEvent found = InterceptMouse.m_mouseWheelHistory.Dequeue();
            m_pushOnUnityThread.Enqueue(found);
            m_pushOnDelegateThread.Enqueue(found);
        }
    }

    public void FlushWheelEventDirectly()
    {
        while (m_pushOnDelegateThread.Count > 0)
        {
            MouseWheelEvent found = m_pushOnDelegateThread.Dequeue();
            if (m_wheelDelegateEvent != null)
                m_wheelDelegateEvent(found);
        }
    }
    public void FlushWheelEventOnUnityThread()
    {
        FlushWheelEventDirectly();
        while (m_pushOnUnityThread.Count > 0)
        {
            MouseWheelEvent found = m_pushOnUnityThread.Dequeue();
            m_wheelEvent.Invoke(found);
            m_history.Insert(0, found);
        }
        while (m_history.Count > 20)
            m_history.RemoveAt(m_history.Count - 1);
    }

    public void FixedUpdate()
    {
        InterceptMouse.FlushToProcess();
        RecoverWheelEvent();
        FlushWheelEventOnUnityThread();
        mouseTick = InterceptMouse.m_mouseTickEvent;
    }
    public ulong mouseTick;
}
public delegate void MouseWheelDelegateEvent(MouseWheelEvent wheelEvent);
[System.Serializable]
public class MouseWheelUnityEvent : UnityEvent<MouseWheelEvent> { }

[System.Serializable]
public class MouseWheelEvent
{
    public enum Direction { Left, Right, Up, Down }
    public Direction m_direction;

    public MouseWheelEvent(Direction direction)
    {
        m_direction = direction;
    }
}

public class InterceptMouse
{

    public static Queue<MouseWheelEvent> m_mouseWheelHistory = new Queue<MouseWheelEvent>();
    private static LowLevelMouseProc _proc = HookCallback;

    private static IntPtr _hookID = IntPtr.Zero;
    public static Thread m_listeningThread;
    public static bool m_wantThreadAlive;
    public static void StartApp()
    {

        _hookID = SetHook(_proc);
       

    }

    public static void StopApp()
    {
        UnhookWindowsHookEx(_hookID);
    }


    private static IntPtr SetHook(LowLevelMouseProc proc)
    {
        
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);

            }
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    public static ulong m_mouseTickEvent;
    //public static Queue<string> m_history = new Queue<string>();


    [System.Serializable]
    public struct ToProcess
    {
        public IntPtr wParam;
        public IntPtr lParam;
        //public long wParam;
        //public long lParam;

        public ToProcess(IntPtr wParam, IntPtr lParam)
        {
            this.wParam = wParam;
            this.lParam = lParam;
            //this.wParam =(long) wParam;
            //this.lParam = (long) lParam;
        }
    }
    public static Queue<ToProcess> toProcess = new Queue<ToProcess>();

    public static void FlushToProcess()
    {
        
        while (toProcess.Count > 0)
        {
            ToProcess p = toProcess.Dequeue();
            m_mouseTickEvent++;
            //if (MouseMessages.WM_MOUSEWHEEL == (MouseMessages)p.wParam)
            if (0x020A == (int) p.wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(p.lParam, typeof(MSLLHOOKSTRUCT));
                m_mouseWheelHistory.Enqueue(new MouseWheelEvent(hookStruct.mouseData == 4287102976 ? MouseWheelEvent.Direction.Down : MouseWheelEvent.Direction.Up));

            }
            else if (0x020E ==(int) p.wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(p.lParam, typeof(MSLLHOOKSTRUCT));
                m_mouseWheelHistory.Enqueue(new MouseWheelEvent(hookStruct.mouseData == 4287102976 ? MouseWheelEvent.Direction.Left : MouseWheelEvent.Direction.Right));

            }

        }
    }

    public static int wp;
    private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
    {
        m_mouseTickEvent++;
        if (nCode < 0)
        {
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
        wp = (int)wParam;
        //if (MouseMessages.WM_MOUSEWHEEL == (MouseMessages)wParam)
        if(wp== 0x020A)
        {
            toProcess.Enqueue(new ToProcess(wParam, lParam));
        }
        else if (0x020E == wp)
        {
            toProcess.Enqueue(new ToProcess(wParam, lParam));
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }


    private const int WH_MOUSE_LL = 14;
    private const int WH_MOUSE = 7;


    private enum MouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_HMOUSEWHEEL = 0x020E

    }


    [StructLayout(LayoutKind.Sequential)]

    private struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]

    private struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }


    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    private static extern IntPtr SetWindowsHookEx(int idHook,

        LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);


    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    [return: MarshalAs(UnmanagedType.Bool)]

    private static extern bool UnhookWindowsHookEx(IntPtr hhk);


    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

        IntPtr wParam, IntPtr lParam);


    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    private static extern IntPtr GetModuleHandle(string lpModuleName);

}