using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class WindowSimWriter : KeyboardWriteMono
{

    InputSimulator m_winKey;
    public void Awake()
    {
        m_winKey = new InputSimulator();
    }
    public override void RealPressDown(KeyboardTouch touch)
    {
        bool isConvertable;
        VirtualKeyCode vk;
        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(touch, out vk, out isConvertable);
        if(isConvertable)
            m_winKey.Keyboard.KeyDown(vk);
    }

    public override void RealPressUp(KeyboardTouch touch)
    {
        bool isConvertable;
        VirtualKeyCode vk;
        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(touch, out vk, out isConvertable);
        if (isConvertable)
            m_winKey.Keyboard.KeyUp(vk);

    }
    public  void VirtualKeyStroke(VirtualKeyCode key, PressType pressType)
    {
        if (pressType == PressType.Down || pressType == PressType.Both)
            m_winKey.Keyboard.KeyDown(key);
        if (pressType == PressType.Up || pressType == PressType.Both)
            m_winKey.Keyboard.KeyUp(key);

    }

    public override void Stroke(string text)
    {
        
            m_winKey.Keyboard.TextEntry(text);

    }

    public override void Stroke(char character)
    {
        m_winKey.Keyboard.TextEntry(character.ToString());
    }

    public override void Stroke(KeyboardTouchPressRequest key)
    {
        if (key.m_pression == PressType.Down || key.m_pression == PressType.Both)
            RealPressDown(key.m_touch);
        if (key.m_pression == PressType.Up || key.m_pression == PressType.Both)
            RealPressUp(key.m_touch);
    }

    public override void Stroke(KeyboardCharPressRequest character)
    {
        Stroke(character.m_character);
    }

    public override void Stroke(AsciiStrokeRequest asciiCode)
    {
        char c; bool ic;
        KeyBindingTable.ConvertASCIIToChar(asciiCode.m_code, out  c, out ic);
        Stroke(c);
    }

    public override void Stroke(UnicodeStrokeRequest unicode)
    {
        char c; bool ic;
        KeyBindingTable.ConvertUnicodeToChar(unicode.m_code, out c, out ic);
        Stroke(c);
    }
}
