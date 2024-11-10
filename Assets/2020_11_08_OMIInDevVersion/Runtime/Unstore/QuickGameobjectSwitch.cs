using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickGameobjectSwitch : MonoBehaviour
{
    public bool m_value;
    public UnityEvent m_on;
    public UnityEvent m_off;
    public SwitchEvent m_switch;
    [System.Serializable]
    public class SwitchEvent : UnityEvent<bool> { }

    public void Switch()
    {
        m_value = !m_value;
        if (m_value)
            m_on.Invoke();
        else m_off.Invoke();
        m_switch.Invoke(m_value);
    }
    public void SetOn()
    {
        m_value = true;
        m_on.Invoke();
        m_switch.Invoke(m_value);
    }
    public void SetOff()
    {
        m_value = false;
        m_off.Invoke();
        m_switch.Invoke(m_value);
    }
}
