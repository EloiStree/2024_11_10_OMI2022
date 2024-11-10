using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseEmittor_KeypressListener : MorseEmittorAbstract
{
    public KeyCode m_keypressed = KeyCode.Space;
    public override bool IsEmitting()
    {
        return Input.GetKey(m_keypressed);
    }

    public void OnValidate()
    {
        SetKeyboardListened(m_keypressed);
    }

    public void SetKeyboardListened(KeyCode keyCodeId)
    {
        m_keypressed = keyCodeId;
        m_emittorName = "Key Pressed " + m_keypressed.ToString();

    }
}
