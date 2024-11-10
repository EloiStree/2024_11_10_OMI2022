using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class E_NativeWindowUtility_GetProcessInfo
{

    public static bool IsRunningByExceptionCatch(Process process)
    {
        return IsRunningByExceptionCatch(process.Id);
    }
    public static bool IsRunningByExceptionCatch(int  processId)
    {
        try { Process.GetProcessById(processId); }
        catch (InvalidOperationException) { return false; }
        catch (ArgumentException) { return false; }
        return true;
    }

    public static bool IsRunningByStateAccess(Process process)
    {
        process.Refresh();
        return !process.HasExited;
    }
}
