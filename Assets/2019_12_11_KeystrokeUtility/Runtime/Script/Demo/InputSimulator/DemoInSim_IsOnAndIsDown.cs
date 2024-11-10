using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class DemoInSim_IsOnAndIsDown : MonoBehaviour
{

    public float m_delay = 5;
    public bool m_isCapsLockOn;
    public bool m_isCapsLockDown;
    public InputSimulator input = new InputSimulator();
    void Start()
    {
        input = new InputSimulator();
    }
    void Update()
    {
        m_isCapsLockOn =
        input.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.CAPITAL);
        m_isCapsLockDown =
        input.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.CAPITAL);


    }
}