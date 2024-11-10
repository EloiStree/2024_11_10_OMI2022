using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Interpreter_Wait : AbstractInterpreterMono
{
    public CommandExecutionInformation m_executionQuote = new CommandExecutionInformation(true, false, false, true);
    public override string GetName()
    {
        return "Wait";
    }
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return command.GetLine().IndexOf("Wait ") == 0;

    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        if (!CanInterpreterUnderstand(ref command)) { 
            succedToExecute.SetAsFinished(false);
            return;
        }
        try
        {
            string cmd = command.GetLine();
            cmd = cmd.Substring("Wait ".Length).Trim();
            //bool inSecond = Regex.IsMatch(cmd, "\\d+s"); 
            float time = 0;
          
                time = float.Parse(cmd)/1000f;
            
            m_waitingCountDown.Add(new CountDown(time, succedToExecute));
        }
        catch (Exception) {
            succedToExecute.StopWithError("Did not succed to convert: " + command.GetLine());
        }
    }
    public List<CountDown> m_waitingCountDown = new List<CountDown>();
    public class CountDown {
        public CountDown(float timeLeft, ExecutionStatus callBackState) {

            m_timeLeft = timeLeft;
            m_state = callBackState;
        }
       private  ExecutionStatus m_state;
       private float m_timeLeft;
        
        public  void RemoveTimeAndTriggerWhenFinish(float time) {
            m_timeLeft -= time;
            if (m_state !=null && !m_state.HasFinish() &&  IsFinish())
                m_state.SetAsFinished(true);
        }
        public bool IsFinish() {
            return m_timeLeft <= 0f;
        }
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = m_executionQuote;
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "Command Will wait with Unity update for some given time to delay action";
    }

    [Header("Debug")]
    public int m_inProgress;
    void Update()
    {
        float dt = Time.deltaTime;
        for (int i = m_waitingCountDown.Count-1; i >=0 ; i--)
        {
            m_waitingCountDown[i].RemoveTimeAndTriggerWhenFinish(dt);
            if (m_waitingCountDown[i].IsFinish())
                m_waitingCountDown.RemoveAt(i);
        }
        m_inProgress = m_waitingCountDown.Count;
    }
}
