using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class WindowKeyboardKeyListener : KeyboardKeyListener
{
    public InputSimulator m_input;
   public InputSimulator WinInput { get {
            if (m_input == null)
                m_input = new InputSimulator();
            return m_input;
        }
    }

    public override bool IsCapsLockOn()
    {
      return   WinInput.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.CAPITAL);
    }

    public override bool IsNumLockOn()
    {
        return WinInput.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.NUMLOCK);
    }

    public override bool IsScrollLockOn()
    {
        return WinInput.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.SCROLL);
    }

    public override bool IsTouchActive(KeyboardTouch keyboardTouch)
    {
        bool isDown = false;
        VirtualKeyCode vkc;
        bool isConverted = false;
        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(keyboardTouch, out vkc, out isConverted);
    
        if (isConverted && WinInput.InputDeviceState.IsKeyDown(vkc))
        {
            isDown = true;
        }

        return isDown;
    }

   

    
}
