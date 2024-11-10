using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class WindowKeyToBooleanStateMachine : MonoBehaviour
{
    public TimeThreadMono m_timer;
    public List<WindowKeyToListenTo> m_registered= new List<WindowKeyToListenTo>();

   

    public InputSimulator m_winKey;
    public BooleanStateRegisterMono m_register;

    void Start()
    {
        TimeThreadMono.LoopPing loop = new TimeThreadMono.LoopPing(40, PingThreadType.InTimeThread);
        loop.SetCallBack(CheckStateOfKey);
        m_timer.SubscribeLoop(loop);
        m_winKey = new InputSimulator();
    }
    public void Clear()
    {
        m_registered.Clear();
    }
    private void CheckStateOfKey()
    {
            bool hasChange;
        for (int i = 0; i < m_registered.Count; i++)
        {
            m_registered[i].m_stateCheck.SetValue(m_winKey.InputDeviceState.IsHardwareKeyDown(m_registered[i].m_virtualKey), out hasChange);
            if (hasChange) {
                m_register.m_register.Set(m_registered[i].m_wantedBoolName, m_registered[i].m_stateCheck.GetValue());
            }

        }
    }
    public void AddKey(VirtualKeyCode key, string booleanName)
    {
        m_registered.Add(new WindowKeyToListenTo(booleanName, key));
    }

    public void AddKey(WindowKeyToListenTo key) {
        m_registered.Add(key);
    }
}
[System.Serializable]
public class WindowKeyToListenTo
{

    public string m_wantedBoolName;
    public VirtualKeyCode m_virtualKey;
    public BooleanSwitchListener m_stateCheck = new BooleanSwitchListener();

    public WindowKeyToListenTo(string wantedBoolName, VirtualKeyCode virtualKey)
    {
        m_wantedBoolName = wantedBoolName;
        m_virtualKey = virtualKey;
    }
}