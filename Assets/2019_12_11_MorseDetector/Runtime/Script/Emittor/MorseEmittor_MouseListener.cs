using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseEmittor_MouseListener : MorseEmittorAbstract
{
    [Range(0,5)]
    public int m_mouseButtonId=0;
    public override bool IsEmitting()
    {
        return Input.GetMouseButton(m_mouseButtonId);
    }


    public void OnValidate()
    {

        SetMouseListened(m_mouseButtonId);
    }

    public void SetMouseListened(int buttonId)
    {
        m_mouseButtonId = buttonId;
        m_emittorName = "Mouse Button " + m_mouseButtonId;
    }
}
