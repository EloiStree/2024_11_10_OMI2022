using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ApplicationFocusToBooleanState : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public ListenToApplicationFocus m_applicationSwitch;
    public List<AppFocusRegexToBoolean> m_applicationSwitchObserved = new List<AppFocusRegexToBoolean>();
    private void Start()
    {
        m_applicationSwitch.AddListener(ChangeOfWindowFocus);
    }

    private void ChangeOfWindowFocus(string previousWinName, string currentWinName)
    {
        BooleanStateRegister reg=null;
        m_register.GetRegister(ref reg);
        for (int i = 0; i < m_applicationSwitchObserved.Count; i++)
        {
            AppFocusRegexToBoolean co = m_applicationSwitchObserved[i];
            reg.Set(co.m_booleanName, co.MatchRegex(currentWinName));
        }

    }

    public void AddAppToListen(string boolName, string regexLinked)
    {
        m_applicationSwitchObserved.Add(new AppFocusRegexToBoolean(boolName, regexLinked));
    }

    public void AddAppToListen(AppFocusRegexToBoolean valueToAdd)
    {
        m_applicationSwitchObserved.Add(valueToAdd);
    }

    public void Clear()
    {
        m_applicationSwitchObserved.Clear();
    }
}
[System.Serializable]
public class AppFocusRegexToBoolean {

    public string m_booleanName;
    public List<string> m_regexs = new List<string>();
    public enum ConditionType { And, Or}
    public ConditionType m_conditionType;

    public AppFocusRegexToBoolean(string booleanName, List<string> regexs, ConditionType conditionType)
    {
        m_booleanName = booleanName;
        m_regexs.Clear();
        for (int i = 0; i < regexs.Count; i++)
        {
            m_regexs.Add(regexs[i]);

        }
        m_conditionType = conditionType;
    }

    public AppFocusRegexToBoolean(string booleanName, string regexLinked)
    {
        m_booleanName = booleanName;
        m_regexs.Clear();
        m_regexs.Add(regexLinked);
        m_conditionType =ConditionType.And;
    }

    public bool MatchRegex(string currentWindowName)
    {
        if (m_conditionType == ConditionType.And)
            return MatchRegexWithAndCondition(currentWindowName);
        else return MatchRegexWithOrCondition(currentWindowName);
    }
    public bool MatchRegexWithAndCondition(string currentWindowName)
    {
        try { 
        for (int i = 0; i < m_regexs.Count; i++)
        {
            if (!Regex.Match(currentWindowName, m_regexs[i]).Success) 
            return false;

        }
        return true;
        }
        catch (Exception) { return false; }
    }
    public bool MatchRegexWithOrCondition(string currentWindowName)
    {
        try
        {
            for (int i = 0; i < m_regexs.Count; i++)
            {
                if (!Regex.Match(currentWindowName, m_regexs[i]).Success)
                    return true;

            }
            return false;
        }
        catch (Exception) { return false; }
    }
}
