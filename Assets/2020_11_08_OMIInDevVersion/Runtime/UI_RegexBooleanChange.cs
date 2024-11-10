using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UI_RegexBooleanChange : MonoBehaviour
{

    public BooleanStateRegisterMono m_register;
    public float m_timeOverwatch=1f;
    public InputField m_regexNewToOld;
    public InputField m_regexOldToNew;
    
    void Update()
    {
        BooleanStateRegister reg=null;
        m_register.GetRegister(ref reg);

        List<NamedBooleanChangeTimed> recentToLatest = BooleanStateUtility
            .GetBoolChangedBetweenTimeRangeFromNow(
            reg, null, m_timeOverwatch, BooleanStateUtility.SortType.RecentToLatest);
        NamedBooleanChangeTimed[] latestToRecent = recentToLatest.ToArray();
        Array.Reverse(latestToRecent);

        if(m_regexNewToOld)
            m_regexNewToOld.text = BooleanStateUtility.Description.Join("", recentToLatest);

        if (m_regexOldToNew)
            m_regexOldToNew.text = BooleanStateUtility.Description.Join("", latestToRecent);

    }
}
