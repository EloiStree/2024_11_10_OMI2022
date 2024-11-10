using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTest_ConditionParse : MonoBehaviour
{
    public string m_text;
    public ClassicBoolState m_condition;

    private void OnValidate()
    {
        TextToBoolStateMachineParser.IsClassicParse(m_text, out m_condition);
    }
}
