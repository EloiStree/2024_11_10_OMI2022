using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;
using UnityEngine;

public class TimeThreadMono : MonoBehaviour
{

    public System.Threading.ThreadPriority m_priorityLevel = System.Threading.ThreadPriority.BelowNormal;
    public int m_sleepTimeInMs=10;
    public bool m_autoStart = true;
    public float m_timeBetweenUpdateInMs = 50;
    public long m_frameUnityPingCount;

    private void Awake()
    {
        if (m_autoStart) {
            StartTheTimer();
        }
        InvokeRepeating("CheckForUnityPing",0, m_timeBetweenUpdateInMs/1000f);
    }

    private void CheckForUnityPing()
    {

        FlushByPinging(ref m_toPingAsapInUnityThread);
        CheckForLoopPing(PingThreadType.InUnityThread);
        m_frameUnityPingCount++;
    }


    public DateTime m_previous;
    public DateTime m_current;
    [Header("Debug")]
    public uint m_deltaTime;
    public uint m_milliSecondSecond;
    public double m_timePastInMs;
    private void TimeLooper()
    {
        m_previous = m_current = DateTime.Now;
        while (m_wantTheThreadAlive)
        {
            m_current = DateTime.Now;
            m_deltaTime = (uint)((m_current - m_previous).TotalMilliseconds);
            m_milliSecondSecond += m_deltaTime;
            m_milliSecondSecond = m_milliSecondSecond % 1000;
            m_timePastInMs += m_deltaTime;

            for (int i = 0; i < m_registered.Count; i++)
            {
                if (m_registered[i] != null && m_registered[i].IsTimePast(m_current))
                {
                    m_registered[i].SetAsTimePast(true);

                }
            }
            for (int i = m_registered.Count - 1; i >= 0; i--)
            {
                if (m_registered[i] != null && m_registered[i].IsTimeSetAsPast())
                {

                    SetReadyToBePing(m_registered[i]);
                    if (m_registered[i].m_linked.m_threadType == PingThreadType.InTimeThread)
                    {
                        m_toPingAsapInThread.Add(m_registered[i]);
                    }
                    else
                    {
                        m_toPingAsapInUnityThread.Add(m_registered[i]);

                    }
                    m_registered.RemoveAt(i);
                }
            }

            FlushByPinging(ref m_toPingAsapInThread);

            for (int i = 0; i < m_loopPingUnder1Seconds.Count; i++)
            {
                bool hasReachCycle;
                m_loopPingUnder1Seconds[i].SetNewModulo(m_milliSecondSecond, out hasReachCycle);
                if (hasReachCycle)
                {
                    m_loopPingUnder1Seconds[i].AddPing();
                }

            }


            for (int i = 0; i < m_loopPingOver1Seconds.Count; i++)
            {
                bool hasReachCycle;
                m_loopPingOver1Seconds[i].SetNewModulo(m_timePastInMs, out hasReachCycle);
                if (hasReachCycle)
                    m_loopPingOver1Seconds[i].AddPing();
            }

            CheckForLoopPing(PingThreadType.InTimeThread);

            m_previous = m_current;
            Thread.Sleep(m_sleepTimeInMs);
        }


    }

    private void CheckForLoopPing(PingThreadType threadType)
    {
        for (int i = 0; i < m_loopPingUnder1Seconds.Count; i++)
        {

            if (m_loopPingUnder1Seconds[i].m_linked.m_threadType == threadType)
            {
                int j;
                m_loopPingUnder1Seconds[i].GetPingCountAndFlush(out j);
                while (j > 0)
                {
                    PingLoop(m_loopPingUnder1Seconds[i]);
                    j--;
                }
            }
        }
        for (int i = 0; i < m_loopPingOver1Seconds.Count; i++)
        {
            if (m_loopPingOver1Seconds[i].m_linked.m_threadType == threadType)
            {
                int j;
                m_loopPingOver1Seconds[i].GetPingCountAndFlush(out j);
                while (j > 0)
                {
                    PingLoop(m_loopPingOver1Seconds[i]);
                    j--;
                }
            }
        }
    }

