using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BooleanStateMachineTagDebugger : MonoBehaviour
{

    public BooleanStateRegisterMono m_register;
    public GameObject m_prefabOfItem;
    public RectTransform m_whereToCreate;

    public Dictionary<string, UI_BooleanStateItem> m_idToUI = new Dictionary<string, UI_BooleanStateItem>();
    public Dictionary<string, UI_BooleanSwitcherMono> m_idToSwitcher = new Dictionary<string, UI_BooleanSwitcherMono>();

    void Update()
    {
        BooleanStateRegister register = m_register.m_register;
        foreach (string key in m_register.m_register.GetAllKeys())
        {
           bool  value= register.GetStateOf(key).GetValue();
            UI_BooleanStateItem uiItem = GetUI(key);
            if (uiItem != null) { 
            UI_BooleanSwitcherMono uiSwitcher = uiItem.GetComponent<UI_BooleanSwitcherMono>();
                if (uiSwitcher) {
                    uiSwitcher.SetNameOfBoolean(key);
                    uiSwitcher.SetRegister(m_register);
                }
            }
            uiItem.SetValue(value);
        }

    }


    private UI_BooleanStateItem GetUI(string key)
    {
        if (m_idToUI.ContainsKey(key))
            return m_idToUI[key];
        UI_BooleanStateItem si;
        GameObject created = GameObject.Instantiate(m_prefabOfItem);
        UI_BooleanStateItem script = created.GetComponent<UI_BooleanStateItem>();
        m_idToUI.Add(key, script);
        created.transform.SetParent( m_whereToCreate,false);
        script.SetName(key);
        script.SetValue(false);


        return script;
    }
}
