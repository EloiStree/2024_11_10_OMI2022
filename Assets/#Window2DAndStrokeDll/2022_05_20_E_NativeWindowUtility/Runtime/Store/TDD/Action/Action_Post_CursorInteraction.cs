using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Post_CursorInteraction : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32MouseClassicButton m_targetButton;
    public User32PressionType m_interactionType;
}
[System.Serializable]
public struct Action_Post_CursorClick : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32MouseClassicButton m_targetButton;
}
[System.Serializable]
public struct Action_Post_CursorClickWithDelay : IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32MouseClassicButton m_targetButton;
    public float m_secondToMaintain;

}