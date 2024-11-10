using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AndStateToJOMI : MonoBehaviour
{
    public InputField m_condition;
    public InputField m_jomiShortcut;
    public ClassicBoolState m_andBoolState;

    public string GetBooleanCondition() { return m_condition.text; }
    public string GetShortcutActionToUse() { return m_jomiShortcut.text; }

    public void SetAsValide(bool isValide) {
        m_condition.textComponent.color = isValide ? Color.green : Color.red;
    }
    public ClassicBoolState GetUserWantedCondition() { return m_andBoolState; }


    private void Awake()
    {
        ChangeBoolCondition(m_condition.text);
        m_condition.onValueChanged.AddListener(ChangeBoolCondition);
    }

    private void ChangeBoolCondition(string arg0)
    {
        TextToBoolStateMachineParser.IsClassicParse(arg0, out m_andBoolState);
    }
}
