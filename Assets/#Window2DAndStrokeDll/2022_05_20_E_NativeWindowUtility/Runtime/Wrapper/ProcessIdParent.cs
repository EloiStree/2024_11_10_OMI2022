using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessIdParent : ProcessIdWrapper, IntPtrWrapGet 
{
    public ProcessIdParent(int id) : base(id)
    {
    }
    public ProcessIdParent():base()
    {
    }

    public void GetAsIntPtr(out IntPtr pointer)
    {
        pointer = (IntPtr)GetProcessId();
    }

    public IntPtr GetAsIntPtr()
    {
        return (IntPtr)GetProcessId();
    }

    public void GetAsInt(out int pointer)
    {
        pointer = GetProcessId();
    }

    public int GetAsInt()
    {
        return GetProcessId();
    }

    public virtual bool IsParent()
    {
        return true;
    }

    public virtual void IsParent(out bool isParent)
    {
        isParent =true;
    }
}
