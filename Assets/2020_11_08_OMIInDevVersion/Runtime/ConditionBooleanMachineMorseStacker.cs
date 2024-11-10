using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;

public class ConditionBooleanMachineMorseStacker : MonoBehaviour
{

    public TimeThreadMono m_timer;
    public BooleanStateRegisterMono m_register;
    public float m_defaultLongTime = 0.15f;
    public float m_defaultExistTime = 0.5f;

    public List<MorseToStringCommand> m_conditionToCheck = new List<MorseToStringCommand>();
    public UnityStringEvent m_commandFound;


     [System.Serializable]
    public class ActionFoundEvent : UnityEvent<string>{}

    public void Start()
    {
        MorseStacker.AddStackChangeListener(CheckOnChanged);
        MorseStacker.AddStackValidatedListener(CheckOnValidate);
        Clear();
        m_timer.SubscribeLoop(new TimeThreadMono.LoopPing(25, PingThreadType.InUnityThread, CheckConditionAndRefreshTimer));
    }
    

    private void CheckOnValidate(MorseStack stackCopyInfo)
    {
        MorseToStringCommand morseToValidate = GetMorseFor(stackCopyInfo.GetIdName());
        if (morseToValidate != null && morseToValidate.m_triggerOnExit
            && morseToValidate.m_morseToTrigger == stackCopyInfo.GetMorseValue())
            Trigger(morseToValidate);
    }

  

    private void CheckOnChanged(MorseStack stackCopyInfo)
    {
        MorseToStringCommand morseToValidate = GetMorseFor(stackCopyInfo.GetIdName());
        if (morseToValidate!=null && morseToValidate.m_triggerOnFound 
            && morseToValidate.m_morseToTrigger == stackCopyInfo.GetMorseValue())
            Trigger(morseToValidate);
    }
    private MorseToStringCommand GetMorseFor(string nameId)
    {
        for (int i = 0; i < m_conditionToCheck.Count; i++)
        {
            if (m_conditionToCheck[i].m_uniqueName == nameId)
                return m_conditionToCheck[i];
        }
        return null;
    }
    private void Trigger(MorseToStringCommand morseToValidate)
    {
        if (morseToValidate == null)
            return;
        m_commandFound.Invoke(morseToValidate.m_stringCommand);
}

    public void Clear() {
        m_conditionToCheck.Clear();
    }

   
    public void AddMorseToListen(MorseToStringCommand morseInfo)
    {
        m_conditionToCheck.Add(morseInfo);
    }

   
   
    public void CheckConditionAndRefreshTimer()
    {
        RefreshTimePastToCheckForMorse();
        //COULD BE LARGELY OPTIMISED !!
        m_currentRefresh = DateTime.Now;
        BooleanStateRegister register=null;
        m_register.GetRegister(ref register);

        double delta = (m_currentRefresh - m_previousRefresh).TotalSeconds;

        for (int i = 0; i < m_conditionToCheck.Count; i++)
        {

            bool hasChange = false;
            if (m_conditionToCheck[i].m_conditionToTrigger.IsBooleansRegistered(register))
            { 
                bool conditionState = m_conditionToCheck[i].m_conditionToTrigger.IsConditionValide(register);
                m_conditionToCheck[i].m_boolSwitch.SetValue(conditionState, out hasChange);

                bool isDown = hasChange && conditionState;
                bool isUp = hasChange && !conditionState;

                if (isDown)
                {
                    m_conditionToCheck[i].m_currentPressingTime = 0;
                }
                else if (isUp)
                {
                    MorseStacker.StackOn(m_conditionToCheck[i].m_uniqueName,
                       m_conditionToCheck[i].m_currentPressingTime < m_conditionToCheck[i].m_longTimerInSecond ? MorseKey.Short : MorseKey.Long, m_conditionToCheck[i].m_exitTimerInSecond);
                }
                else if (conditionState)
                {

                    m_conditionToCheck[i].m_currentPressingTime += (float)delta;
                }
            }
        }
         m_previousRefresh= m_currentRefresh;
    }
    DateTime m_currentRefresh;
    DateTime m_previousRefresh;

    public void RefreshTimePastToCheckForMorse()
    {
        MorseStacker.CheckAutoValidationWithRealTimepass();
    }


 
}

[System.Serializable]
public class MorseToStringCommand {
    public string m_uniqueName;
    public ClassicBoolState m_conditionToTrigger;
    public string m_stringCommand;
    public MorseValue m_morseToTrigger;
    public float m_longTimerInSecond=800;
    public float m_exitTimerInSecond= 1600;
    public bool m_triggerOnFound=false;
    public bool m_triggerOnExit=true;

    public float m_currentPressingTime;
    public BooleanSwitchListener m_boolSwitch= new BooleanSwitchListener(false);

    public MorseToStringCommand(string uniqueName, ClassicBoolState found, MorseValue morse, string stringCommand)
    {
        m_uniqueName = uniqueName;
        this.m_conditionToTrigger = found;
        this.m_morseToTrigger = morse;
        this.m_stringCommand = stringCommand;
    }
}