using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BooleanSwitcherMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_target;
    public string m_booleanName;


    public void SetNameOfBoolean(string booleanName) {
        m_booleanName = booleanName;
    }

    public void SetBooleanValue(bool setTrue)
    {

        if (m_target == null) return;
        m_target.m_register.Set(m_booleanName, setTrue);
    }
    public void SetBooleanTrue()
    {
        SetBooleanValue(true);


    }
    public void SetBooleanFalse()
    {

        SetBooleanValue(false);
    }

    public void SwitchBoolean() {

        if (m_target == null) return;
        m_target.m_register.SwitchValue(m_booleanName);
    }

    public void SetRegister(BooleanStateRegisterMono register)
    {
        m_target = register;
    }
}

public class BooleanRegisterRef
{
    public string m_booleanName;
    public BooleanStateRegisterMono m_linkedRegister;

    //public bool IsBooleanExist()
    //{
    //    m_linkedRegister.m_register.Contains(m_booleanName);
    //}
    //public bool IsBooleanTrue(bool exist , bool value)
    //{
    //    exist = IsBooleanExist();
    //    if (exist)
    //        value = m_linkedRegister.m_register.GetValueOf(m_booleanName);
    //    else value = false;
    //}

    public void SetBooleanValue(bool setTrue)
    {

    }
    public void SetBooleanTrue()
    {


    }
    public void SetBooleanFalse()
    {

    }

    public void SwitchBoolean()
    {

    }
}
