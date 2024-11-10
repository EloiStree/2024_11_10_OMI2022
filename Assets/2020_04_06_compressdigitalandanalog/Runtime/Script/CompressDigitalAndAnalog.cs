using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;


public interface ICompressDigitalAndAnalogListener {


    void SetGivenName(string name);
    void SetState(string state); //  #00...0|99...99

    bool IsDigitalActive(int index, out bool exist);
    bool IsAnalogActive(int index, out bool exist, Digit threshold);
    int GetAnalogCount();
    int GetDigitalCount();
    string GetGivenName();
    void AddDigitalListener(DigitalChange listener);
    void AddAnalogListener(AnalogChange listener);
    void RemoveDigitalListener(DigitalChange listener);
    void RemoveAnalogListener(AnalogChange listener);
}
public delegate void DigitalChange(string givenName, int index, bool newValue);
public delegate void AnalogChange(string givenName, int index, Digit oldValue, Digit newValue);
public enum Digit : int { _0, _1, _2, _3, _4, _5, _6, _7, _8, _9  }

public class CompressDigitalAndAnalogListener: ICompressDigitalAndAnalogListener {

    public  static  bool IsCompressionValide(string compression) {
        return Regex.Matches(compression, "#\\d*\\|\\d*", RegexOptions.IgnoreCase).Count > 0;
    }
    public static void GetValueOfCompression(string compression, out bool[] digit, out short [] analog) {
        
        string[] tokens = compression.Replace("#", "").Split('|');
        string d = tokens[0];
        string a = tokens[1];
        digit = new bool[d.Length];
        analog = new short[a.Length];
        for (int i = 0; i < d.Length; i++)
        {
            digit[i] = d[i] == '1';
        }
        for (int i = 0; i < analog.Length; i++)
        {
            analog[i] =  ParseChar(a[i]);
        }

    }


    public string m_givenName;
    public List<DigitalValueObserve> m_listenToDigital = new List<DigitalValueObserve>();
    public List<AnalogValueObserve> m_listenToAnalog = new List<AnalogValueObserve>();
    public DigitalChange m_digitalListeners;
    public AnalogChange m_analogListeners;
    [SerializeField]
    [Header("Debug")]
    public string m_lastMessageReceived;

    #region STATE MANAGEMENT
    public void SetState(string state)
    {
        string messageValideRegex = 
        m_lastMessageReceived = state;
        if (IsCompressionValide(state))
        {
            bool[] digits;
            short[] analog;
            GetValueOfCompression(state, out digits, out analog);
            SetDigitalListenerCountToMinimum(digits.Length);
            SetAnalogListenerCountToMinimum(analog.Length);

            for (int i = 0; i < digits.Length; i++)
            {
                if (i < m_listenToDigital.Count)
                {
                    m_listenToDigital[i].SetNewValue(digits[i]);

                    if (m_digitalListeners != null  && m_listenToDigital[i].HasChange()) {
                        m_digitalListeners(m_givenName, i, m_listenToDigital[i].IsOn());
                    }
                }
            }
            for (int i = 0; i < analog.Length; i++)
            {
                if (i < m_listenToAnalog.Count)
                {
                    m_listenToAnalog[i].SetNewValue(analog[i]);
                    if (m_analogListeners != null && m_listenToAnalog[i].HasChange())
                    {
                        m_analogListeners(m_givenName, i, (Digit) m_listenToAnalog[i].GetPreviousValue(), (Digit)m_listenToAnalog[i].GetValue());
                    }
                }
            }

        }
    }
    private static short ParseChar(char v)
    {
        switch (v)
        {
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            default:
                return 0;
        }
    }


    void SetDigitalListenerCountToMinimum(int v)
    {
        int difference = v - m_listenToDigital.Count;
        if (difference > 0)
            for (int i = 0; i < difference; i++)
            {
                m_listenToDigital.Add(new DigitalValueObserve());

            }

    }
    void SetAnalogListenerCountToMinimum(int v)
    {
        int difference = v - m_listenToAnalog.Count;
        if (difference > 0)
            for (int i = 0; i < difference; i++)
            {
                m_listenToAnalog.Add(new AnalogValueObserve());

            }
    }
   
    #endregion

    public int GetAnalogCount()
    {
        return m_listenToAnalog.Count;
    }

    public int GetDigitalCount()
    {
        return m_listenToDigital.Count;
    }

    public string GetGivenName()
    {
        return m_givenName;
    }

  

    public bool IsDigitalActive(int index, out bool exist)
    {
        if (index < 0 || index >= m_listenToDigital.Count)
        {
            exist = false; return false;
        }

        exist = true;
        return m_listenToDigital[index].IsOn();
    }
    public void AddAnalogListener(AnalogChange listener)
    {
        m_analogListeners += listener;
    }

    public void AddDigitalListener(DigitalChange listener)
    {
        m_digitalListeners += listener;
    }
    public void RemoveAnalogListener(AnalogChange listener)
    {
        m_analogListeners -= listener;
    }

    public void RemoveDigitalListener(DigitalChange listener)
    {
        m_digitalListeners -= listener;
    }

    public void SetGivenName(string name)
    {
        m_givenName = name;
    }

    public bool IsAnalogActive(int index, out bool exist, Digit threshold)
    {
        if (index < 0 || index >= m_listenToAnalog.Count)
        {
            exist = false; return false;
        }

        exist = true;
        return m_listenToAnalog[index].IsOn(threshold);
    }
}
[Serializable]
public class DigitalValueObserve
{
    public bool m_digitalValue;
    public bool m_previousValue;

    public void SetNewValue(bool value)
    {
        m_previousValue = m_digitalValue;
        m_digitalValue = value;
    }
    public bool GetPreviousValue()
    {
        return m_previousValue;
    }

    public bool IsOn()
    {
        return m_digitalValue;
    }
    public bool HasChange() {
        return m_digitalValue != m_previousValue;
    }
}
[Serializable]
public class AnalogValueObserve
{
    public int m_analogValue;
    int m_previousValue;

    public void SetNewValue(int value)
    {
       
            m_previousValue= m_analogValue;
            m_analogValue = value;
  
    }
    public float GetValue()
    {
        return m_analogValue;
    }
    public float GetPreviousValue()
    {
        return m_previousValue;
    }

    public bool IsOn(Digit threshold)
    {
        return m_analogValue >= (int) threshold;
    }
    public bool HasChange()
    {
        return m_analogValue != m_previousValue;
    }
}
