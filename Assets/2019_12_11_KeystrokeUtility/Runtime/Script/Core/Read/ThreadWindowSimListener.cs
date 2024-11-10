using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using WindowsInput.Native;
using System.Threading;
using WindowsInput;
using System.Security.Policy;

public class ThreadWindowSimListener : KeyboardReadMono, IKeyboardRead
{
    [SerializeField] ThreadLink m_threadLink;
    [SerializeField] EnumBoolHistory<KeyboardTouch> m_touchHistory = new EnumBoolHistory<KeyboardTouch>();
    [SerializeField] List<KeyboardTouch> m_touchActive = new List<KeyboardTouch>();
    [SerializeField] List<VirtualKeyCode> m_winKey = new List<VirtualKeyCode>();
     
    
    //Attempt to fix multithreading.
    List<KeyboardTouch> m_touchActiveUnityThread = new List<KeyboardTouch>();
    List<VirtualKeyCode> m_winKeyUnityThread = new List<VirtualKeyCode>();



    public List<KeyboardTouch> GetTouchActive() { return m_touchActiveUnityThread; }
    public List<VirtualKeyCode> GetWindowKey() { return m_winKeyUnityThread;  }

    public float m_debugTimeInSecond = 0.1f;
    public float m_unityDeltaTime;
    public int m_unityFps;
    [System.Serializable]
    public class ThreadLink {
        public Thread m_thread;
        public bool m_killWhenPossible;
        public int m_timeBetweenCheck = 25;
        public long m_threadFrame = 0;
        public float m_deltaTime = 0;
        public int m_tickPerSecond;
        public System.Threading.ThreadPriority m_threadPriority = System.Threading.ThreadPriority.AboveNormal;
    }

    public void Awake()
    {
        InvokeRepeating("RefreshDebug", 0, m_debugTimeInSecond);
       
        m_unityDeltaTime = Time.deltaTime;
        m_unityFps = (int)(1f / m_unityDeltaTime);
    }
    public static object lockObject = new object();  
    public void RefreshDebug() {
        lock (lockObject) { 
        
            m_touchActive = m_touchHistory.GetActiveElements();
            m_touchActiveUnityThread = m_touchActive.ToList();
            m_winKeyUnityThread = m_winKey.ToList();
        }
    }

    public void OnEnable()
    {
        if (m_threadLink.m_thread != null)
            m_threadLink.m_thread.Abort();
        m_threadLink.m_thread = new Thread(CheckWindowState);
        m_threadLink.m_thread.Priority = m_threadLink.m_threadPriority;
        m_threadLink.m_thread.Start();
    }


    public object m_readKeyLocker = new object();

    private void CheckWindowState()
    {
        lock (m_readKeyLocker) { 
            KeyboardTouch [] kts= new KeyboardTouch[0];
            bool isConvertable;
            InputSimulator winKey = new InputSimulator(); 
            List<VirtualKeyCode> vkList = KeystrokeUtility.GetEnumList<VirtualKeyCode>();
            List<VirtualKeyCode> notConvertable = KeystrokeUtility.GetEnumList<VirtualKeyCode>();
            DateTime now = DateTime.Now;
            DateTime lastCheck = DateTime.Now;
        
            while (m_threadLink.m_killWhenPossible == false) {
         
                            m_winKey.Clear();
                for (int i = 0; i < vkList.Count; i++)
                {

                    KeyBindingTable.ConvertWindowVirtualKeyCodesToTouch(vkList[i], out kts, out isConvertable);
               
                    for (int j = 0; j < kts.Length; j++)
                    {
                        if (isConvertable)
                        {
                            bool value = winKey.InputDeviceState.IsHardwareKeyDown(vkList[i]);
                            if(value)
                                m_winKey.Add(vkList[i]);
                       
                            m_touchHistory.SetState(kts[j],value);
                        
                        }
                    }

                }
                now = DateTime.Now;
                float dt = (float)(now - lastCheck).TotalSeconds;
                m_threadLink.m_deltaTime = dt;
                if(dt!=0f)
                m_threadLink.m_tickPerSecond = (int)(1f /  dt);
                lastCheck = now;
                m_touchHistory.AddTimeToAll((float)m_threadLink.m_deltaTime);
           

                if (m_threadLink.m_timeBetweenCheck < 1)
                    m_threadLink.m_timeBetweenCheck = 1;
                Thread.Sleep(m_threadLink.m_timeBetweenCheck);
                m_threadLink.m_threadFrame++;
            }
        }
    }