    private void FlushByPinging(ref List<PingWhenItIsTimeState> pingable)
    {
        for (int i = 0; i < pingable.Count; i++)
        {
            try
            {
                pingable[i].m_linked.DoTheCallback();
            }
            catch (Exception e) {

                UnityEngine.Debug.LogWarning("Exception to deal with later when time" + e.StackTrace);
            }

        }
        pingable.Clear();
    }

    private void SetReadyToBePing(PingWhenItIsTimeState pingWhenItIsTimeState)
    {
        pingWhenItIsTimeState.m_linked.RequestTheCallbackAsap();
    }

    private void PingLoop(LoopPingStateOver1Second linked)
    {
        linked.m_linked.PingTimeThread();
    }
    private void PingLoop(LoopPingStateUnder1Second linked)

    {
        linked.m_linked.PingTimeThread();
    }

   
    private void PingAsFound(PingWhenItIsTimeState pingWhenItIsTimeState)
    {
        pingWhenItIsTimeState.DoTheCallback();
    }

    public void AddFromNow(double millisecondsFromNow, Action callback, PingThreadType threadType)
    {
        Add(new PingWhenItisTime(DateTime.Now.AddMilliseconds(millisecondsFromNow), callback, threadType));
    }
    public void Add(DateTime whenToPingBack, Action callback, PingThreadType threadType)
    {


        Add(new PingWhenItisTime(whenToPingBack, callback, threadType));
    }


    public void Add(PingWhenItisTime tracker) {


        m_registered.Add(new PingWhenItIsTimeState( tracker));
    }

    public void SubscribeLoop(LoopPing looper)
    {
        if (looper.m_loopInMs < 1000)
            m_loopPingUnder1Seconds.Add(new LoopPingStateUnder1Second(looper));
        else
            m_loopPingOver1Seconds.Add(new LoopPingStateOver1Second(looper));
    }


    List<PingWhenItIsTimeState> m_registered = new List<PingWhenItIsTimeState>();
     List<PingWhenItIsTimeState> m_toPingAsapInThread = new List<PingWhenItIsTimeState>();
     List<PingWhenItIsTimeState> m_toPingAsapInUnityThread = new List<PingWhenItIsTimeState>();
     List<LoopPingStateUnder1Second> m_loopPingUnder1Seconds = new List<LoopPingStateUnder1Second>();
     List<LoopPingStateOver1Second> m_loopPingOver1Seconds = new List<LoopPingStateOver1Second>();


    [System.Serializable]
    public class PingWhenItIsTimeState
    {

        public bool m_isItTime = false;
        public bool m_wasItPingYet = false;
        public PingWhenItisTime m_linked;

        public PingWhenItIsTimeState(PingWhenItisTime link)
        {
            m_linked = link;
        }

        public void DoTheCallback()
        {
            m_linked.DoTheCallback();
        }

        public bool IsTimePast(DateTime currentTime)
        {
            return m_linked.IsTimePast( currentTime);
        }

        public bool IsTimeSetAsPast()
        {
            return m_isItTime;
        }

        public void SetAsTimePast(bool isitTime)
        {
            m_isItTime = isitTime;
        }
    }



    public class LoopPingState
    {
        public LoopCountSinceLastTime m_loopCount= new LoopCountSinceLastTime();
        public LoopPing m_linked;
        public void AddPing()
        {
            m_loopCount.AddPing(1);
        }
        public void GetPingCountAndFlush(out int count)
        {
            count = m_loopCount.GetCount();
            m_loopCount.Flush();
        }
    }

    public class LoopPingStateUnder1Second : LoopPingState
    {

        public uint m_previousModulo;
        public uint m_currentModulo;
        public LoopPingStateUnder1Second(LoopPing looper)
        {
            this.m_linked = looper;
        }

