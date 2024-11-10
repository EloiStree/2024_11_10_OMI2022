using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AndStatesToJomi : MonoBehaviour
{
    public BooleanStateRegisterMono m_booleanRegister;
    public UI_ServerDropdownJavaOMI m_jomiTargets;
    public UI_AndStateToJOMI [] m_statesConfig;
    public bool[] m_previousState;
    private void Start()
    {
        m_previousState = new bool[m_statesConfig.Length];
    }
    void Update()
    {
        float time = (float)(DateTime.Now - m_previous).TotalSeconds;


        BooleanStateRegister register = m_booleanRegister.m_register;
        for (int i = 0; i < m_statesConfig.Length; i++)
        {
            bool isValide = m_statesConfig[i].GetUserWantedCondition().IsConditionValide(register);
            if (isValide && m_previousState[i]!= isValide)
            {
                string shortcut= m_statesConfig[i].GetShortcutActionToUse();
            //    Debug.Log("Send: " + m_statesConfig[i].GetShortcutActionToUse());
                foreach (var item in m_jomiTargets.GetJavaOMISelected())
                {
                  item.SendShortcutCommands(shortcut);
                }
            }
            m_previousState[i] = isValide;
            m_statesConfig[i].SetAsValide(isValide);

        }


        m_previous = DateTime.Now;
    }
    public DateTime m_previous;
}
