using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class BooleanStateRegisterMono : MonoBehaviour
{
    public BooleanStateRegister m_register = new BooleanStateRegister();
    public enum TimeUpdateType { Update, Thread}
    public TimeUpdateType m_timeUpdate = TimeUpdateType.Thread;
    public BooleanStateRegister GetRegister() { return m_register; }

    public void GetRegister(ref BooleanStateRegister registerRef)
    {
        registerRef= m_register;
    }

    public DateTime m_lastCheck;
    public DateTime m_now;
    private Thread m_thread;
    public void Update()
    {
        if (!(m_timeUpdate == TimeUpdateType.Update))
            return;
        UpdateTime();
    }

    private void UpdateTime()
    {
        m_now = DateTime.Now;
        float timePast = (float)(m_now - m_lastCheck).TotalSeconds;
        m_register.AddElapsedTimeToAll(timePast);

        m_lastCheck = m_now;
    }

    public void Switch(List<string> booleanNames)
    {
        foreach (var item in booleanNames)
        {
            m_register.SwitchValue(item);
        }
    }

    public void Awake()
    {
        m_thread = new Thread(UpdateTimeThread);
        m_thread.Start();
    }

    private void UpdateTimeThread()
    {
        while (true) {
            Thread.Sleep(10);
            UpdateTime();
        }
    }

    public void Set(string nameofboolean, bool value, bool createItIfNotFound)
    {
        if (nameofboolean == null
            || nameofboolean.Length == 0
            || nameofboolean.Trim().Length == 0)
            return;


        if (!m_register.Contains(nameofboolean))
        {
            if (createItIfNotFound)
                m_register.Set(nameofboolean, value);
        }
        else { m_register.Set(nameofboolean, value); }
    }
    public bool Contain(string nameofboolean)
    {
        return m_register.Contains(nameofboolean);
    }
    public void Get(string nameofboolean, out bool containt, out bool value)
    {
        containt = Contain(nameofboolean);
        if(containt)
            value = m_register.GetStateOf(nameofboolean).GetValue();
        else
            value = false; ;
    }

    public void OnDestroy()
    {
        if(m_thread!=null)
            m_thread.Abort();
    }

    public void Set(bool value, List<string> names)
    {
        foreach (string item in names)
        {
            Set(item,value,true);
        }
    }
}
public class BooleanStateRegister
{

    private Dictionary<string, BooleanState> m_stored = new Dictionary<string, BooleanState>();
    public List<BooleanState> m_valuesRef= new List<BooleanState>();
    public List<string> m_keysRef = new List<string>();


    public void Set(string name, bool value) {
        name = name.ToLower();
        if (!m_stored.ContainsKey(name)) { 
            m_stored.Add(name, new BooleanState(name, value));
            m_keysRef.Clear();
            m_keysRef.AddRange(m_stored.Keys);
            m_valuesRef.Clear();
            m_valuesRef.AddRange(m_stored.Values);
        }
        else m_stored[name].SetValue(value);
   
    
    }
    public bool Contains(string name)
    {
        name = name.ToLower(); return m_stored.ContainsKey(name); }
    public bool GetValueOf(string name)
    {
        name = name.ToLower();
        return m_stored[name].GetValue();
    }
    public BooleanState GetStateOf(string name)
    {
        name = name.ToLower();
        return m_stored[name];
    }

    public void GetAll(out BooleanGroup group)
    {
        group = new BooleanGroup(GetAllKeys());
    }

    public string[] GetAllKeys() { return m_keysRef.ToArray(); }
    public BooleanState[] GetAllState() { return m_valuesRef.ToArray(); }

    public void AddElapsedTimeToAll(float timePast)
    {
        foreach (var item in GetAllState())
        {
            item.GetHistory().AddElapsedTime(timePast);
        }
    }

    public void GetAllState(ref List<BooleanState> stateRef)
    {
        stateRef = m_valuesRef;
    }
    public void GetAllKeys(ref List<string> stateRef)
    {
        stateRef = m_keysRef;
    }


    public void SwitchValue(string booleanName)
    {
        bool has = Contains(booleanName);
        if (has) { 
        bool value = GetValueOf(booleanName);
            Set(booleanName, !value);
        }
    }

    #region Listeners
    private BooleanChange m_newIndexName;
    public void AddNewIndexListener(BooleanChange changeListener)
    {
        m_newIndexName += changeListener;

    }
    public void RemoveNewIndexListener(BooleanChange changeListener)
    {
        m_newIndexName -= changeListener;

    }
    private void NotifyNewIndexChange(string booleanName, bool newValue)
    {
        if (m_newIndexName != null)
            m_newIndexName(booleanName, newValue);
    }

    private BooleanChange m_changeListeners;
    public delegate void BooleanChange(string booleanName, bool newValue);
    public void AddChangeListener(BooleanChange changeListener)
    {
        m_changeListeners += changeListener;

    }
    public void RemoveChangeListener(BooleanChange changeListener)
    {
        m_changeListeners -= changeListener;

    }
    private void NotifyBooleanChange(string booleanName, bool newValue) {
        if(m_changeListeners!=null)
            m_changeListeners(booleanName, newValue);
    }
    #endregion
}
public class BooleanState {
    string m_name="";
    BoolHistory m_boolState;

    public BooleanState(string name, bool startState)
    {
        m_boolState = new BoolHistory(startState);
        name = name.ToLower();
        m_name = name;
    }
    public void SetValue(bool value) {
        m_boolState.SetState(value);
    }
    public bool GetValue() {
        return m_boolState.GetState();
    }
    public bool WasSetTrue(float time,bool countCurrentState) {

        return m_boolState.WasSetTrue(time,  countCurrentState);
    }
    public bool WasSetFalse(float time, bool countCurrentState)
    {
        return m_boolState.WasSetFalse(time,  countCurrentState);
    }

    public BoolHistory GetHistory() { return m_boolState; }

    public string GetName()
    {
        return m_name;
    }
}
