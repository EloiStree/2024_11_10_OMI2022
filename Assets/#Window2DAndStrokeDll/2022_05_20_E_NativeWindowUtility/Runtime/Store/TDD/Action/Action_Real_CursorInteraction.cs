using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Real_CursorInteraction : IUser32Action
{
    public User32MouseClassicButton m_targetButton;
    public User32PressionType m_interaction;

}
[System.Serializable]
public struct Action_Real_CursorClick : IUser32Action
{
    public User32MouseClassicButton m_targetButton;

}
[System.Serializable]
public struct Action_Real_CursorHoldPression : IUser32Action
{
    public User32MouseClassicButton m_targetButton;
    public float m_secondToMaintain;

}