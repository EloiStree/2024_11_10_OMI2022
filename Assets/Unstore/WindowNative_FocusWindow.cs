using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowNative_FocusWindow : MonoBehaviour
{



    public  void  SetFocusToExternalApp(string strProcessName, float waitWindowToReactInSec = 0.10f)
    {
        StartCoroutine(Coroutine_SetFocusToExternalApp(strProcessName, waitWindowToReactInSec));
    }
    private  IEnumerator Coroutine_SetFocusToExternalApp(string strProcessName, float waitWindowToReactInSecond=0.10f)
    {
        Process[] arrProcesses = Process.GetProcessesByName(strProcessName);
        if (arrProcesses.Length > 0)
        {
           
            IntPtr ipHwnd = arrProcesses[0].MainWindowHandle;
            yield return new WaitForSeconds(waitWindowToReactInSecond);
            SetForegroundWindow(ipHwnd);
        }
    }

    //API-declaration
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public static string GetCurrentProcessorName() {
       return Process.GetCurrentProcess().ProcessName;
    }
}
