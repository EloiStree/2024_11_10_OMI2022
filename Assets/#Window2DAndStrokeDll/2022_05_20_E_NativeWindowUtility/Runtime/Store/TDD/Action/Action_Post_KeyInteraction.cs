using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Post_KeyInteraction :  IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32PostMessageKeyEnum m_targetKey;
    public User32PressionType m_pressionType;

    public Action_Post_KeyInteraction(
        int processId,
        User32PostMessageKeyEnum targetKey, 
        User32PressionType pressionType):this(new IntPtrProcessId(processId), targetKey, pressionType)
    {    }
    public Action_Post_KeyInteraction(IntPtrProcessId processId, 
        User32PostMessageKeyEnum targetKey, 
        User32PressionType pressionType)
    {
        m_processId = processId;
        m_targetKey = targetKey;
        m_pressionType = pressionType;
    }

}
