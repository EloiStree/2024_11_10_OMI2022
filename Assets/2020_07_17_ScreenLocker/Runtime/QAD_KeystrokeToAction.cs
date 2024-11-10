using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class QAD_KeystrokeToAction : MonoBehaviour
{
    public KeyboardReadMono m_reader;
    public KeyboardTouch m_key;
    public UnityEvent m_actionDown;
    public UnityEvent m_actionUp;
    public bool m_current;
    public bool m_previous;
    void Update()
    {
       
            BoolHistory boolHistory = m_reader.GetTouchHistory(m_key);
            bool current;
            int tc, fc;
            boolHistory.HasChangeRecenlty(Time.deltaTime, out current, out tc, out fc);
            m_current = current;
           bool hasChange = m_current != m_previous;
            if (hasChange && current )
            {
                m_actionDown.Invoke();
            }
            if (hasChange && !current)
            {
                m_actionUp.Invoke();
            }
            m_previous = m_current;
        
    }
}
