using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConditionCheckerInputField : MonoBehaviour
{
    public BooleanStateRegisterMono m_mono;
    public InputField m_inputField;
    public Image m_imageDebug;
    public ClassicBoolState m_condition;
    public Color m_valide = Color.green;
    public Color m_wrong = Color.red;
    public Color m_notExisting = Color.cyan;
    public float m_conditionRefreshTime = 0.1f;
    void Start()
    {
        m_inputField.onValueChanged.AddListener(Refresh);
        InvokeRepeating("Refresh", 0, m_conditionRefreshTime);
        Refresh(m_inputField.text);
    }


    private void Refresh(string condition)
    {
        TextToBoolStateMachineParser.IsClassicParse(condition, out m_condition);

        Refresh();
    }

    private void Refresh()
    {
        if (!m_condition.IsBooleansRegistered(m_mono.m_register))
        {
            m_imageDebug.color = m_notExisting;
        }
        else
        {
            m_imageDebug.color = m_condition.IsConditionValide(m_mono.m_register) ? m_valide : m_wrong;

        }
    }
}
