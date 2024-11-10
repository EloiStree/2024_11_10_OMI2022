using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BooleanSwitchListener 
{
    [SerializeField]private bool m_value;
    private OnBooleanChanged m_onChanged;
    private OnBooleanChangedTrigger m_onFalse;
    private OnBooleanChangedTrigger m_onTrue;

    public BooleanSwitchListener()
    {
        ForceTrigger();

    }
    public BooleanSwitchListener(bool startValue)
    {
        m_value = startValue;
        ForceTrigger();
    }

    public void AddChangeListener(OnBooleanChanged toDo)
    {
        m_onChanged += toDo;
    }
    public void RemoveChangeListener(OnBooleanChanged toDo)
    {
        m_onChanged -= toDo;
    }
    public void AddFalseChangeListener(OnBooleanChangedTrigger toDo)
    {
        m_onFalse += toDo;
    }
    public void RemoveFalseChangeListener(OnBooleanChangedTrigger toDo)
    {
        m_onFalse -= toDo;
    }
    public void AddTrueChangeListener(OnBooleanChangedTrigger toDo)
    {
        m_onTrue += toDo;
    }
    public void RemoveTrueChangeListener(OnBooleanChangedTrigger toDo)
    {
        m_onTrue -= toDo;
    }


    public void SetValue(bool value, out bool hasChanged) {
        bool previousValue = m_value;
        m_value = value;
        hasChanged = previousValue != value;
        if (hasChanged) { 
            if (m_onChanged != null)
                m_onChanged(previousValue, value);
            if (m_value && m_onTrue != null)
                m_onTrue();
            else if(!m_value && m_onFalse != null)
                m_onFalse();
        }
    
    }
    public bool GetValue() { return m_value; }

    public void ForceTrigger() {
        if(m_onChanged!=null)
            m_onChanged(m_value, m_value);
        if (m_value && m_onTrue!=null)
            m_onTrue();
        else if(!m_value && m_onFalse != null) 
            m_onFalse();
    }

}
public delegate void OnBooleanChanged(bool previousValue, bool newValue);
public delegate void OnBooleanChangedTrigger();
