using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LooperRegisterAndHandlerMono : MonoBehaviour
{
    public TimeThreadMono m_timer;
    public Dictionary<string, PingLooperRef> m_register = new Dictionary<string, PingLooperRef>();
    public CommandLineEvent m_commandLine;

    public void Clear()
    {
        m_register.Clear();
        m_loopAsList.Clear();
    }

    public List<PingLooperRef> m_loopAsList = new List<PingLooperRef>();
    public float m_frameCheckInMs=20;
    public void Start()
    {
        TimeThreadMono.LoopPing tmp = new TimeThreadMono.LoopPing(m_frameCheckInMs/1000f, PingThreadType.InUnityThread);
        tmp.SetCallBack(RemoveDeltaPast);
        m_timer.SubscribeLoop(tmp);
    }

    private void RemoveDeltaPast()
    {
        current = DateTime.Now;
        int millisecond = (current - previous).Milliseconds;
        for (int i = 0; i < m_loopAsList.Count; i++)
        {
            if (m_loopAsList[i].HasTimeLeft())
            { 
                m_loopAsList[i].RemoveTime(millisecond);
            }
            if (!m_loopAsList[i].HasTimeLeft())
            {
                m_loopAsList[i].ResetTimer();
                m_loopAsList[i].Ping();
            }
        }

        previous = current;
    }

    public void Remove(string nameId)
    {
        if (m_register.ContainsKey(nameId))
        {
            m_loopAsList.Remove(m_register[nameId]);
            m_register.Remove(nameId);
        }
    }

    public DateTime previous;
    public DateTime current;


    public void SetOnOff(string nameId, bool isOn)
    {

        if (m_register.ContainsKey(nameId))
        {
            m_register[nameId].SetAsActive(isOn);
        }
    }
    public void ResetTime(string nameId)
    {

        if (m_register.ContainsKey(nameId))
        {
            m_register[nameId].ResetTimer();
        }
    }
    public void Add(string nameId,PingLooperRef loopRef) {
        if (!m_register.ContainsKey(nameId))
        {
            m_register.Add(nameId, loopRef);
            m_loopAsList.Add(loopRef);
        }
    }

    public void SetTime(string nameId, uint millisecond)
    {
        if (m_register.ContainsKey(nameId))
        {
            m_register[nameId].SetTimeOfFrame(millisecond);
        }
    }
    //
    public void Create(string nameId, uint millisecond, Action actionToDo)
    {
        Remove(nameId);
        Add(nameId, new PingLooperRef(millisecond, actionToDo,false));
    }
    public void Create(NamedLooperBean loop)
    {
        Create(loop.m_name, loop.m_timeBetweenInMs, ()=>ExecuteCommandOf(loop));
    }

    private void ExecuteCommandOf(NamedLooperBean loop)
    {
        for (int i = 0; i < loop.m_linkedAction.Count; i++)
        {
            m_commandLine.Invoke(loop.m_linkedAction[i]);
        }
    }
}

public class NamedLooperBean {
    public string m_name;
    public uint m_timeBetweenInMs;
    public List<CommandLine> m_linkedAction;
    
}

[System.Serializable]
public class PingLooperRef {

    public bool m_isActive;
    public int m_millisecondFrameTime=1;
    public int m_millisecondFrameTimeLeft;
    public Action m_toDo;

    public PingLooperRef(uint millisecond, Action actionToDo, bool isActiveAtStart)
    {
        m_isActive = isActiveAtStart;
        this.m_millisecondFrameTime = (int)millisecond;
        this.m_toDo = actionToDo;
    }

    public void ResetTimer() {
        m_millisecondFrameTimeLeft = m_millisecondFrameTime;
    }
    public void Ping() {
        if (m_isActive)
            m_toDo.Invoke();
    }
    public bool HasTimeLeft() {
        return m_millisecondFrameTimeLeft > 0;
    }

    public void RemoveTime(int millisecond)
    {
        if(m_millisecondFrameTimeLeft>0)
        m_millisecondFrameTimeLeft =(int) (m_millisecondFrameTimeLeft-millisecond);
    }

    public void SetAsActive(bool isOn)
    {
        m_isActive = isOn;
    }
    public void Pause()
    {
        m_isActive = false;
    }
    public void Resume()
    {
        m_isActive = true;
    }
    public void Play()
    {
        m_isActive = true;
        ResetTimer();
    }
    public void SetActionOnPing(Action action) {
        m_toDo = action;
    }
    public void SetTimeOfFrame(uint millisecond)
    {
        m_millisecondFrameTime = (int)millisecond;
    }
}
