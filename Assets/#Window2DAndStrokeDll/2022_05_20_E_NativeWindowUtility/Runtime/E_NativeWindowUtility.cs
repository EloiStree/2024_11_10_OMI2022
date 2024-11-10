using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class E_NativeWindowUtility 
{




    public static bool IsRunning(Process process)
    {
        return E_NativeWindowUtility_GetProcessInfo.IsRunningByExceptionCatch(process);
    }
    public static bool IsRunning(int processId)
    {
        return E_NativeWindowUtility_GetProcessInfo.IsRunningByExceptionCatch(processId);
    }

}
