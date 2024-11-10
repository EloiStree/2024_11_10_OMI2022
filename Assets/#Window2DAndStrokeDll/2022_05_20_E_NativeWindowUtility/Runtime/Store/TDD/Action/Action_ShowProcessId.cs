using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_ShowProcessId : IUser32Action
{
    public IntPtrProcessId m_processId;

    public Action_ShowProcessId(IntPtrProcessId processId)
    {
        m_processId = processId;
    }
    public Action_ShowProcessId(IntPtrWrapGet processId)
    {
        m_processId = new IntPtrProcessId( processId.GetAsInt()) ;
    }
}
