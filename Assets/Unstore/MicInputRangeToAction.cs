using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//BUGGED work but did not work last time i tried
public class MicInputRangeToAction : MonoBehaviour
{
    public MicInputObserverRanges m_rangesObserved;
    public List<RangeObserver> m_observers= new List<RangeObserver>();



    [System.Serializable]
    public class RangeObserver {
        public string m_name;
        public UnityEvent m_inRange;
        public UnityEvent m_outRange;
        public void InRange()
        {
            m_inRange.Invoke();
        }
        public void OutRange()
        {
            m_outRange.Invoke();
        }
    }


    public void Awake()
    {
        Invoke("QuickSleepyTest", 0.5f);
       
    }

    private void QuickSleepyTest()
    {
        for (int i = 0; i < m_observers.Count; i++)
        {
            if (m_rangesObserved.Containt(m_observers[i].m_name))
                m_rangesObserved.AddListenerInOutRange(m_observers[i].m_name
                    , m_observers[i].InRange, m_observers[i].OutRange);

        }
    }
}
