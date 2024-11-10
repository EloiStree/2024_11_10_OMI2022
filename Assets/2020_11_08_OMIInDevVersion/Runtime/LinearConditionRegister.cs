using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearConditionRegister : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public List<LinearConditionObserver> m_observedCondition = new List<LinearConditionObserver>();
    public UnityStringEvent m_commandFound;
    void Update()
    {
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        if (reg == null)
            return;

        for (int i = 0; i < m_observedCondition.Count; i++)
        {
            LinearConditionObserver condition = m_observedCondition[i];
            ClassicBoolState currentState = condition.GetCurrentCondition();
            if (condition.ReadyToBeCheck() && (currentState!=null && currentState.IsBooleansRegistered(reg) &&  currentState.IsConditionValide(reg) )) {
                bool hasFinished;
                if(condition.IsFirstCondition())
                    condition.RefreshGlobalExitTime();
                condition.RefreshStepExitTime();
                condition.GoNextCondition(out hasFinished);
                condition.AddBreakTime();
                if (hasFinished)
                {
                    if (condition.m_observed.m_triggerOnFound)
                       Trigger(condition);
                }
            }
            if (condition.ExitTimeIsReach()) {
                if(condition.m_observed.m_triggerOnExit && condition.IsAllConditionTriggered())
                    Trigger(condition);
                condition.ResetToZero();
            }

            if (!condition.IsFirstCondition())
                condition.RemoveExitTime(Time.deltaTime);

            condition.RemoveBreakTime(Time.deltaTime);
            condition.RemoveCooldownTime(Time.deltaTime);

        }
        
    }

    private void Trigger(LinearConditionObserver condition)
    {

        m_commandFound.Invoke(condition.GetStoreAction());
        condition.ResetToZero();
    }

    public void AddLinearCondition(LinearCondition condition) {

        m_observedCondition.Add(new LinearConditionObserver(condition));
        
    }

    public void Clear()
    {
        m_observedCondition.Clear();
    }
}

[System.Serializable]
public class LinearConditionObserver {

    public LinearCondition m_observed;
    public int m_index;
    public float m_currentBreakTime;
    public float m_currentStepExitTime;
    public float m_currentGlobalExitTime;
    public float m_currentCooldownTime;

    public LinearConditionObserver(LinearCondition condition)
    {
        this.m_observed = condition;
        ResetToCountDown();
    }

    public bool IsFirstCondition() { return m_index == 0; }
    public bool IsLastCondition() { return m_index >= GetConditionCount() - 1; }
    public bool IsAllConditionTriggered() { return m_index >= GetConditionCount() ; }
    public int GetConditionCount() { return m_observed.m_conditions.Count; }
    public bool IsObserved() {
        return m_index > 0;
    }
    public void ResetToCountDown()
    {
        m_currentBreakTime = m_observed.m_breakTimeInMs/1000f;
        m_currentGlobalExitTime = m_observed.m_globalExitTimeInMs / 1000f;
        m_currentStepExitTime = m_observed.m_stepExitTimeInMs / 1000f;
        m_currentCooldownTime = m_observed.m_cooldownInMs / 1000f;

    }
    public ClassicBoolState GetCurrentCondition() {
        return m_observed.GetCondition(m_index);
    }

    public void GoNextCondition(out bool hasFinished)
    {
        m_index+=1;
        hasFinished = IsAllConditionTriggered();
    }

    public bool ExitTimeIsReach()
    {
        return m_currentGlobalExitTime <= 0 || m_currentStepExitTime<=0;
    }

    public void ResetToZero()
    {
        m_index = 0;
        ResetToCountDown();
    }

    public bool ReadyToBeCheck()
    {
        return m_currentBreakTime <= 0 && m_currentCooldownTime<=0;
    }

    public void RemoveBreakTime(float deltaTime)
    {
        if (m_currentBreakTime > 0)
            m_currentBreakTime -= deltaTime;
    }
    public void RemoveExitTime(float deltaTime)
    {
        if (m_currentGlobalExitTime > 0)
            m_currentGlobalExitTime -= deltaTime;
        if (m_currentStepExitTime > 0)
            m_currentStepExitTime -= deltaTime;
    }
    public bool HasCoolDownTime() {
        return m_currentCooldownTime>0f;
    }
    public void RemoveCooldownTime(float deltaTime)
    {
        if (m_currentCooldownTime > 0)
            m_currentCooldownTime -= deltaTime;
    }

    public string GetStoreAction()
    {
        return m_observed.m_actionAsText;
    }

    public void RefreshGlobalExitTime()
    {
        m_currentGlobalExitTime = m_observed.m_globalExitTimeInMs/1000f;
    }

    public void RefreshStepExitTime()
    {
        m_currentStepExitTime = m_observed.m_stepExitTimeInMs / 1000f;
    }

    public void AddBreakTime()
    {
        m_currentBreakTime = m_observed.m_breakTimeInMs / 1000f;
    }
}

[System.Serializable]
public class LinearCondition {

    public List<ClassicBoolState> m_conditions= new List<ClassicBoolState>();
    public uint m_globalExitTimeInMs =2000;
    public uint m_stepExitTimeInMs = 800;
    public uint m_breakTimeInMs =0;
    public uint m_cooldownInMs=1000;
    public bool m_useSingleInstance;//Code Later
    public string m_actionAsText;
    public bool m_triggerOnFound;
    public bool m_triggerOnExit;

    public ClassicBoolState GetEntryPointCondition() { return m_conditions[0]; }
                                                                        
    public ClassicBoolState GetCondition(int index)
    {
        if (index < 0 || index >= m_conditions.Count)
            return null;
        return m_conditions[index];
    }
    public bool IsFirstCondition(int index) { return index == 0; }
    public bool IsLastCondition(int index) { return index >= GetConditionCount() - 1; }
    public bool IsAllConditionTriggered(int index) { return index >= GetConditionCount(); }
    public int GetConditionCount() { return m_conditions.Count; }

    public void Add(ClassicBoolState condition)
    {
        m_conditions.Add(condition);
    }
}
