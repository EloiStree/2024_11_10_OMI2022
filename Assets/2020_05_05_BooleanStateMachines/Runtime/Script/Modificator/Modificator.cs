using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Modificator 
{
}

public class BetweenTime  : Modificator
{
    [SerializeField] protected float m_minTimeInSecond;
    [SerializeField] protected float m_maxTimeInSecond;

    public BetweenTime(float minTimeInSecond, float maxtimeInSecond)
    {
        m_minTimeInSecond = minTimeInSecond;
        m_maxTimeInSecond = maxtimeInSecond;
    }
}
public class BetweenTimeValue {
    BetweenTime m_info;
    public BetweenTimeValue(BetweenTime info)
    {
        m_info = info;
    }
    [SerializeField] float m_time;
    public void Reset() {
        m_time = 0;
    }
    public void AddTime(float time) {
        m_time = time;
    }
    public float GetTime() { return m_time; }
}

public class Cooldown : Modificator
{
    [SerializeField] float m_timeInSecond;

    public Cooldown(float maxTimeInSecond)
    {
        m_timeInSecond = maxTimeInSecond;
    }
}
public class CooldownValue
{
    Cooldown m_info;

    public CooldownValue(Cooldown info)
    {
        m_info = info;
    }
    [SerializeField] float m_time;
    public void Reset()
    {
        m_time = 0;
    }
    public void AddTime(float time)
    {
        m_time = time;
    }
    public float GetTime() { return m_time; }
}
public class MaxTime : BetweenTime
{
   
    public MaxTime(float maxTimeInSecond): base(0, maxTimeInSecond)
    {}
}
public class ChangeBoolTimeObserved : Modificator
{
    [SerializeField] float m_timeInSecond;

    public ChangeBoolTimeObserved(float timeInSecond)
    {
        m_timeInSecond = timeInSecond;
    }
}