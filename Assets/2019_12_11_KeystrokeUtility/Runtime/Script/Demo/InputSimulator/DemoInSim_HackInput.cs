using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput;
using WindowsInput.Native;

public class DemoInSim_HackInput : MonoBehaviour {


    public EnumDictionary<VirtualKeyCode> m_keyboardsState;
    public List<VirtualKeyCode> m_activeKeys;
    public List<VirtualKeyCode> m_availaibleKeys;
    public InputField m_input;
    public bool m_useHardware;

    void Awake()
    {
        m_keyboardsState = new EnumDictionary<VirtualKeyCode>();
        m_availaibleKeys = KeystrokeUtility.GetEnumList<VirtualKeyCode>();
        m_keyboardsState.onStateChange += DebugDisplay;
    }

    private void DebugDisplay(VirtualKeyCode element, bool isOn)
    {
        string debug = string.Format("{0:0000000}({1}): {2}\n", ((int)(Time.time * 1000f)), isOn, element);
        if (m_input)
            m_input.text = debug + m_input.text;
        Debug.Log(debug);

    }

    void OnValidate()
    {
        m_availaibleKeys = KeystrokeUtility.GetEnumList<VirtualKeyCode>();
    }

    // Update is called once per frame
    void Update () {

        InputSimulator input = new InputSimulator();
        m_activeKeys = m_keyboardsState.GetActiveElements() ;

        foreach (VirtualKeyCode vkc in m_availaibleKeys)
        {
            m_keyboardsState.SetState(vkc, input.InputDeviceState.IsHardwareKeyDown(vkc));
        }
        


    }
}
