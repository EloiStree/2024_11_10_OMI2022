using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedConditionMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public List<TimedConditionBeforeAfterState> m_timeBeforeAfterRegister = new List<TimedConditionBeforeAfterState>();
    public List<TimedConditionBetweenState> m_timeBetweenRegister= new List<TimedConditionBetweenState>();
    public UnityStringEvent m_commandFound;

    public void Reset()
    {
        ClassicBoolState condition;
        TextToBoolStateMachineParser.IsClassicParse("Jump", out condition);
        m_timeBetweenRegister.Add(new TimedConditionBetweenState(new TimedConditionBetween()
        {
            m_actionToTrigger = "A↓ A↑",
            m_condition = condition,
            m_timeInMsMin = 1000,
            m_timeInMsMax = 3000,
            m_timeConditionType = TimedConditionBetween.TimeType.Between,
            m_triggerOnInRange = true
        }));
        TextToBoolStateMachineParser.IsClassicParse("Shift", out condition);
        m_timeBeforeAfterRegister.Add(new TimedConditionBeforeAfterState(new TimedConditionBeforeAfter()
        {
            m_actionToTrigger = "B↓ B↑",
            m_condition = condition,
            m_timeInMs = 4000,
            m_timeConditionType = TimedConditionBeforeAfter.TimeType.After,
            m_triggerOnInRange = true
        }));

        m_register = GameObject.FindObjectOfType<BooleanStateRegisterMono>();

    }
    public void Update()
    {
        BooleanStateRegister reg=null;
        m_register.GetRegister(ref reg);
        float deltaTime = Time.deltaTime;
        bool hasChangeChange;
        bool hasConditionChange;
        bool isTrueInRange;
        bool stateCondition;
        for (int i = 0; i < m_timeBeforeAfterRegister.Count; i++)
        {
            TimedConditionBeforeAfterState tmp= m_timeBeforeAfterRegister[i];
            if (tmp.m_information.m_condition.IsBooleansRegistered(reg)) { 
                bool wasTrue = tmp.IsTrue();
                stateCondition = tmp.m_information.m_condition.IsConditionValide(reg);
                tmp.m_conditionChange.SetValue(stateCondition, out hasConditionChange); 
                tmp.SetCurrentCondition(stateCondition);
                tmp.AddDeltaTimeIfNeed(deltaTime);
                isTrueInRange = tmp.IsTrue();
                tmp.m_booleanChange.SetValue(isTrueInRange, out hasChangeChange);

                if (hasChangeChange)
                {
                    if (isTrueInRange && tmp.m_information.m_triggerOnInRange) {
                        SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                    }
                    if (!isTrueInRange && tmp.m_information.m_triggerOnOutRange)
                    {
                        SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                    }
                }
                if (hasConditionChange)
                {
                    if (tmp.m_information.m_triggerOnReleaseInRange && !stateCondition && wasTrue )
                    {
                        SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                    }
                }
            }
        }
        for (int i = 0; i < m_timeBetweenRegister.Count; i++)
        {

            TimedConditionBetweenState tmp = m_timeBetweenRegister[i];
            if (tmp.m_information.m_condition.IsBooleansRegistered(reg)) { 
                bool wasTrue = tmp.IsTrue();
                stateCondition = tmp.m_information.m_condition.IsConditionValide(reg);
                tmp.m_conditionChange.SetValue(stateCondition, out hasConditionChange);
                tmp.SetCurrentCondition(stateCondition);
                tmp.AddDeltaTimeIfNeed(deltaTime);
                isTrueInRange = tmp.IsTrue();
                tmp.m_booleanChange.SetValue(isTrueInRange, out hasChangeChange);
                if (hasChangeChange)  
                {
               
                        if (isTrueInRange && tmp.m_information.m_triggerOnInRange)
                        {
                            SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                        }
                        if (!isTrueInRange && tmp.m_information.m_triggerOnOutRange)
                        {
                            SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                        }

                }
                if (hasConditionChange)
                {
                    if (tmp.m_information.m_triggerOnReleaseInRange && !stateCondition && wasTrue)
                    {
                        SendCommandShortcut(tmp.m_information.m_actionToTrigger);
                    }
                }
            }
        }
    }

    public void SendCommandShortcut(string command) {
        m_commandFound.Invoke(command);
    }

    public void Clear()
    {
        m_timeBeforeAfterRegister.Clear();
        m_timeBetweenRegister.Clear();

    }

    public void Add(TimedConditionBeforeAfter timedCondition)
    {
        if (timedCondition == null)
            return ;
        if (timedCondition.m_condition == null)
            return ;

        m_timeBeforeAfterRegister.Add(new TimedConditionBeforeAfterState( timedCondition));
    }

    public void Add(TimedConditionBetween timedCondition)
    {
        if (timedCondition == null)
            return;
        if (timedCondition.m_condition == null)
            return;
        m_timeBetweenRegister.Add(new TimedConditionBetweenState( timedCondition));
    }
}


