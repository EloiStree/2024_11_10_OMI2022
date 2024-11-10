
using System;
using System.Collections.Generic;
using UnityEngine;

public class LinearBooleanStateMachine {
    public BooleanStateRegister m_register;
    public BooleanGroup m_allListen = null;
    public List<StepAndActions> m_steps= new List<StepAndActions>();
    public LinearBooleanStateMachine(BooleanStateRegister register, IEnumerable<StepAndActions> steps)
    {
        m_register = register;
        m_steps.AddRange(steps);
    }

    public string GetPerLineDescription()
    {
        string description = string.Format("Steps:{0}", GetStepsCount());
        foreach (StepAndActions s in m_steps)
        {
            description += "\nS:" + s.GetCondition().ToString();
            foreach (BooleanStateAction a in s.GetActions())
            {
                description += "\n  A:" + a.ToString();
            }
        }
        
        return description;

    }
    public StepAndActions GetStep(int index)
    {
        if (index >= m_steps.Count)
            return null;
        return m_steps[index];
    }
    public int GetStepsCount()
    {
        return m_steps.Count;
    }

    public bool HasNextState(int index)
    {
        return m_steps.Count > index+1;
    }

    public void AddState(StepAndActions state) { m_steps.Add(state); }
    public bool IsStateConditionValide( int index) {
        StepAndActions step = GetStep(index);
        if (step == null) return false;
        return GetStep(index).GetCondition().IsConditionValide(m_register);
    }

    
}



[System.Serializable]
public class LinearBSMIterator {

    public int m_stepIndex;
    public double m_cooldown = 3f;
   
    public DateTime m_lastStateChange;
    public DateTime m_lastFullValidation;

    private LinearBooleanStateMachine m_linearBooleanStateMachine;

    public LinearBSMIterator(LinearBooleanStateMachine linearBooleanStateMachine)
    {
        this.m_linearBooleanStateMachine = linearBooleanStateMachine;
    }
    public void ResetToReuse() {
        m_stepIndex = 0;
    }
  
    public int GetIndex() { return m_stepIndex; }
    public void SetStateIndex(int index) 
    { 
        m_stepIndex = Mathf.Clamp(m_stepIndex, 0, m_linearBooleanStateMachine.GetStepsCount() - 1); 
    }
    public StepAndActions GetStep()
    {
        return GetStep(m_stepIndex);
    }
    public StepAndActions GetStep(int index)
    {
        if (HasState(index))
        {
            return m_linearBooleanStateMachine.GetStep(index);
        }
        return null;
    }
    public bool HasNextState()
    {
        return HasNextState(m_stepIndex);
    }
    public bool HasNextState(int index)
    {
        return HasState(index + 1);
    }
    public bool HasState(int index)
    {
        if (index < 0) 
            return false;
        return index < m_linearBooleanStateMachine.GetStepsCount() ;
    }
    public void GoNextState()
    {
        
        DateTime now = DateTime.Now; 
        if (IsLastState())
        {
            m_lastFullValidation = now;
        }
        if (IsFinish()) {       
            m_lastStateChange = now;
        }
        m_stepIndex++;
    }

    public bool IsFinish()
    {
        return m_stepIndex >= m_linearBooleanStateMachine.GetStepsCount();
    }

    public bool IsLastState()
    {
        return m_stepIndex == m_linearBooleanStateMachine.GetStepsCount() - 1;
    }

    public bool IsStateConditionValide()
    {
        return m_linearBooleanStateMachine.IsStateConditionValide(m_stepIndex);
    }
}

public class StepAndActions {
    BooleanStateStep m_validity;
    List<BooleanStateAction> m_actions;
    List<Modificator> m_modificators;

    public StepAndActions(BooleanStateStep validity, List<BooleanStateAction> actions, List<Modificator> modificators)
    {
        m_validity = validity;
        m_actions = actions;
        m_modificators = modificators;
    }

    public BooleanStateStep GetCondition()
    {
        return m_validity;
    }
    public BooleanStateAction[] GetActions()
    {
        return m_actions.ToArray();
    }
    public Modificator[] GetModificators()
    {
        return m_modificators.ToArray();
    }

}

public abstract class BooleanStateStep
{
    public abstract bool IsConditionValide(BooleanStateRegister useSpecificRegister);


}
