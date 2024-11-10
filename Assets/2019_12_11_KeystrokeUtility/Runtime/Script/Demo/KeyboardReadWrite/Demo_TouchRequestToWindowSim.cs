using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class Demo_TouchRequestToWindowSim : MonoBehaviour
{

    public float m_delay;
    public KeyboardTouch[] m_touchs;
    public string m_text;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_delay);
        InputSimulator input = new InputSimulator();
        for (int i = 0; i < m_touchs.Length; i++)
        {
          VirtualKeyCode vk;
            bool isConvertable;
            KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(
                m_touchs[i], out vk, out isConvertable);
            if (isConvertable)
                input.Keyboard.KeyPress(vk);           
        }
        input.Keyboard.TextEntry(m_text);


    }

}
