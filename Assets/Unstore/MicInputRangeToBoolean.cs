using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MicInputRangeToBoolean : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public MicInputObserverRanges m_rangesObserved;
    public List<RangeObserver> m_observers = new List<RangeObserver>();



    [System.Serializable]
    public class RangeObserver
    {
        public string m_nameOfDevice;
        public string m_nameOfBoolean;
        
    }

    public void Clear()
    {
        m_observers.Clear();
        m.Clear();
    }
    public void Add(string deviceName, string booleanName, float minIntensity, float maxIntensity) {

        RangeObserver r = new RangeObserver()
        {
            m_nameOfBoolean = booleanName,
            m_nameOfDevice = deviceName
        };

        m_observers.Add(r);
         BooleanStateRegister reg = null;
         m_register.GetRegister(ref reg);
        RangeObserverListener listener = new RangeObserverListener() { m_observed = r, m_reg = reg };
        m_rangesObserved.Add(deviceName, minIntensity, maxIntensity,
           listener.SetTrue, listener.SetFalse) ;
        m.Add(listener);
            
    }
    private List<RangeObserverListener> m = new List<RangeObserverListener>();

    private class RangeObserverListener {
        public RangeObserver m_observed;
        public BooleanStateRegister m_reg;
        public void SetTrue() { m_reg.Set(m_observed.m_nameOfBoolean, true); }
        public void SetFalse() { m_reg.Set(m_observed.m_nameOfBoolean, false); }
    
    }
}
