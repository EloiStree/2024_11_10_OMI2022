using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToBooleanChangeInRegister : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public string m_boolName;
    public DefaultBooleanChangeListener m_boolObserver;
 
   

    void FixedUpdate()
    {
        m_register.Get(m_boolName, out bool exist, out bool value);
        m_boolObserver.SetBoolean(in value, out bool changed);
    }
}
