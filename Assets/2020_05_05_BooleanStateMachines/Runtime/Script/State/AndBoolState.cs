using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class AndBoolState : AbstractConditionBoolState
{
    public AndBoolState(List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange, float timeToCheckChange) : base(observedState, observedChange)
    {
    }

    

    public override bool IsConditionValide(BooleanStateRegister register)
    {
        if (!BooleanStateUtility.AreBooleansRegistered(register, m_observedState, m_observedChange)){
            throw new BooleanIsNotDefinedException();
        }
        return BooleanStateUtility.AND(register, m_observedState, m_observedChange);
    }

    
    public override string ToString()
    {
        return "(BSM, AND:" + string.Join(" ", m_observedState.Select(k => k.GetNameWithDesciption())) +" - " +string.Join(" ", m_observedChange.Select(k => k.GetNameWithDesciption())) + ")";
    }
}

public class BooleanIsNotDefinedException:Exception{
    //public BooleanIsNotDefinedException() : base()
    //{
    //    Debug.LogWarning("Some boolean was not found");
    //}
    //public BooleanIsNotDefinedException(string booleanName) : base() {
    //    Debug.LogWarning("Boolean was not found:" + booleanName);
    //}
}




[System.Serializable]
public class OrBoolState : AbstractConditionBoolState
{
    public OrBoolState(List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange, float timeToCheckChange) : base(observedState, observedChange)
    {
    }

    public override bool IsConditionValide(BooleanStateRegister register)
    {

        return BooleanStateUtility.OR(register, m_observedState, m_observedChange);
    }


    public override string ToString()
    {
        return "(BSM, OR:" + string.Join(" ", m_observedState.Select(k => k.GetNameWithDesciption())) + " - " + string.Join(" ", m_observedChange.Select(k => k.GetNameWithDesciption())) + ")";
    }
}


[System.Serializable]
public class XorBoolState : AbstractConditionBoolState
{
    public XorBoolState(List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange, float timeToCheckChange) : base(observedState, observedChange)
    {
    }

    public override bool IsConditionValide(BooleanStateRegister register)
    {
        return BooleanStateUtility.XOR(register, m_observedState, m_observedChange);
    }

    public override string ToString()
    {
        return "(BSM, XOR:" + string.Join(" ", m_observedState.Select(k => k.GetNameWithDesciption())) + " - " + string.Join(" ", m_observedChange.Select(k => k.GetNameWithDesciption())) + ")";
    }
}



[System.Serializable]
public class ClassicBoolState : AndBoolState
{
    public ClassicBoolState( float timeToCheckChange) : base(new List<BooleanValueRef>(), new List<BooleanValueChangeRef>() , timeToCheckChange)
    {
    }

    public ClassicBoolState(List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange, float timeToCheckChange) : base(observedState, observedChange, timeToCheckChange)
    {
    }

  
    public override string ToString()
    {
        return "(BSM, CLASSIC:" + string.Join(" ", m_observedState.Select(k=>k.GetNameWithDesciption())) + "  " + string.Join(" ", m_observedChange.Select(k => k.GetNameWithDesciption())) + ")";
    }
}



[System.Serializable]
public abstract class AbstractConditionBoolState : BooleanStateStep
{
    [SerializeField] protected List<BooleanValueRef> m_observedState;
    [SerializeField] protected List<BooleanValueChangeRef> m_observedChange;
    public  bool IsBooleansRegistered(BooleanStateRegister register)
    {
        return BooleanStateUtility.AreBooleansRegistered(register, m_observedState, m_observedChange);
    }
    public AbstractConditionBoolState(List<BooleanValueRef> observedState, List<BooleanValueChangeRef> observedChange)
    {
        m_observedState = observedState;
        m_observedChange = observedChange;
    }

    public abstract override bool IsConditionValide(BooleanStateRegister register);

    protected bool IsChangeFound(BooleanStateRegister reg, BooleanValueChangeRef booleanValueChange)
    {
        return BooleanStateUtility.HasChanged(reg, booleanValueChange);
    }

    public override string ToString()
    {
        return "(BSM, CONDITION:" + string.Join(" ", m_observedState.Select(k => k.GetNameWithDesciption())) + " - " + string.Join(" ", m_observedChange.Select(k => k.GetNameWithDesciption())) + ")";
    }
}