[System.Serializable]
public class TimedConditionBeforeAfterState : IBooleanable
{

    public TimedConditionBeforeAfter m_information= new TimedConditionBeforeAfter();
    public float m_timeBeeingTrue;
    public bool m_isConditionTrue;
    public BooleanSwitchListener m_booleanChange = new BooleanSwitchListener();
    public BooleanSwitchListener m_conditionChange = new BooleanSwitchListener();

    public TimedConditionBeforeAfterState(TimedConditionBeforeAfter timedConditionBeforeAfter)
    {
        this.m_information = timedConditionBeforeAfter;
    }

    public void AddDeltaTimeIfNeed(float deltaTime)
    {
        if (m_isConditionTrue)
            m_timeBeeingTrue += deltaTime;
        else m_timeBeeingTrue = 0;

    }
    public void SetCurrentCondition(bool conditionState) {
        m_isConditionTrue = conditionState;
    }

    public bool IsTrue()
    {
        if (m_information.m_timeConditionType == TimedConditionBeforeAfter.TimeType.After)
        {
            return m_timeBeeingTrue > m_information.m_timeInMs / 1000f;
        }
        else
        {
            return m_timeBeeingTrue < m_information.m_timeInMs / 1000f;
        }
    }

    public bool IsFalse()
    {
        return !IsTrue();
    }

}

[System.Serializable]
public class TimedConditionBeforeAfter
{

    public ClassicBoolState m_condition;
    public string m_actionToTrigger;
    public uint m_timeInMs;
    public enum TimeType { Before, After }
    public TimeType m_timeConditionType = TimeType.Before;
    public bool m_triggerOnInRange;
    public bool m_triggerOnOutRange;
    public bool m_triggerOnReleaseInRange;
}

[System.Serializable]
public class TimedConditionBetweenState: IBooleanable
{

    public TimedConditionBetween m_information= new TimedConditionBetween();
    public float m_timeBeeingTrue;
    public bool m_isConditionTrue;
    public BooleanSwitchListener m_booleanChange = new BooleanSwitchListener();
    public BooleanSwitchListener m_conditionChange = new BooleanSwitchListener();

    public TimedConditionBetweenState(TimedConditionBetween timedConditionBetween)
    {
        this.m_information = timedConditionBetween;
    }

    public void AddDeltaTimeIfNeed(float deltaTime)
    {
        if (m_isConditionTrue)
            m_timeBeeingTrue += deltaTime;
        else m_timeBeeingTrue = 0;

    }
    public void SetCurrentCondition(bool conditionState)
    {
        m_isConditionTrue = conditionState;
    }

    public bool IsTrue()
    {
      
        if (m_information.m_timeConditionType == TimedConditionBetween.TimeType.Before )
        {
            return m_timeBeeingTrue < m_information.m_timeInMsMin / 1000f;
        }
       if (m_information.m_timeConditionType == TimedConditionBetween.TimeType.After )
        {
            return m_timeBeeingTrue > m_information.m_timeInMsMax / 1000f;
        }

        if (m_information.m_timeConditionType == TimedConditionBetween.TimeType.Between)
        {
            return m_timeBeeingTrue > m_information. m_timeInMsMin / 1000f 
                && m_timeBeeingTrue < m_information.m_timeInMsMax / 1000f; 
        }
        if (m_information.m_timeConditionType == TimedConditionBetween.TimeType.NotBetween)
        {
            return m_timeBeeingTrue < m_information.m_timeInMsMin / 1000f
                || m_timeBeeingTrue > m_information.m_timeInMsMax / 1000f;
        }
        throw new Exception("Should not be reach");

    }

    public bool IsFalse()
    {
        return !IsTrue();
    }
}
[System.Serializable]
public class TimedConditionBetween
{

    public ClassicBoolState m_condition= new ClassicBoolState(500);
    public string m_actionToTrigger;
    public uint m_timeInMsMin;
    public uint m_timeInMsMax;
    public enum TimeType { Between, NotBetween,
        Before,
        After
    }
    public TimeType m_timeConditionType = TimeType.Between;
    public bool m_triggerOnInRange;
    public bool m_triggerOnOutRange;
    public bool m_triggerOnReleaseInRange;
}



