using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Macro_TriggerAtGivenTimeMono : MonoBehaviour
{

    public List<CommandLinkedToTime> m_registered = new List<CommandLinkedToTime>();
    public CommandLineEvent m_emitted = new CommandLineEvent();
    [TextArea(1,5)]
    public string m_debugNextInTime;
    public int m_waitingCount;

    public class CommandLinkedToTime {
         DateTime m_executionTime;
         CommandLine m_commandLine;

        public CommandLinkedToTime(DateTime time, CommandLine command) {
            m_executionTime = time;
            m_commandLine = command;
        }
        public double GetTimeLeft(DateTime now)
        {
            return (m_executionTime - now).TotalSeconds;
        }

        public CommandLine GetCommand()
        {
            return m_commandLine;
        }
    }

    internal void StopInWaitingCommand()
    {
        m_registered.Clear();
    }

    private IEnumerator EmitIndependantly(CommandLine cmd) {

        m_emitted.Invoke(cmd);
        yield break;
    }
    void Update()
    {
        DateTime now = DateTime.Now;
        m_registered = m_registered.OrderBy(k => k.GetTimeLeft(now)).ToList();
        for (int i = m_registered.Count - 1; i >= 0; i--)
        {
            if (m_registered[i].GetTimeLeft(now) <= 0f)
            {
                // Should I emit the in coroutine ?
                StartCoroutine(EmitIndependantly(m_registered[i].GetCommand()));
                m_registered.RemoveAt(i);
            }

        }
        m_debugNextInTime = "";
        for (int i = 0; i < m_registered.Count; i++)
        {
            m_debugNextInTime +=string.Format("{0:0.00}>{1}\n" , m_registered[i].GetTimeLeft(now) , m_registered[i].GetCommand().GetLine());

        }
        m_waitingCount = m_registered.Count;
    }

    public void AddSpecificTime(DateTime time, CommandLine command)
    {
         m_registered.Add(new CommandLinkedToTime( time, command));
    }
    public void TriggerIn( CommandLine command, int millisecond , int second = 0, int minute = 0, int hour=0)
    {
        DateTime t = DateTime.Now;
        t.AddMilliseconds(millisecond);
        t.AddSeconds(second);
        t.AddMinutes(minute);
        t.AddHours(hour);
        AddSpecificTime(t, command);
    }

}
