using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_KeyInteraction : IUser32Action
{

    public User32KeyboardStrokeCodeEnum m_targetKey;
    public User32PressionType m_pressionType;

    public Action_Real_KeyInteraction(
        User32KeyboardStrokeCodeEnum targetKey, 
        User32PressionType pressionType)
    {
        m_targetKey = targetKey;
        m_pressionType = pressionType;
    }
}
