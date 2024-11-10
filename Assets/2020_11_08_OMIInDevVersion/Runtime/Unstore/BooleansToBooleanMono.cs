using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleansToBooleanMono : MonoBehaviour
{

    public BooleanStateRegisterMono m_register;
    public List<BooleansToBoolean> m_booleanToStateMachine = new List<BooleansToBoolean>();

    public void Clear()
    {
        m_booleanToStateMachine.Clear();
    }
    public void Add(BooleansToBoolean booleansToBoolean)
    {
        m_booleanToStateMachine.Add(booleansToBoolean);
    }

    void Update()
    {
        if (m_register == null) return;
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        if (reg == null) return;

        for (int i = 0; i < m_booleanToStateMachine.Count; i++)
        {
            if (m_booleanToStateMachine[i].m_andBooleanState.IsBooleansRegistered(reg)) { 
                bool requestInverse = false;

                bool isvalide = m_booleanToStateMachine[i].m_andBooleanState.IsConditionValide(reg);
                reg.Set(m_booleanToStateMachine[i].m_booleanToAffect
                    , requestInverse ? !isvalide : isvalide
                    );
            }
        }
    }

    
}
[System.Serializable]
public class BooleansToBoolean
{
    public string m_booleanToAffect = "";
    public AndBoolState m_andBooleanState = null;
}