    public void OnDisable()
    {
        m_threadLink.m_killWhenPossible = true;
    }
    public void OnDestroy()
    {
        m_threadLink.m_killWhenPossible = true;
    }


    public override KeyboardTouch[] GetActiveTouches()
    {
        try
        {
            return m_touchHistory.GetActiveElements().ToArray();
        }

        catch (Exception e) {


            NotNowException.Ping(e);
            return new KeyboardTouch[0];
        }
    }

    public override BoolHistory GetTouchHistory(KeyboardTouch keyboardTouch)
    {
        return m_touchHistory.GetHistory(keyboardTouch);
    }

    public override bool IsAltActive()
    {
        return IsTouchActive(KeyboardTouch.Alt);
    }

    public override bool IsAltGrActive()
    {
        return IsTouchActive(KeyboardTouch.AltGr);
    }

    public override bool IsCapsLockOn()
    {
        return IsTouchActive(KeyboardTouch.CapsLock);
    }

    public override bool IsControlActive()
    {
        return IsTouchActive(KeyboardTouch.Control);
    }

    public override bool IsMetaActive()
    {
        return IsTouchActive(KeyboardTouch.Meta);
    }

    public override bool IsNumLockOn()
    {
        return IsTouchActive(KeyboardTouch.NumLock);
    }

    public override bool IsScrollLockOn()
    {
        return IsTouchActive(KeyboardTouch.ScrollLock);
    }

    public override bool IsShiftActive()
    {
        return IsTouchActive(KeyboardTouch.Shift);
    }

    public override bool IsTouchActive(KeyboardTouch keyboardTouch)
    {
        return m_touchHistory.GetState(keyboardTouch);
    }
}




[System.Serializable]
public class EnumBoolHistory<K> where K : struct, IConvertible 
{
    public List<K> m_activeElements = new List<K>();
    public Dictionary<K, BoolHistory> m_elementStates = new Dictionary<K, BoolHistory>();
    public delegate void OnStateChange(K element, bool isOn);
    public OnStateChange onStateChange;

    public EnumBoolHistory()
    {
        List<K> values = GetEnumList<K>();
        for (int i = 0; i < values.Count; i++)
        {
            if (!m_elementStates.ContainsKey(values[i]))
                m_elementStates.Add(values[i], new BoolHistory(false));
        }
    }
    public static List<G> GetEnumList<G>() where G : struct, IConvertible
    {
        return Enum.GetValues(typeof(G)).OfType<G>().ToList();
    }

    public void SetState(K element, bool value)
    {
        CheckExisting(element);
        if (value && m_elementStates[element].GetState() == false)
        {
            m_activeElements.Add(element);
            m_elementStates[element].SetState( true);
            if (onStateChange != null)
                onStateChange(element, true);
        }
        else if (!value && m_elementStates[element].GetState() == true)
        {
            m_activeElements.Remove(element);
            m_elementStates[element].SetState(false);
            if (onStateChange != null)
                onStateChange(element, false);
        }

    }

    private void CheckExisting(K element)
    {
        if (!m_elementStates.ContainsKey(element))
        {
            m_elementStates.Add(element,  new BoolHistory(false));
        }
    }

    public bool GetState(K element)
    {
        CheckExisting(element);
        return m_elementStates[element].GetState();
    }
    public BoolHistory GetHistory(K element)
    {
        CheckExisting(element);
        return m_elementStates[element];
    }
    public List<K> GetActiveElements()
    {
        return m_activeElements;
    }

    public void Clear()
    {
        m_elementStates.Clear();
        m_activeElements.Clear();
    }

    public void AddTimeToAll(float timeToAdd)
    {
        foreach (var item in m_elementStates.Values.ToList())
        {
            item.AddElapsedTime(timeToAdd);
        }
    }
}
