using OpenMacroInputComAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmAPI_ShareBooleanRegister : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public string m_sharedRegisterNameAsMMF="DefaultBooleanState";

    public BooleanRawRegister defaultRegister;
    public BooleanRawRegisterIPC defaultMemoryPointer;


    [TextArea(0, 10)]
    public string m_names;

    [TextArea(0, 10)]
    public string m_booleans;



    private void Awake()
    {
        defaultRegister = new BooleanRawRegister(BooleanRawRegister.Size._4x4);
        
        defaultMemoryPointer = new BooleanRawRegisterIPC(ref defaultRegister, m_sharedRegisterNameAsMMF);

        //m_register.m_register.AddValueChangeListener(RefreshBooleansFile);
        //m_register.m_register.AddNamedIndexChangeListener(RefreshIdNamesFile);
        m_register.m_register.AddChangeListener(RefreshBasedOnChange);
        InvokeRepeating("PushWhileListenerNotCoded", 0, 0.1f);
    }
    public void PushWhileListenerNotCoded()
    {
        RefreshBooleansFile();
        RefreshIdNamesFile();
    }

    public void RefreshBasedOnChange(string booleanName, bool newValue)
    {

        Debug.Log("T:" + booleanName + ">" +( newValue?"1":"0") );

    }
   

    public void RefreshBooleansFile()
    {
        defaultMemoryPointer.OverrideMemoryValue_BooleanState();
        m_booleans = defaultMemoryPointer.m_linkedRegisterCompressor.GetBooleansCompressed();
    }
    public void RefreshIdNamesFile()
    {
        defaultMemoryPointer.OverrideMemoryValue_BooleanIndexName();
        m_names = defaultMemoryPointer.m_linkedRegisterCompressor.GetIdsCompressed();
    }

    
}
