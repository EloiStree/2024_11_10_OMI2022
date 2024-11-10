using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RegexBooleanToActionMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public List<RegexBooleanToActionObserved> m_observedRegex = new List<RegexBooleanToActionObserved>();
    public UnityStringEvent m_actionFound;

    void Update()
    {
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);

        string currentStateTmp="";
        for (int i = 0; i < m_observedRegex.Count; i++)
        {
            RegexBooleanToActionObserved toCheck = m_observedRegex[i];

            List<NamedBooleanChangeTimed> recentToLatest = BooleanStateUtility
                .GetBoolChangedBetweenTimeRangeFromNow(
                reg, toCheck.m_boolGroup.GetCount()>0?toCheck.m_boolGroup:null,toCheck.m_timeObserved, 
                 toCheck.m_observeType==RegexBooleanToActionObserved.ObserveType.NewToOld?
                 BooleanStateUtility.SortType.RecentToLatest: BooleanStateUtility.SortType.LatestToRecent);
                currentStateTmp = toCheck. m_lastCheck = BooleanStateUtility.Description.Join("", recentToLatest);

            bool hasChange=false;
            try
            {
                Match m = Regex.Match(currentStateTmp, toCheck.m_regex);
                if (toCheck != null)
                    toCheck.m_boolSwitch .SetValue(m.Success, out hasChange);
            }
            catch (Exception e)
            {
                if(toCheck!=null )
                toCheck.m_boolSwitch.SetValue(false, out hasChange);
            }
            if (hasChange && toCheck.m_boolSwitch.GetValue()) {
                m_actionFound.Invoke(toCheck.m_actionAsString);
            
            }

        }

    }

    public void Clear()
    {
        m_observedRegex.Clear();
    }

    public void AddRegexToListen(RegexBooleanToActionObserved regexToListen)
    {
        m_observedRegex.Add(regexToListen);
    }
}

[System.Serializable]
public class RegexBooleanToActionObserved {

    public string m_regex="*";
    public BooleanGroup m_boolGroup=null;
    public float m_timeObserved=1000;
    public enum ObserveType { NewToOld, OldToNew}
    public ObserveType m_observeType= ObserveType.NewToOld;
    public string m_actionAsString="";
    [Header("Debug")]
    public string m_lastCheck;
    public bool m_regexFound;
    public BooleanSwitchListener m_boolSwitch= new BooleanSwitchListener();
}
