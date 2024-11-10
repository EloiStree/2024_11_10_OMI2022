using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MorseStackToTextEvent : MonoBehaviour
{
    public MorseEventAsText m_onCorrespondingValidated;
    public MorseEventAsText m_onCorrespondingChange;
    public List<StackListener> m_listeners = new List<StackListener>();
    [System.Serializable]
    public class MorseEventAsText : UnityEvent<string> { }
    [System.Serializable]
    public class StackListener {  
        public string m_textEmitted;
        public MorseStack m_lookingFor;
    }
     [Header("Debug")]
    public StackListener m_lastCorresponding;
    public MorseStack m_lastChange;
    public MorseStack m_lastValidated;

    private void OnEnable()
    {
        MorseStacker.AddStackChangeListener( MorseStackChange);
        MorseStacker.AddStackValidatedListener(MorseStackValidated);
    }

    private void OnDisable()
    {
        MorseStacker.RemoveStackChangeListener( MorseStackChange);
        MorseStacker.RemoveStackValidatedListener(MorseStackValidated);
    }

    public void MorseStackValidated(MorseStack stack)
    {

        m_lastValidated = stack;
        CheckStack(stack, m_onCorrespondingValidated);
    }
    public void MorseStackChange(MorseStack stack)
    {
        m_lastChange = stack;
        CheckStack(stack, m_onCorrespondingChange);
     
    }

    private void CheckStack(MorseStack stack, MorseEventAsText morseEvent)
    {
        for (int i = 0; i < m_listeners.Count; i++)
        {
            if (MorseStacker.AreEquals(m_listeners[i].m_lookingFor, stack))
            {
                m_lastCorresponding = m_listeners[i];
                morseEvent.Invoke(m_listeners[i].m_textEmitted);
            }
        }
    }
}
