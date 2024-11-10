using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class LinearBSMBuilder
{

    BooleanStateStep   previousStateFound = null;
    BooleanGroup       m_currentSetGroup;

    List<BooleanStateAction> actionsAssociated;
    List<StepAndActions> stepCreated;
    BooleanStateRegister     register;
    string m_history = "";
    int m_historyIndex;

    private LinearBSMBuilder() {
        Reset();
    }

    public void Reset() {
        previousStateFound = null;
        m_currentSetGroup= null;
        register = null;
        actionsAssociated = new List<BooleanStateAction>();
        stepCreated = new List<StepAndActions>();
        m_history = "";
        m_historyIndex = 0;
    }

    public LinearBSMBuilder Set(BooleanGroup group)
    {
        m_currentSetGroup = group;
        m_history += "\n"+ (m_historyIndex++)+":" + group;
        return this;
    }
    public LinearBSMBuilder Set(BooleanStateRegister reg)
    {
        register = reg;
        m_history += "\n" + (m_historyIndex++) + ":" + reg;
        return this;
    }

    public LinearBSMBuilder Add(BooleanStateAction action) {

        actionsAssociated.Add(action);
        m_history += "\n" + (m_historyIndex++) + ":" + action;
        return this;
    }

    public LinearBSMBuilder Add(BooleanStateStep newState) {
        if (newState == null) return this;
        if (previousStateFound != null && newState!= previousStateFound  )
        {
            StepAndActions s = CreateStepAndFlushActions(previousStateFound, actionsAssociated);
            stepCreated.Add(s);
        }


        previousStateFound = newState;
        m_history += "\n" + (m_historyIndex++) + ":" + newState;
        return this;
    }

    private StepAndActions CreateStepAndFlushActions(BooleanStateStep state, List<BooleanStateAction> actionsAssociated)
    {
        if (state == null) throw new Exception("Should not be null");
        StepAndActions s = new StepAndActions(state, actionsAssociated.ToList(), null);
        actionsAssociated.Clear();
        return s;

    }

    public int GetStepsCount()
    {
        return stepCreated.Count;
    }

    public int GetActionsStack()
    {
        return actionsAssociated.Count;
    }
    public LinearBSMBuilder FlushLastState() {
        if (previousStateFound != null)
        {
            StepAndActions s = CreateStepAndFlushActions(previousStateFound, actionsAssociated);
            stepCreated.Add(s);
        }
        previousStateFound = null;
        return this;
    }
    public LinearBooleanStateMachine Finalize() {
        if (previousStateFound !=null)
        {
            StepAndActions s = CreateStepAndFlushActions(previousStateFound, actionsAssociated);
            stepCreated.Add(s);
        }
        LinearBooleanStateMachine linear = new LinearBooleanStateMachine(register, stepCreated.ToList());
        Reset();
        return linear;
    }

    public bool IsValide()
    {
        return register != null && stepCreated != null && stepCreated.Count > 0;
    }

    
    public string BuildHistory()
    {
        return m_history;
    }

    public void ResetNewActionsList() {
        actionsAssociated = new List<BooleanStateAction>();
    }

    public static LinearBSMBuilder Create() { return new LinearBSMBuilder(); }
}