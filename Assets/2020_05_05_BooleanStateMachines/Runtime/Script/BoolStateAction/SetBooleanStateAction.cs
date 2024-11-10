using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetBooleanStateAction : BooleanStateAction
{
    [SerializeField] string m_booleanStateNameToAffect;
    [SerializeField] bool m_valueToAffect;

    public SetBooleanStateAction(string registerName, string booleanStateName, bool value)
    {
        m_booleanStateNameToAffect = booleanStateName;
        m_valueToAffect = value;
    }

    public string GetBooleanVariableName()
    {
        return m_booleanStateNameToAffect;
    }

    public bool GetBooleanValue()
    {
        return m_valueToAffect;
    }

    public override string ToString()
    {
        return "[SET|"+m_booleanStateNameToAffect+"|"+m_valueToAffect+"]";
    }
}
