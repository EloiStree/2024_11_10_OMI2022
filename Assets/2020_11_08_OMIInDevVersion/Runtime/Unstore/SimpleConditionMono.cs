using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleConditionMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public List<AndBooleanChangeToAction<string>> m_simpleCondition = new List<AndBooleanChangeToAction<string>>();
    public UnityStringEvent m_actionFound = new UnityStringEvent();
   
    internal void Clear()
    {
        m_simpleCondition.Clear();
    }

    public void Add(AndBooleanChangeToAction<string> simpleCondition) {
        if (simpleCondition == null)
            return;
        if(simpleCondition.m_looperTrue!=null)
            simpleCondition.m_looperTrue.SetTheAction(SendActionAsString);
        if (simpleCondition.m_looperFalse != null)
            simpleCondition.m_looperFalse.SetTheAction(SendActionAsString);
        m_simpleCondition.Add(simpleCondition);


    }

    
    void Update()
    {

        if (m_register == null) return;
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        if (reg == null) return;

        for (int i = 0; i < m_simpleCondition.Count; i++)
        {

            AndBooleanChangeToAction<string> b2j = m_simpleCondition[i];
            if (b2j.m_andBooleanState.IsBooleansRegistered(reg)) { 
                bool isvalide = b2j.m_andBooleanState.IsConditionValide(reg);
                bool hasChange;
                b2j.m_eventListener.SetValue(isvalide, out hasChange);
                if (hasChange)
                {
                    if (isvalide && b2j.m_listenToTrueChange)
                    {
                        SendActionAsString(b2j.m_informationToTrigger);
                    }

                    if (!isvalide && b2j.m_listenToFalseChange)
                    {
                        SendActionAsString(b2j.m_informationToTrigger);
                    }
                }
                if (b2j.m_looperTrue != null && b2j.m_useLoopTrue)
                    b2j.m_looperTrue.SetAsActive(isvalide);
                if (b2j.m_looperFalse != null && b2j.m_useLoopFalse)
                    b2j.m_looperFalse.SetAsActive(!isvalide);
                SendShortCutWhenLoopIsReady(b2j.m_looperFalse);
                SendShortCutWhenLoopIsReady(b2j.m_looperTrue);
            }
        }

    }

    private void SendActionAsString(string action)
    {
        m_actionFound.Invoke(action);
    }

    private void SendShortCutWhenLoopIsReady(ActionLooper<ActionAsString> loop)
    {
        if (loop == null) return;
        if (!loop.IsActive()) return;
        loop.RemoveTimeToCountdown(Time.deltaTime);
        if (loop.IsReadyToExecute())
        {
            loop.DoDefaultAction();
            loop.ResetTCountDown();
        }
    }
    private void SendActionAsString(ActionAsString parameter)
    {
        SendActionAsString(parameter.m_action);
    }

}
