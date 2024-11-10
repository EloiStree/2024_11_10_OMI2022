using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CmdConvert_Wait : CmdConvertAbstract
{
    public int m_numberOfCountDown;
    public List<WaitCountDown> m_waiting =new List<WaitCountDown>();
    public class WaitCountDown {
        public float m_timeLeft;
        public FinishChecker m_finishChecker;
        public SuccessChecker m_succesChecker;

        public WaitCountDown(float timeInSecond, SuccessChecker hasBeenConverted, FinishChecker hasFinish)
        {
            m_timeLeft = timeInSecond;
            m_finishChecker = hasFinish;
            m_succesChecker = hasBeenConverted;
        }
    }
    public override bool CanTakeResponsability(string command)
    {
        return Regex.IsMatch(command.ToLower(), "wait\\s.*");
    }

    public override void DoTheThing(string command, SuccessChecker hasBeenConverted, FinishChecker hasFinish)
    {
        if (!CanTakeResponsability(command))
        {
            hasBeenConverted.SetAsFail("Do not respect the format accepted");
            hasFinish.SetAsFinished();
            return;
        }
       
        int number = 0;
        command = command.ToLower().Replace("wait ", "");
        if (int.TryParse(command, out number)) { 
            m_waiting.Add(new WaitCountDown(((float)number)/1000f, hasBeenConverted, hasFinish));        
        }
    }

    
    void Update()
    {
        m_numberOfCountDown = m_waiting.Count;
        float timePast = Time.deltaTime;
        for (int i = m_waiting.Count-1; i >= 0; i--)
        {
            m_waiting[i].m_timeLeft -= timePast;
            if (m_waiting[i].m_timeLeft <= 0f) {
                m_waiting[i].m_succesChecker.SetAsDone();
                m_waiting[i].m_finishChecker.SetAsFinished();
                m_waiting.RemoveAt(i);
            }
        }
    }
}
