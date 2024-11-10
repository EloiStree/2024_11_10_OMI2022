using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmitTextAction : BooleanStateAction
{
    [SerializeField] string m_textToEmit;

    public EmitTextAction(string textToEmit)
    {
        m_textToEmit = textToEmit;
    }
    public override string ToString()
    {
        return "[TEXT|" + m_textToEmit + "]";
    }

    public string GetText()
    {
        return m_textToEmit;
    }
}
