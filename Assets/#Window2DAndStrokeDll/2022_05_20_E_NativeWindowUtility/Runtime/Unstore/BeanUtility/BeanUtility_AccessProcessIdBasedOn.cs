using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class BeanUtility_AccessProcessIdBasedOn 
{

    public static void GetAllProcessesName(out List<string> allProcessName) {
        allProcessName = Process.GetProcesses().Select(k=>k.ProcessName).ToList();
    }


    public static void GetScreenPositionFromWindow(in string whatProcessName, in int processIndex,out bool found, out IntPtr pointer)
    {
        Process[] processes = Process.GetProcessesByName(whatProcessName);
        found = false;
        pointer = new IntPtr();
        if (processes.Length > processIndex) {
            found = true;
            Process lol = processes[0];
            pointer = lol.MainWindowHandle;
        }
    }
}
