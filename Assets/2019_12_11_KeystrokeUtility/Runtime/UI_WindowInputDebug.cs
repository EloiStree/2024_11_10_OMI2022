using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowInputDebug : MonoBehaviour
{
    public ThreadWindowSimListener m_windowInfo;
    public InputField m_keyboardTouch;
    public InputField m_windowTouch;
    void Start()
    {
        InvokeRepeating("Refresh",0, 0.5f);
    }

    void Refresh()
    {
        try
        {
            if (m_keyboardTouch && m_windowInfo)
                m_keyboardTouch.text = string.Join(" ", m_windowInfo.GetTouchActive());
            if (m_windowTouch && m_windowInfo)
                m_windowTouch.text = string.Join(" ", m_windowInfo.GetWindowKey());

        }
        catch (Exception e) {
            NotNowException.Ping(e);
        }

    }
}

public class NotNowException : Exception {



    public static void Ping(Exception e)
    {

        Debug.LogWarning("Correct later Exception:" + e.Message);
    }
}
