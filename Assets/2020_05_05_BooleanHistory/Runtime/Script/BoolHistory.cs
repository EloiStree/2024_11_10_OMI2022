using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoolHistory
{
    public BoolHistory(bool startState, int maxChangeSave = 50)
    {
        m_inProgress = new BoolStatePeriode(startState);
        m_maxSave = maxChangeSave;
    }
     BoolStatePeriode m_inProgress = new BoolStatePeriode(false);
     Queue<BoolStatePeriode> m_previousSave = new Queue<BoolStatePeriode>();
     int m_maxSave = 50;

    public BoolStatePeriode GetInProgressState() {
        return m_inProgress;
    }

    public void SetState(bool state)
    {
        if (m_inProgress.m_state == state) return;
        BoolStatePeriode toUse;
        if (m_previousSave.Count >= m_maxSave)
        {
            toUse = m_previousSave.Dequeue();
            toUse.SetElapsedTime(0);
            toUse.SetStateTo(state);
        }
        else { 
            toUse = new BoolStatePeriode(state);
        }
        m_previousSave.Enqueue(m_inProgress);
        m_inProgress = toUse;
    }
    public void AddElapsedTime(float time)
    {
        m_inProgress.AddSomeElapsedTime(time);
    }

    public void SetMaxSaveTo(int value)
    {
        m_maxSave = value;
    }

    public bool HasChangeRecenlty(float time, out bool current, out int trueCount, out int falseCount)
    {
        float timeCount = m_inProgress.GetElpasedTime();

        current = m_inProgress.GetState();
        trueCount = 0;
        falseCount = 0;
        if (timeCount >= time) { return false; }

        BoolStatePeriode[] history;
        GetFromNowToPast(out history,false);
        int index = 0;
        while (timeCount < time && index < history.Length)
        {
            if (history[index].GetState())
                trueCount++;
            else
                falseCount++;

            timeCount += history[index].GetElpasedTime();
            index++;
        }

        return trueCount > 0 || falseCount > 0;
    }

 

    public void GetFromNowToPast(out BoolStatePeriode[] history, bool countingCurrent)
    {
        BoolStatePeriode[] result;
        GetFromPastToNow(out result, countingCurrent);
        Array.Reverse(result);
        history = result;
    }
    public void GetFromPastToNow(out BoolStatePeriode[] history,bool countingCurrent)
    {
        List<BoolStatePeriode> l = new List<BoolStatePeriode>();
        l.AddRange(m_previousSave.ToArray());
        if(countingCurrent)
            l.Add(m_inProgress);
        history =  l.ToArray();
    }

    public bool GetState()
    {
        return m_inProgress.GetState();
    }

    public void GetFromPastToNow(out TimedBooleanChange[] history, DateTime from) {
        TimedBooleanChange[] tmp;
        GetFromNowToPast(out tmp,from);

        //history = tmp.OrderByDescending(k => k.GetTime()).ToArray();
        history = tmp.Reverse().ToArray();

    }
    public void GetFromNowToPast(out TimedBooleanChange[] history)
    {
        GetFromNowToPast(out history, DateTime.Now);
    }
    public bool WasSetTrue(float time, bool countCurrentState = true)
    {
        if (countCurrentState)
        {
            if (m_inProgress.GetElpasedTime() < time)
            {
                if (m_inProgress.GetState())
                    return true;
            }
        }

        bool current;
        int trueCountSwitch, falseCountSwitch;
        HasChangeRecenlty(time, out current, out trueCountSwitch, out falseCountSwitch);
        return trueCountSwitch > 0;
    }
    public bool WasSetFalse(float time, bool countCurrentState=true)
    {
        if (countCurrentState)
        {
            if (m_inProgress.GetElpasedTime() < time)
            {
                if (!m_inProgress.GetState())
                    return true;
            }
        }

        bool current;
        int trueCountSwitch, falseCountSwitch;
        HasChangeRecenlty(time, out current, out trueCountSwitch, out falseCountSwitch);
        return falseCountSwitch > 0;
    }
    public void GetFromNowToPast(out TimedBooleanChange[] history, DateTime fromNowValue) {
        BoolStatePeriode[] periodeChange;
        GetFromNowToPast(out periodeChange,true);
 
        history = new TimedBooleanChange[periodeChange.Length];
        DateTime timeIndex = fromNowValue;
        for (int i = 0; i < periodeChange.Length; i++)
        {
            BooleanChangeType changeType = periodeChange[i].GetChangeType();
            timeIndex = timeIndex.AddSeconds(-periodeChange[i].GetElpasedTime());
            history[i] = new TimedBooleanChange(changeType, timeIndex);
        }
    }

    public bool HasHistory()
    {
       return  m_previousSave.Count>0;
    }
}

public class BoolStatePeriode
{
    public bool m_state;
    public float m_elapsedTime;

    public BoolStatePeriode(bool state, float elapsedTime = 0)
    {
        m_state = state;
        m_elapsedTime = elapsedTime;
    }

    public void SetStateTo(bool value) { m_state = value; }
    public bool GetState() { return m_state; }
    public float GetElpasedTime() { return m_elapsedTime; }
    public void AddSomeElapsedTime(float time) { m_elapsedTime += time; }
    public void SetElapsedTime(float time) { m_elapsedTime = time; }
   
    public BooleanChangeType GetChangeType()
    {
        return m_state ? BooleanChangeType.SetTrue : BooleanChangeType.SetFalse;
    }
}

[System.Serializable]
public class BooleanChange
{
    BooleanChangeType m_changeType;
    public BooleanChange( BooleanChangeType changeType)
    {
        m_changeType = changeType;
    }

    public BooleanChangeType GetChange() { return m_changeType; }
}

public class TimedBooleanChange : BooleanChange
{
    DateTime m_time;
    public TimedBooleanChange(BooleanChangeType changeType, DateTime time) : base( changeType)
    {
        m_time = time;
    }
    public TimedBooleanChange(BooleanChangeType changeType) : base(changeType)
    {
        m_time = DateTime.Now;
    }
   
    public DateTime GetTime() { return m_time; }
}



public enum BooleanInverseTag
{ None, Inverse}

public enum BooleanChangeType
{
    SetTrue, SetFalse
}