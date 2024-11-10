using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicInputObserverRanges : MonoBehaviour
{
    public MicInputObserver m_observer;

    public List<RangeObservedState> m_rangeObserved;

    [System.Serializable]
    public class RangeObserved {

        public string m_name="";
        public float m_min=0, m_max=1;

        public bool IsInRange(float lastValue)
        {
            return lastValue >= m_min && lastValue <= m_max;
        }
    }

    internal void Clear()
    {
        m_rangeObserved.Clear();
    }

    [System.Serializable]
    public class RangeObservedState {

        public RangeObserved m_ref= new RangeObserved();
        public string m_realName="";
        public float m_currentValue=0;
        public BooleanSwitchListener m_boolState= new BooleanSwitchListener();


        internal void SetValue(float lastValue)
        {
            bool isInRange = m_ref.IsInRange(lastValue);
            m_currentValue = lastValue;
            bool hasChange;
            m_boolState.SetValue( isInRange, out hasChange);

        }
    }

    public void Add(string deviceName, float minIntensity, float maxIntensity, OnBooleanChangedTrigger inrange, OnBooleanChangedTrigger outrange)
    {
        RangeObservedState created;
        Add(deviceName, minIntensity, maxIntensity, out created);
        if (created != null)
        { 
            created.m_boolState.AddTrueChangeListener(inrange);
            created.m_boolState.AddFalseChangeListener(outrange);
        }
    }

    public void GetObserver(string name, out RangeObservedState state) {
        state = null;
        for (int i = 0; i < m_rangeObserved.Count; i++)
        {
            if (m_rangeObserved[i].m_ref.m_name == name)
                state = m_rangeObserved[i];
        }
    }

    public void AddListenerInOutRange(string name, OnBooleanChangedTrigger inRange, OnBooleanChangedTrigger outRange)
    {

        RangeObservedState state;
        GetObserver(name, out state);
        if (state != null) { 
            state.m_boolState.AddTrueChangeListener(inRange);
            state.m_boolState.AddFalseChangeListener(outRange);
        }
    }

    public bool Containt(string name)
    {
        RangeObservedState state;
        GetObserver(name, out state);
        return state != null;
    }
    public void Add(string name, float min, float max, out RangeObservedState created)
    {
        created = null;
        
            RangeObservedState s = new RangeObservedState();
            s.m_ref.m_name = name;
            s.m_ref.m_min = min;
            s.m_ref.m_max = max;
            m_rangeObserved.Add(s);
            m_observer.TryToAddMicToListen(name);
            created = s;

        
    }

    public bool m_useAutoRefresh=true;
    public float m_refreshTime=0.1f;

    void Start()
    {
        if(m_useAutoRefresh)
          InvokeRepeating("Refresh", 0, m_refreshTime);
    }

    void Refresh()
    {
        
        for (int i = 0; i < m_rangeObserved.Count; i++)
        {
            if (string.IsNullOrEmpty(m_rangeObserved[i].m_realName)) {
                bool found;
                m_observer.TryToGetCorrespondingName(m_rangeObserved[i].m_ref.m_name,
                   out found, out m_rangeObserved[i].m_realName);
            }

            if (!string.IsNullOrEmpty(m_rangeObserved[i].m_realName)) {
                if (!m_observer.Contain(m_rangeObserved[i].m_realName))
                {
                    m_observer.TryToAddMicToListen(m_rangeObserved[i].m_realName);
                }
                else
                {
                    float lastValue = m_observer.GetValueOf(m_rangeObserved[i].m_realName);
                    // if (lastValue < m_rangeObserved[i].m_ref.m_min)
                    //    lastValue = 0;
                    m_rangeObserved[i].SetValue(lastValue);
                }
            }
           
            
        }
    }
}
