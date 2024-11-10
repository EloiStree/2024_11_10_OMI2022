using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

public class Demo_DisplayEnumAsText : MonoBehaviour {
    public InputField m_field;
    // Use this for initialization
    public bool m_lineCount;

	public void DisplayCharInfo () {
        m_field.text = "";
        int i = 0;
        foreach (char c in KeyBindingTable.GetAllASCII())
        {
            m_field.text += "\n" + LineCount(ref i) + c + "-" + ((int)c);
        }

    }

    public void DisplayTouchInfo()
    {
        m_field.text = "";
        int i = 0;
        foreach (KeyboardTouch c in KeyBindingTable.GetAllTouches())
        {
            m_field.text += "\n" + LineCount(ref i) + c + "-" + ((int)c);
        }

    }
    public void DisplayKeyCodeInfo()
    {
        m_field.text = "";
        int i = 0;
        foreach (KeyCode c in KeystrokeUtility.GetUnityKeyCodes())
        {
            m_field.text += "\n" + LineCount(ref i) + c + "-" + ((int)c);
        }

    }
    public void DisplayVirtualKeyCodeInfo()
    {
        m_field.text = "";
        int i = 0;
        foreach (VirtualKeyCode c in KeystrokeUtility.GetVirtualKeyCode())
        {
            m_field.text += "\n" + LineCount(ref i) + c + "-" + ((int)c);
        }
    }
    private string LineCount(ref int i)
    {
        return (m_lineCount ? "" + i++ + ": " : "");
    }

}
