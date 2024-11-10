using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class CompressDigitalAndAnalogIndexListener : MonoBehaviour
{


    public List<ListenToDigital> m_listenToDigital = new List<ListenToDigital>();
    public List<ListenToAnalog> m_listenToAnalog = new List<ListenToAnalog>();

    [Header("Debug")]
    public CompressDigitalAndAnalogListener m_state = new CompressDigitalAndAnalogListener();

    public void OnEnable()
    {
        m_state.AddAnalogListener(WhenAnalogChange);
        m_state.AddDigitalListener(WhenDigitChange);

    }
    public void OnDisable()
    {
        m_state.RemoveAnalogListener(WhenAnalogChange);
        m_state.RemoveDigitalListener(WhenDigitChange);

    }

    public void SetWithCompressStateMessage(string msg)
    {
        m_state.SetState(msg);

    }

    private void WhenDigitChange(string givenName, int index, bool newValue)
    {
        for (int i = 0; i < m_listenToDigital.Count; i++)
        {
            if (m_listenToDigital[i].GetIndexListened() == index)
                m_listenToDigital[i].SetNewValue(newValue);

        }
    }

    private void WhenAnalogChange(string givenName, int index, Digit oldValue, Digit newValue)
    {
        for (int i = 0; i < m_listenToAnalog.Count; i++)
        {
            if (m_listenToAnalog[i].GetIndexListened() == index)
            {
                m_listenToAnalog[i].SetNewValue((int)newValue);
            }

        }
    }

    public void AddListener(ListenToDigital listener)
    {

        m_listenToDigital.Add(listener);
    }
    public void AddListener(ListenToAnalog listener)
    {
        m_listenToAnalog.Add(listener);
    }
    public void RemoveListener(ListenToDigital listener)
    {
        m_listenToDigital.Remove(listener);
    }
    public void RemoveListener(ListenToAnalog listener)
    {
        m_listenToAnalog.Remove(listener);
    }

    public void Reset()
    {
        for (int i = 0; i < 2; i++)
        {
            m_listenToDigital.Add(new ListenToDigital() { m_index = i });

        }
        for (int i = 0; i < 1; i++)
        {
            m_listenToAnalog.Add(new ListenToAnalog() { m_index = i });

        }
    }





  

}
[Serializable]
public class BoolChangeEvent : UnityEvent<bool> { }
[Serializable]
public class FloatChangeEvent : UnityEvent<int> { }
[Serializable]
public class ListenToDigital
{
    public int m_index;
    public BoolChangeEvent m_onChange= new BoolChangeEvent();
    public bool m_value;

    public void SetNewValue(bool value)
    {
        if (m_value != value)
        {
            m_value = value;
            m_onChange.Invoke(m_value);
        }
    }
    public int GetIndexListened()
    {
        return m_index;
    }
}
[Serializable]
public class ListenToAnalog
{
    public int m_index;
    public int m_threshold = 3;
    public FloatChangeEvent m_onAnalogChange= new FloatChangeEvent();
    public BoolChangeEvent m_onDigitalChange = new BoolChangeEvent();
    public int m_analogValue;
    public bool m_boolValue;

    public void SetNewValue(int value)
    {
        bool boolValue = value > m_threshold;
        if (m_boolValue != boolValue)
        {
            m_boolValue = boolValue;
            m_onDigitalChange.Invoke(m_boolValue);
        }
        if (m_analogValue != value)
        {
            m_analogValue = value;
            m_onAnalogChange.Invoke(m_analogValue);
        }

    }

    public int GetIndexListened()
    {
        return m_index;
    }
}