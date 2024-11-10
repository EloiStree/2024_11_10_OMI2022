using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MorseDetectorDefault : MorseDetector {

    #region PUBLIC VARIABLE AND PROPERTY
    public OnMorseWithOrigineDetected m_onMorseChanged;
    [Header("Detector Params")]
    [Tooltip("Time before considering as finished writing morse value")]
    public float m_timeToBeConsiderAsCommit = 0.75f;

    [Tooltip("Time to be considered as long morse key")]
    public float m_timeToBeLongMorseKey = 0.3f;

    [Header("Script Specific Events")]
    public OnMorseKeyDetected m_onMorseKeyDetected;
    public OnMorseValueDetected m_onMorseValueDetected;
    public OnMorseDownState m_onMorseDownState;
    #endregion


    #region PROTECT FROM DESIGNER INJECTION
    public void OnValidate()
    {
        if (m_timeToBeConsiderAsCommit <= 0)
            m_timeToBeConsiderAsCommit = 0.1f;
        if (m_timeToBeLongMorseKey <= 0)
            m_timeToBeLongMorseKey = 0.1f;

        if (m_timeToBeLongMorseKey >= m_timeToBeConsiderAsCommit) {
            m_timeToBeConsiderAsCommit = m_timeToBeLongMorseKey + 0.5f;
        }
    }
    #endregion

    public void Reset()
    {
        MorseEmittorAbstract emittor = this.GetComponent<MorseEmittorAbstract>();
        if (emittor != null)
            m_morseSource = emittor;
    }


    #region PRIVATE VARIABLE AND PROPERTY
    [Header("Debug (Don't Touch)")]
    [SerializeField]//*/
    List<MorseKey> m_detectedKeys = new List<MorseKey>();
    [SerializeField]//*/
    bool m_isListeningNextKey;
    [SerializeField]//*/
    TimeCounter m_userPressingCounter = new TimeCounter();
    [SerializeField]//*/
    TimeCounter m_userWaitingCounter = new TimeCounter();
    [SerializeField]//*/
    TimeCounter m_timeSinceEditingMorseValue = new TimeCounter();
    [SerializeField]//*/
    StateChange<bool> m_pressingState = new StateChange<bool>();
    [SerializeField]//*/
    bool hasPressingStateChanged;
    #endregion


    #region LOGIC OF THE DETECTION

    public void Update()
    {
        m_pressingState.SetNewValue(IsEmittorActive(), out hasPressingStateChanged);
        AddTimeToCounters();

        if (IsWritingMorse())
        {
            SetAsListeningNextKey();
            if (HasFinishedToPressKey())
            {
                MorseKey newMorseKey = GetPreviousKeyDetected();
                AddNewMorseKey(newMorseKey);
                NotifyMorseInProgress(newMorseKey);
            }
            if (hasPressingStateChanged)
            {
                NotifyChangeInPressingState();
            }

        }
        else {
            if (HasMorseKeyDetected()) {
                NotifyMorseDetected();
            }
            if(IsListeningNextKey())
                ResetMorseDetection();
        }


        

        
    }
    #endregion
    #region Manipulation Methode
    private void SetAsListeningNextKey()
    {
        m_isListeningNextKey = true;
    }

    private void NotifyChangeInPressingState()
    {
        m_onMorseDownState.Invoke( m_pressingState.GetCurrentState());
    }

    private bool IsListeningNextKey()
    {
        return m_isListeningNextKey;
    }

    private void NotifyMorseDetected()
    {
        MorseValueWithOrigine morseLinked = GetCurrentMorseValueWithOrigine();
        m_onMorseDetected.Invoke(morseLinked);
    }

    

    private void NotifyMorseInProgress(MorseKey newMorseKey)
    {
        MorseValueWithOrigine morseLinked = GetCurrentMorseValueWithOrigine();
        m_onMorseKeyDetected.Invoke(newMorseKey, morseLinked);
        m_onMorseChanged.Invoke(morseLinked);
    }

    
    private void AddNewMorseKey(MorseKey newMorseKey)
    {
        m_detectedKeys.Add(newMorseKey);
    }

    private MorseKey GetPreviousKeyDetected()
    {
        return m_userPressingCounter.GetPreviousRecordedTime() > m_timeToBeLongMorseKey ? MorseKey.Long : MorseKey.Short;
    }

    private bool HasFinishedToPressKey()
    {
        return hasPressingStateChanged && m_pressingState.GetCurrentState() == false;
    }

    private void AddTimeToCounters()
    {
        // Add time
        float delta = Time.deltaTime;
        if (GetPressingState())
        {
            m_userPressingCounter.AddTime(delta);
            m_userWaitingCounter.SetTime(0);
        }
        else
        {
            m_userWaitingCounter.AddTime(delta);
            m_userPressingCounter.SetTime(0);
        }

        if (IsWritingMorse())
            m_timeSinceEditingMorseValue.AddTime(delta);
        else m_timeSinceEditingMorseValue.SetTime(0);

    }

    private void ResetMorseDetection()
    {
        m_userPressingCounter.Reset();
        m_userWaitingCounter.Reset();
        m_timeSinceEditingMorseValue.Reset();
        m_detectedKeys.Clear();

    }
    #endregion
    #region CAN BE PUBLIC METHODE
    public bool IsEmittorActive() { return base.m_morseSource.IsEmitting(); }

    public MorseValueWithOrigine GetCurrentMorseValueWithOrigine()
    {
        MorseValue valueDetected = new MorseValue(m_detectedKeys.ToArray());
        string morseName = m_morseSource.GetSourceName();
        MorseValueWithOrigine morseLinked = new MorseValueWithOrigine(m_morseSource, valueDetected);
        return morseLinked;
    }
    public MorseValue GetCurrentMorseValue()
    {
        return new MorseValue(m_detectedKeys.ToArray());
    }

    public bool IsWritingMorse()
    {
        return GetPressingState() || HasStateChangedRecently();
    }

    public bool GetPressingState()
    {
        return m_pressingState.GetCurrentState();
    }

    public bool HasStateChangedRecently()
    {
        return m_userWaitingCounter.GetCurrentTimeCount() < m_timeToBeConsiderAsCommit;
    }
    public bool HasMorseKeyDetected()
    {
        return m_detectedKeys.Count > 0;
    }


    #endregion
    #region CLASS DEFINITION
    [Serializable]
    public class OnMorseKeyDetected : UnityEvent<MorseKey, MorseValueWithOrigine> { };
    [Serializable]
    public class OnMorseValueDetected : UnityEvent<MorseValue> { };
    [Serializable]
    public class OnMorseDownState : UnityEvent<bool> { };

    [System.Serializable]
    public struct StateChange<T> where T : IComparable
    {
        [SerializeField]
        T m_previousValue;
        [SerializeField]
        T m_currentValue;
        public void SetNewValue(T value, out bool hasChanged)
        {
            hasChanged = (m_previousValue.CompareTo(m_currentValue) != 0);
            m_previousValue = m_currentValue;
            m_currentValue = value;
        }
        public T GetPreviousState() { return m_previousValue; }
        public T GetCurrentState() { return m_currentValue; }

    }
    [System.Serializable]
    public struct TimeCounter
    {
        public float m_time;
        public float m_lastTimeRecorded;
        public bool IsCountingTime() { return m_time <= 0f; }
        public float GetCurrentTimeCount() { return m_time; }
        public float GetPreviousRecordedTime()
        {
            return m_lastTimeRecorded;
        }
        public void AddTime(float time)
        {
            SetTime(m_time + time);
        }
        public void SetTime(float time)
        {
            if (time == 0f && m_time > 0f)
                m_lastTimeRecorded = m_time;
            m_time = time;
        }

        public void Reset()
        {
            m_time = 0;
            m_lastTimeRecorded = 0;
        }
    }



    #endregion
}
