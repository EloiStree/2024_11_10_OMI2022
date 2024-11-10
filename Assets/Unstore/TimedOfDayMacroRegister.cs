using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedOfDayMacroRegister : MonoBehaviour
{
  //  public BooleanStateRegisterMono m_register;
    public Dictionary<TimeOfDayFrequence, List<ConditionWaitingCommands>> m_frequences= new Dictionary<TimeOfDayFrequence, List<ConditionWaitingCommands>>();
    public List<TimeOfTheDayWithCommandLines> m_recordTime = new List<TimeOfTheDayWithCommandLines>();

    public int m_debugAt;
    public int m_debugFrequence;


    internal void Clear()
    {
        m_frequences.Clear();
        m_recordTime.Clear();
    }

   
    public void AddAt(int hour, int minute, int second, int millisecond, string condition, params string[] cmds)
    {
        //Debug.Log(string.Format("TAt:  {0} {1} {2} {3} ...", hour, minute, second, millisecond));
        m_recordTime.Add(new TimeOfTheDayWithCommandLines(hour, minute, second, millisecond, condition, cmds  )  );
        m_debugAt = m_recordTime.Count;
    }
    public void AddFrequence(TimeOfDayFrequence frequence,string condition, params string[] cmd)
    {
       // Debug.Log(string.Format("TE:  {0}", frequence));
        if (!m_frequences.ContainsKey(frequence))
            m_frequences.Add(frequence, new List<ConditionWaitingCommands>());
        for (int i = 0; i < cmd.Length; i++)
        {
            m_frequences[frequence].Add(new ConditionWaitingCommands(condition, cmd[i]));
        }
        m_debugFrequence = m_frequences.Keys.Count;
    }

    public List<ConditionWaitingCommands> GetCommandsOf(TimeOfDayFrequence frequence)
    {
        if (!m_frequences.ContainsKey(frequence))
            m_frequences.Add(frequence, new List<ConditionWaitingCommands>());
        return m_frequences[frequence];
    }

    public void GetMacroBetween(DateTime previous, DateTime current, out List<ConditionWaitingCommands> cmds)
    {
        cmds = new List<ConditionWaitingCommands>();
        TimeSpan tprevious= previous.TimeOfDay, tcurrrent =current.TimeOfDay;
        //if midnight
        if (previous.Day != current.Day)
        {
            TimeSpan end = new TimeSpan(0, 23, 59, 59, 999);
            TimeSpan start = new TimeSpan(0, 0,0,0,0);
            List<ConditionWaitingCommands> cls = m_recordTime.Where(k => k.m_time.GetTimeAsTimeSpan() > tprevious
            && k.m_time.GetTimeAsTimeSpan() <= end).Select(k => k.m_commands).ToList();
            cmds.AddRange(cls);


            cls.Clear();
            cls = m_recordTime.Where(k => k.m_time.GetTimeAsTimeSpan() > start
            && k.m_time.GetTimeAsTimeSpan() <= tcurrrent).Select(k => k.m_commands).ToList();
            cmds.AddRange(cls);
        }
        else {
            List<ConditionWaitingCommands> cls = m_recordTime.Where(k => k.m_time.GetTimeAsTimeSpan() > tprevious
            && k.m_time.GetTimeAsTimeSpan() <= tcurrrent).Select(k => k.m_commands).ToList();
            cmds.AddRange(cls);

        }

        
    }
}
[System.Serializable]
public class TimeOfTheDayBean
{
    public int m_hours;
    public int m_minutes;
    public int m_seconds;
    public int m_milliseconds;
    public TimeSpan m_time;

    public TimeOfTheDayBean(int hour, int minute, int second, int millisecond)
    {
        this.m_hours = hour;
        this.m_minutes = minute;
        this.m_seconds = second;
        this.m_milliseconds = millisecond;
        m_time = new TimeSpan(0, hour, minute, second, millisecond);
    }
    public TimeSpan GetTimeAsTimeSpan() { 
        return m_time; 
    }
}
[System.Serializable]
public class TimeOfTheDayWithCommandLines
{
    public TimeOfTheDayWithCommandLines(int hour, int minute, int second, int millisecond,string condition, params string[] cmds)
    {
        m_time = new TimeOfTheDayBean(hour, minute, second, millisecond);
        m_commands = new ConditionWaitingCommands(condition, cmds);
    }
    public TimeOfTheDayBean m_time;
    public ConditionWaitingCommands m_commands;
}

public enum TimeOfDayFrequence
{
     _1Second,
     _2Seconds,
     _3Seconds,
     _4Seconds,
     _5Seconds,
    _10Seconds,
    _15Seconds,
    _20Seconds,
    _30Seconds,
    _1Minute,
    _2Minutes,
    _3Minutes,
    _4Minutes,
    _5Minutes,
    _10Minutes,
    _15Minutes,
    _20Minutes,
    _30Minutes,
    _1Hour,
    _2Hours,
    _3Hours,
    _4Hours,
    _6Hours,

}
[System.Serializable]
public class ConditionWaitingCommands
{
    public ConditionWaitingCommands(string condition, params string[] cmds)
    {
        TextToBoolStateMachineParser.IsClassicParse(condition, out m_conditionToBeAllow);
        for (int i = 0; i < cmds.Length; i++)
        {
            m_commands.Add(new CommandLine(cmds[i]));
        }
    }
    public ClassicBoolState m_conditionToBeAllow;
    public List<CommandLine> m_commands = new List<CommandLine>();

}