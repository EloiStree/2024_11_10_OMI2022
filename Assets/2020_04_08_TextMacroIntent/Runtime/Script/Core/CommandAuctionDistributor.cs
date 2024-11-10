using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandAuctionDistributor : MonoBehaviour
{
    [Header("Interpreters")]
    public List<AbstractInterpreterMono> m_monoInterpreters = new List<AbstractInterpreterMono>();
    public List<IInterpreter> m_interpreters = new List<IInterpreter>();

    [Header("Debug")]
    public string m_previousAuction = "";
    [TextArea(1,4)]
    public string m_takerJobDescription="";
    public string m_lastNoTakerCommand;

    public CommandLineInterpreterEvent m_interpreterFound;
    public CommandLineEvent            m_commandLineNotTakeInCharge;


    public void AddInterpreter(IInterpreter interpreter)
    {
        m_interpreters.Add(interpreter);
    }
    public void RemoveInterpreter(IInterpreter interpreter)
    {
        m_interpreters.Remove(interpreter);
    }
    private void Awake()
    {
        m_interpreters.Clear();
        CheckThatMonoInterpretorAreListened();
    }
    private void OnEnable()
    {
        
        CheckThatMonoInterpretorAreListened();
    }
    private void OnValidate()
    {
        CheckThatMonoInterpretorAreListened();

    }

    private void CheckThatMonoInterpretorAreListened()
    {
        for (int i = 0; i < m_monoInterpreters.Count; i++)
        {
            m_interpreters.Remove(m_monoInterpreters[i]);
            m_interpreters.Add(m_monoInterpreters[i]);
        }
    }

    public bool SeekForFirstTaker(string command, out IInterpreter taker)
    {
       return SeekForFirstTaker(new CommandLine(command), out taker);
    }
    public bool SeekForFirstTaker(ICommandLine command, out IInterpreter taker)
    {
        bool foundTaker = false;
        taker = null;
        for (int i = 0; i < m_interpreters.Count; i++)
        {
            if (m_interpreters[i].CanInterpreterUnderstand(ref command)) {
                
                foundTaker = true;
                taker = m_interpreters[i];
                m_previousAuction = command.GetLine();
                m_takerJobDescription = taker.WhatWillYouDoWith(ref command);
                m_interpreterFound.Invoke(taker, command);
                break;
            }
        }
        m_lastNoTakerCommand = command.GetLine();


        if (!foundTaker)
            m_commandLineNotTakeInCharge.Invoke( command);
        return foundTaker;
    }

}

[System.Serializable]
public class CommandLineEvent : UnityEvent<ICommandLine> { }
[System.Serializable]
public class CommandLineInterpreterEvent : UnityEvent<IInterpreter, ICommandLine> { }

