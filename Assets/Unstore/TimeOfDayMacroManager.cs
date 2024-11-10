using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayMacroManager : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public TimedOfDayMacroRegister m_timeRegister;
    public string m_debugTime;
    public DateTime previous;
    public DateTime current;

    public int m_hour;
    public int m_minute;
    public int m_second;

    public CommandLineEvent m_emitted;

    public void RefreshAndCheckTime() {
        current = DateTime.Now;

        bool isSecondPasted = previous.Second != current.Second;
        bool isMinutePasted = previous.Minute != current.Minute;
        bool isHourPasted = previous.Hour != current.Hour;
        m_hour  = current.Hour;
        m_minute= current.Minute;
        m_second= current.Second;

        if (isHourPasted)
        {
            if (m_hour % 6 == 0)
                Trigger(TimeOfDayFrequence._6Hours, m_timeRegister);
            if (m_hour % 4 == 0)
                Trigger(TimeOfDayFrequence._4Hours, m_timeRegister);
            if (m_hour % 3 == 0)
                Trigger(TimeOfDayFrequence._3Hours, m_timeRegister);
            if (m_hour % 2 == 0)
                Trigger(TimeOfDayFrequence._2Hours, m_timeRegister);
            Trigger(TimeOfDayFrequence._1Hour, m_timeRegister);
        }


        if (isMinutePasted)
        {
            if (m_minute % 30 == 0)
                Trigger(TimeOfDayFrequence._30Minutes, m_timeRegister);
            if (m_minute % 20 == 0)
                Trigger(TimeOfDayFrequence._20Minutes, m_timeRegister);
            if (m_minute % 15 == 0)
                Trigger(TimeOfDayFrequence._15Minutes, m_timeRegister);
            if (m_minute % 10 == 0)
                Trigger(TimeOfDayFrequence._10Minutes, m_timeRegister);
            if (m_minute % 5 == 0)
                Trigger(TimeOfDayFrequence._5Minutes, m_timeRegister);
            if (m_minute % 4 == 0)
                Trigger(TimeOfDayFrequence._4Minutes, m_timeRegister);
            if (m_minute % 3 == 0)
                Trigger(TimeOfDayFrequence._3Minutes, m_timeRegister);
            if (m_minute % 2 == 0)
                Trigger(TimeOfDayFrequence._2Minutes, m_timeRegister);
            Trigger(TimeOfDayFrequence._1Minute, m_timeRegister);

        }

        if (isSecondPasted)
        {

            if (m_second % 30 == 0)
                Trigger(TimeOfDayFrequence._30Seconds, m_timeRegister);
            if (m_second % 20 == 0)
                Trigger(TimeOfDayFrequence._20Seconds, m_timeRegister);
            if (m_second % 15 == 0)
                Trigger(TimeOfDayFrequence._15Seconds, m_timeRegister);
            if (m_second % 10 == 0)
                Trigger(TimeOfDayFrequence._10Seconds, m_timeRegister);
            if (m_second % 5 == 0)
                Trigger(TimeOfDayFrequence._5Seconds, m_timeRegister);
            if (m_second % 4 == 0)
                Trigger(TimeOfDayFrequence._4Seconds, m_timeRegister);
            if (m_second % 3 == 0)
                Trigger(TimeOfDayFrequence._3Seconds, m_timeRegister);
            if (m_second % 2 == 0)
                Trigger(TimeOfDayFrequence._2Seconds, m_timeRegister);
            Trigger(TimeOfDayFrequence._1Second, m_timeRegister);
        }


        List<ConditionWaitingCommands> foundMacro;
        m_timeRegister.GetMacroBetween(previous, current, out foundMacro);
        if (foundMacro.Count > 0)
        {
            Debug.Log("TTTT");
            Push(foundMacro);
        }

        //Debug.Log("ddd");
        previous = current;
        m_debugTime = current.ToString();
    }

    private void Trigger(TimeOfDayFrequence frequence, TimedOfDayMacroRegister timeRegister)
    {
       List<ConditionWaitingCommands> cmds = timeRegister.GetCommandsOf(frequence);
       Push(cmds);
    }

    private void Push(List<ConditionWaitingCommands> cmds)
    {

        BooleanStateRegister reg = m_register.m_register;
        for (int i = 0; i < cmds.Count; i++)
        {
            if(cmds[i].m_conditionToBeAllow.IsBooleansRegistered(reg) &&
                cmds[i].m_conditionToBeAllow.IsConditionValide(reg) )
                for (int j = 0; j < cmds[i].m_commands.Count; j++)
                {
                        
                    Debug.Log("TTDD:"+ cmds[i].m_commands[j]);
                    m_emitted.Invoke(cmds[i].m_commands[j]);
                }
        }
    }
}


   //_1Second,
   //_2Seconds
   //_3Seconds
   //_4Seconds
   //_5Seconds
   //_10Seconds
   //_15Seconds
   //_20Seconds
   //_30Seconds

    //_1Minute,
    //_2Minutes,
    //_3Minutes,
    //_4Minutes,
    //_5Minutes,
    //_10Minutes,
    //_15Minutes,
    //_20Minutes,
    //_30Minutes,

//_1Hour,
//_2Hours,
//_3Hours,
//_4Hours,
//_6Hours,