        public void SetNewModulo(uint milliSecondSecond, out bool hasReachCycle)
        {
            //IT don't take account if the thread is freezing 
            m_currentModulo = milliSecondSecond % m_linked.m_loopInMs;
            hasReachCycle= m_currentModulo < m_previousModulo;
            m_previousModulo = m_currentModulo;

        }
    }
    public class LoopPingStateOver1Second : LoopPingState
    {
        public double m_previousModulo;
        public double m_currentModulo;

        public LoopPingStateOver1Second(LoopPing looper)
        {
            this.m_linked = looper;
        }



        public void SetNewModulo(double timePastInMs, out bool hasReachCycle)
        {
            //IT don't take account if the thread is freezing 
            m_currentModulo = timePastInMs % m_linked.m_loopInMs;
            hasReachCycle = m_currentModulo < m_previousModulo;
            m_previousModulo = m_currentModulo;
        }
        
    }

    public class LoopCountSinceLastTime {
        public int m_count;
        public int GetCount() { return m_count; }
        public void Flush() { m_count = 0; }
        public void AddPing(int toAdd) { m_count += toAdd; }
    
    }

    public class LoopPing {

        public PingThreadType m_threadType;
        public uint m_loopInMs=10;
        public Action m_callBackInThread;
        public LoopPing(uint millisecondsOfLoop, PingThreadType threadType): this(millisecondsOfLoop, threadType, null)
        { }
        public LoopPing(float secondsOfLoop, PingThreadType threadType):this((uint)(1000f * secondsOfLoop), threadType)
        {}
     
        public LoopPing(uint millisecondsOfLoop, PingThreadType threadType, Action toDoCallback)
        {
            m_loopInMs = millisecondsOfLoop;
            m_threadType = threadType;
            m_callBackInThread = toDoCallback;
        }

        public void PingTimeThread()
        {
            if (m_callBackInThread != null)
                m_callBackInThread.Invoke();
        }

        public void SetCallBack(Action actionToDo)
        {
            m_callBackInThread = actionToDo;
        }
    }


    #region Make the thread work
    public Thread m_timeThread;
    public bool m_wantTheThreadAlive=false;

    public void StartTheTimer()
    {
        StartTheTimer(m_priorityLevel);
    }
    public void StartTheTimer(System.Threading.ThreadPriority priority)
    {

        StopTheTimer();
        m_wantTheThreadAlive = true;
        m_timeThread = new Thread(TimeLooper);
        m_timeThread.Priority = priority;
        m_timeThread.Start();

    }

    

    public void StopTheTimer() {
        m_wantTheThreadAlive = false;
        if (m_timeThread!=null && m_timeThread.IsAlive) {
            m_timeThread.Abort();
        }
    }

    private void OnDestroy()
    {
        StopTheTimer();
    }
    private void OnApplicationQuit()
    {
        StopTheTimer();
    }
    #endregion
}

public enum PingThreadType { InTimeThread, InUnityThread }
public class PingWhenItisTime {
   
    public PingThreadType m_threadType;
    public Action m_timeThreadCallBack;
    public DateTime m_whenToPingBack;
    public bool m_requestPingAsap;
    public bool m_hadBeenPing;
    public PingWhenItisTime(double whenFromNowInSecond, Action inTimeThreadCallback, PingThreadType threadType) : this(DateTime.Now.AddSeconds(whenFromNowInSecond), inTimeThreadCallback, threadType)
    { }

    public PingWhenItisTime(DateTime whenToPingBack, Action inTimeThreadCallback, PingThreadType threadType)
    {
        m_threadType = threadType;
        m_whenToPingBack = whenToPingBack;
        m_timeThreadCallBack = inTimeThreadCallback;
        m_hadBeenPing = false;

    }

    public void RequestTheCallbackAsap() {
        m_requestPingAsap=true;
    }
    public void DoTheCallback()
    {
        if (m_requestPingAsap && !m_hadBeenPing) { 
            if (m_timeThreadCallBack != null)
                m_timeThreadCallBack.Invoke();
            m_hadBeenPing = true;
        }
    }

    public bool IsTimePast(DateTime currentTime)
    {
        return currentTime > m_whenToPingBack;
    }
    public bool HadBeenPing() { return m_hadBeenPing; }
}
