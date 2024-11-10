
using System.Text.RegularExpressions;
using UnityEngine;
[System.Serializable]
public class RegexBoolState : BooleanStateStep
{
    [SerializeField] string m_regex;
    [SerializeField] BooleanGroup m_observed;
    [SerializeField] float m_timeToCheckChange;
    [SerializeField] RegexableValueType m_textToApplyType = RegexableValueType.NewToLast;



    public RegexBoolState(string regex, BooleanGroup groupObserved, RegexableValueType textToApplyType, float timeToCheckChange)
    {
        m_regex = regex;
        m_observed = groupObserved;
        m_textToApplyType = textToApplyType;
        m_timeToCheckChange = timeToCheckChange;

    }
        
    public override bool IsConditionValide(BooleanStateRegister register)
    {
        return Regex.IsMatch(GetTextToRegex(register), m_regex);
    }

    private string GetTextToRegex(BooleanStateRegister register)
    {
        return BooleanStateUtility.GetTextFor(m_textToApplyType, register, m_observed, m_timeToCheckChange);
    }

    public override string ToString()
    {
        return "(BSM,Regex:" + m_regex + ")";
    }
}
