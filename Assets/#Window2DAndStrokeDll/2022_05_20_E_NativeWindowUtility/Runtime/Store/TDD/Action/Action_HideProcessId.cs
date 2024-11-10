using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_HideProcessId : IUser32Action
{
    public IntPtrProcessId m_processId;

    public Action_HideProcessId(int processId) 
    {
        this.m_processId =new IntPtrProcessId(processId);
    }
    public Action_HideProcessId(IntPtrWrapGet intPtrWrapGet) 
    {
        this.m_processId = new IntPtrProcessId(intPtrWrapGet.GetAsInt()); 
    }
}

