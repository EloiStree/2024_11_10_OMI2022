using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyStrokeToBooleanStateMachine : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public KeyboardReadMono m_keyboardReader;
    public List<ObserveKeyboardToBooleanState> m_keyListWithSwitch = new List<ObserveKeyboardToBooleanState>();



    void Update()
    {
        BooleanStateRegister reg=null; 
        m_register.GetRegister(ref reg);
        for (int i = 0; i < m_keyListWithSwitch.Count; i++)
        {
            bool state = m_keyboardReader.IsTouchActive(m_keyListWithSwitch[i].m_info.m_key);
            bool hasChange = false;
            bool isToggle = m_keyListWithSwitch[i].m_info.IsToggler();
            m_keyListWithSwitch[i].m_switcher.SetValue(state, out hasChange);
            if (!isToggle ) { 
                reg.Set(m_keyListWithSwitch[i].m_info.m_booleanKeyName,state);
            }
            if (isToggle && hasChange)
            {
                if(state && m_keyListWithSwitch[i].m_info.m_toggleOnTrue)
                    Toggle(reg, m_keyListWithSwitch[i].m_info.m_booleanKeyName);
                if (!state && m_keyListWithSwitch[i].m_info.m_toggleOnFalse)
                    Toggle(reg, m_keyListWithSwitch[i].m_info.m_booleanKeyName);
            }

        }

    }

    private void Toggle(BooleanStateRegister reg,string nameKey)
    {
        bool state = false;
        if (reg.Contains(nameKey))
        {
            state = reg.GetValueOf(nameKey);
        }
        reg.Set(nameKey, !state);
    }

    public void Clear()
    {
        m_keyListWithSwitch.Clear();
    }

    public void Add(KeyboardToBooleanState tmpState)
    {
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        m_keyListWithSwitch.Add(new ObserveKeyboardToBooleanState(tmpState));
    
       
    }
}

[System.Serializable]
public class KeyboardToBooleanState {

    public KeyboardTouch m_key;
    public string m_booleanKeyName;
    public bool m_toggleOnTrue;
    public bool m_toggleOnFalse;

    public KeyboardToBooleanState(KeyboardTouch key, string booleanKeyName)
    {
        this.m_key = key;
        this.m_booleanKeyName = booleanKeyName;
    }

   
    public bool IsToggler() {
        return m_toggleOnTrue || m_toggleOnFalse;
    }
}

public class ObserveKeyboardToBooleanState {
    public KeyboardToBooleanState m_info;
    public BooleanSwitchListener m_switcher= new BooleanSwitchListener();

    public ObserveKeyboardToBooleanState(KeyboardToBooleanState tmpState)
    {
        this.m_info = tmpState;
    }
}
