using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CallFunctionAction : BooleanStateAction
{
    [SerializeField] string m_functionName;
    [SerializeField] string[] m_arguments;

    public string GetFunctionName()
    {
        return m_functionName;
    }

    public CallFunctionAction(string functionName, string[] arguments)
    {
        m_functionName = functionName;
        m_arguments = arguments;
    }

    public bool HasArguments()
    {
        return m_arguments.Length>0;
    }

    public string [] GetArguments()
    {
        return m_arguments;
    }

    public override string ToString()
    {
        return "[CALL|" + m_functionName + "|" + string.Join(" ", m_arguments) + "]";
    }
}
