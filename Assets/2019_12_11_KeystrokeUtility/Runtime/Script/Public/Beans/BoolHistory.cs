using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHistory 
{
    public BoolHistory(bool startState, int maxChangeSave=50) {
        m_inProgress = new BoolStatePeriode(startState);
        m_maxSave = maxChangeSave;
    }
    public BoolStatePeriode m_inProgress = new BoolStatePeriode(false);
    public Queue<BoolStatePeriode> m_previousSave = new Queue<BoolStatePeriode>();
    public int m_maxSave=50;

    public void SetState(bool state) {
        if (m_inProgress.m_state == state) return;
        if (m_previousSave.Count >= m_maxSave)
            m_previousSave.Dequeue();
        m_previousSave.Enqueue(m_inProgress);
        m_inProgress = new BoolStatePeriode(state);
    }
    public void AddElapsedTime(float time) {
        m_inProgress.AddSomeElapsedTime(time);
    }

    public void SetMaxSaveTo(int value) {
        m_maxSave = value;
    }

    public bool HasChangeRecenlty(float time,out bool current, out int trueCount, out int falseCount) {
        float timeCount=m_inProgress.GetElpasedTime();

        current = m_inProgress.GetState();
        trueCount = 0;
        falseCount = 0;
        if (timeCount >= time) { return false; }

        BoolStatePeriode[] history = GetFromNowToPast();
        int index=0;
        while (timeCount < time && index<history.Length) {
            if (history[index].GetState())
                trueCount++;
            else 
                falseCount++;

           timeCount += history[index].GetElpasedTime();
           index++;
        }

        return trueCount > 0 || falseCount > 0;
    }


    public string GetDescriptionPastToNow(float timeWatch = 1f,float dotPerSecond=4, string falseSym="_", string trueSym="-", string switchTrueSym= "1↓", string switchFalseSym= "0↑")
    {
        string result="";
        if (dotPerSecond == 0)
            dotPerSecond = 1;
        BoolStatePeriode[] history = GetFromPastToNow();
        
        for (int i = 0; i < history.Length; i++)
        {

            for (int j = 0; j < 1+(int)(history[i].m_elapsedTime/(1f/ dotPerSecond)); j++)
            {
                result += history[i].GetState() ? trueSym : falseSym;

            }
            result += history[i].GetState() ? switchTrueSym : switchFalseSym;
        }
        for (int j = 0; j < 1 + (int)(m_inProgress.GetElpasedTime()/ (1f / dotPerSecond)); j++)
        {
            result += GetState() ? trueSym : falseSym;

        }
        result += GetState() ? switchTrueSym : switchFalseSym;

        return result;
    }
    public string GetDescriptionNowToPast(float timeWatch = 1f,float dotPerSecond = 4, string falseSym = "_", string trueSym = "-", string switchTrueSym = "1↓", string switchFalseSym = "0↑")
    {
        bool watcherUse=false;
        float timePast=0;
        string result = "";
        if (dotPerSecond == 0)
            dotPerSecond = 1;
        BoolStatePeriode[] history = GetFromNowToPast();
        result += GetState() ? switchTrueSym : switchFalseSym;
        for (int j = 0; j < 1 + (int)(m_inProgress.GetElpasedTime() / (1f / dotPerSecond)); j++)
        {
            timePast += (1f / (float)dotPerSecond);
            if (!watcherUse && timePast > timeWatch)
            {
                watcherUse = true;
                result += "|";
            }
            result += GetState() ? trueSym : falseSym;
        }

        for (int i = 0; i < history.Length; i++)
        {

            result += history[i].GetState() ? switchTrueSym : switchFalseSym;
            for (int j = 0; j < 1 + (int)(history[i].m_elapsedTime / (1f / dotPerSecond)); j++)
            {
                timePast += (1f / (float)dotPerSecond);
                if (!watcherUse && timePast > timeWatch)
                {
                    watcherUse = true;
                       result += "|";
                }
                result += history[i].GetState() ? trueSym : falseSym;

            }
        }
       

        return result;
    }
    public string GetNumericDescriptionNowToPast(float timeWatch = 1f,string switchTrueSym = "↓", string switchFalseSym = "↑")
    {
        bool watcherUse = false;
        float timePast = 0;
        string result = "";
        BoolStatePeriode[] history = GetFromNowToPast();
        result += string.Format("{0:0.00}{1}", m_inProgress.GetElpasedTime(), (GetState() ? switchTrueSym : switchFalseSym));
       

        for (int i = 0; i < history.Length; i++)
        {

            result += string.Format("{0:0.00}{1}", history[i].GetElpasedTime() , (history[i].GetState() ? switchTrueSym : switchFalseSym));
            timePast += history[i].GetElpasedTime();

            if (!watcherUse && timePast > timeWatch)
            {
                watcherUse = true;
                result += "|";
            }
        }


        return result;
    }

    public BoolStatePeriode [] GetFromNowToPast() {
        BoolStatePeriode[] result = GetFromPastToNow();
        Array.Reverse(result);
        return result;  }
    public BoolStatePeriode [] GetFromPastToNow() {
        return m_previousSave.ToArray();
    }

    public struct BoolStatePeriode {
        public bool m_state;
        public float m_elapsedTime;

        public BoolStatePeriode(bool state, float elapsedTime=0)
        {
            m_state = state;
            m_elapsedTime = elapsedTime;
        }

        public void SetStateTo(bool value) { m_state = value; }
        public bool GetState() { return m_state; }
        public float GetElpasedTime() { return m_elapsedTime; }
        public void AddSomeElapsedTime(float time) {  m_elapsedTime += time; }
        public void SetElapsedTime(float time) {  m_elapsedTime = time; }

    }

    public bool GetState()
    {
        return m_inProgress.GetState();
    }
}
