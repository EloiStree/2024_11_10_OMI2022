using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToMacroExecution : MonoBehaviour
{

    [TextArea(1,10)]
    public string m_macro;
    public CmdConvertAbstract [] m_converters;

    [Header("Debug")]
    public string m_name;
    public string m_description;
    public string m_currentCommand;
    public string m_lastError;
    public int m_cursor;

    IEnumerator Start()
    {
        string[] lines = m_macro.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            m_cursor = i;
            if (lines[i].StartsWith("##") && lines[i].Length > 2)
            {
                m_description = lines[i].Substring(2);
            }
            else if (lines[i].StartsWith("#") && lines[i].Length > 1)
            {
                m_name = lines[i].Substring(1);
            }
            else {
                m_currentCommand = lines[i];
                FinishChecker finish = new FinishChecker();
                SuccessChecker success = new SuccessChecker();
                for (int j = 0; j < m_converters.Length; j++)
                {
                    if (m_converters[j].CanTakeResponsability(m_currentCommand)) { 
                        m_converters[j].DoTheThing(m_currentCommand, success, finish);
                        yield return new WaitUntil(finish.IsFinished);
                        m_lastError = success.m_errorMessage;
                    }
                }
            }
        }
        yield break;
    }

   
}
