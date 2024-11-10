
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class EnumDictionary<T> where T : struct, IConvertible
{
    public List<T> m_activeElements = new List<T>();
    public Dictionary<T, bool> m_elementStates = new Dictionary<T, bool>();
    public delegate void OnStateChange(T element, bool isOn);
    public OnStateChange onStateChange;

    public EnumDictionary()
    {
        List<T> values = GetEnumList<T>();
        for (int i = 0; i < values.Count; i++)
        {
            if (!m_elementStates.ContainsKey(values[i]))
                m_elementStates.Add(values[i], false);
        }
    }
    public static List<G> GetEnumList<G>() where G : struct, IConvertible
    {
        return Enum.GetValues(typeof(G)).OfType<G>().ToList();
    }

    public void SetState(T element, bool value)
    {
        CheckExisting(element);
        if (value && m_elementStates[element] == false)
        {
            m_activeElements.Add(element);
            m_elementStates[element] = true;
            if (onStateChange != null)
                onStateChange(element, true);
        }
        else if (!value && m_elementStates[element] == true)
        {
            m_activeElements.Remove(element);
            m_elementStates[element] = false;
            if (onStateChange != null)
                onStateChange(element, false);
        }

    }

    private void CheckExisting(T element)
    {
        if (!m_elementStates.ContainsKey(element)) {
            m_elementStates.Add(element, false);
        }
    }

    public bool GetState(T element)
    {
        CheckExisting(element);
        return m_elementStates[element];
    }

    public List<T> GetActiveElements()
    {
        return m_activeElements;
    }

    public void Clear()
    {
        m_elementStates.Clear();
        m_activeElements.Clear();
    }
}



[System.Serializable]
public class TimeDictionary<T> where T : struct, IConvertible
{
    public Dictionary<T, DateTime> m_elementStates = new Dictionary<T, DateTime>();
    public delegate void OnStateChange(T element, DateTime isOn);
    public OnStateChange onStateChange;

    public TimeDictionary()
    {
        List<T> values = GetEnumList<T>();
        for (int i = 0; i < values.Count; i++)
        {
            if (!m_elementStates.ContainsKey(values[i]))
                m_elementStates.Add(values[i], GetNoDateRepresentative());
        }
    }
    DateTime m_zero = new DateTime(1970, 1, 1);
    public DateTime GetNoDateRepresentative() { return m_zero; }
    public DateTime GetNowTime() { return DateTime.Now; }
    public static List<G> GetEnumList<G>() where G : struct, IConvertible
    {
        return Enum.GetValues(typeof(G)).OfType<G>().ToList();
    }

    public void SetTimeRecorded(T element, DateTime value)
    {
        CheckExisting(element);
        if ( m_elementStates[element] != value)
        {
            m_elementStates[element] = value;
            if (onStateChange != null)
                onStateChange(element, value);
        }

    }

    private void CheckExisting(T element)
    {
        if (!m_elementStates.ContainsKey(element))
        {
            m_elementStates.Add(element, GetNoDateRepresentative());
        }
    }

    public DateTime GetTime(T element)
    {
        CheckExisting(element);
        return m_elementStates[element];
    }
    public double GetTimeInSecondsBetween(DateTime oldTime, DateTime newTime)
    {
        var diffInSeconds = (newTime - oldTime).TotalSeconds;
        return diffInSeconds;
    }
    public double GetTimeInMillisecondsBetween(DateTime oldTime, DateTime newTime)
    {
        var diffInSeconds = (newTime - oldTime).TotalMilliseconds;
        return diffInSeconds;
    }

    public void Clear()
    {
        m_elementStates.Clear();
     }

    public bool HasBeenUsedYet(T keyboardTouch)
    {
        CheckExisting(keyboardTouch);
        return m_elementStates[keyboardTouch] != GetNoDateRepresentative();
    }

    public double GetTimeInSeconds(T touchRequest)
    {
        DateTime time = GetTime(touchRequest);
        return GetTimeInSecondsBetween(time, GetNowTime());
    }
}
