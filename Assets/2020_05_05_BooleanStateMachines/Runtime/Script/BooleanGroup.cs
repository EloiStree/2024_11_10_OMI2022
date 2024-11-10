using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BooleanGroup
{

    [SerializeField] string[] m_booleanNames;

    public BooleanGroup(params string[] booleanNames)
    {
        m_booleanNames = booleanNames;
    }
    public string[] GetNames() { return m_booleanNames; }
    public int GetCount() { return m_booleanNames.Length; }

}

[System.Serializable]
public class BooleanGroupRef
{
    [SerializeField] List<BooleanValueRef> m_booleanTracked = new List<BooleanValueRef>();
    [SerializeField] List<BooleanValueChangeRef> m_booleanChangeTracked = new List<BooleanValueChangeRef>();

    public BooleanGroupRef(List<BooleanValueRef> booleanTracked, List<BooleanValueChangeRef> booleanChangeTracked)
    {
        this.m_booleanTracked = booleanTracked;
        this.m_booleanChangeTracked = booleanChangeTracked;
    }

    public string[] GetNames()
    {
        List<string> result = new List<string>();
        result.AddRange(m_booleanTracked.Select(k => k.GetName()));
        result.AddRange(m_booleanChangeTracked.Select(k => k.GetName()));
        return result.ToArray();
    }
    public string[] GetValueDescriptions()
    {
        List<string> result = new List<string>();
        result.AddRange(m_booleanTracked.Select(k => k.GetNameWithDesciption()));
        result.AddRange(m_booleanChangeTracked.Select(k => k.GetNameWithDesciption()));
        return result.ToArray();
    }
    public int GetCount() { return m_booleanTracked.Count + m_booleanChangeTracked.Count; }

}


[System.Serializable]
public class BooleanValueRef
{
    [SerializeField] string m_name;
    [SerializeField] BooleanInverseTag m_inverseTag;

    public BooleanInverseTag GetInverseTag() { return m_inverseTag; }
    public string GetName() { return m_name; }

    public override string ToString()
    {
        return GetNameWithDesciption();
    }

    public BooleanValueRef(string name, BooleanInverseTag inverseTag =BooleanInverseTag.None)
    {
        m_name = name;
        m_inverseTag = inverseTag;
    }

    public static BooleanGroup GetAsGroup(List<BooleanValueRef> observedState)
    {
        return new BooleanGroup(observedState.Select(k => k.GetName()).ToArray());
    }

    //private static string m_pattern = "(^[a-zA-Z]+\\s)|(\\s[a-zA-Z]+\\s)|(\\s[a-zA-Z]+$)";
    public static bool HasArrow(string txt)
    {
        return txt.IndexOf('↑') >= 0 || txt.IndexOf('↓') >= 0;

    }
    public static bool CreateFrom(string [] txt, out List< BooleanValueRef> created)
    {
        BooleanValueRef br;
        created = new List<BooleanValueRef>();
        for (int i = 0; i < txt.Length; i++)
        {
            if (CreateFrom(txt[i], out br))
                created.Add(br);
        }
        return created.Count > 0;
    }

    public bool IsRequestingActive()
    {
        return m_inverseTag == BooleanInverseTag.None;
    }

    public bool IsRequestingInverse()
    {
        return m_inverseTag == BooleanInverseTag.Inverse;
    }

    public static bool CreateFrom(string txt, out BooleanValueRef created)
    {
        txt = txt.ToLower();
        created = null;
        txt = Regex.Replace(txt, "[^!a-zA-Z0-9\\s↑↓]", "");
        txt = txt.Trim();
        if (HasArrow(txt) || txt.Length<=0)
            return false;
        bool inverse = txt[0] == '!';
        txt=txt.Replace("!", "");
        created = new BooleanValueRef(txt, inverse ? BooleanInverseTag.Inverse : BooleanInverseTag.None);
        return true;
    }

    public string GetNameWithDesciption()
    {
        return ((m_inverseTag == BooleanInverseTag.None) ? "" : "!") + m_name;
    }

}
[System.Serializable]
public class BooleanValueChangeRef
{
    [SerializeField] string m_name;
    [SerializeField] BooleanChangeType m_changeType;
    [SerializeField] BooleanInverseTag m_inverseTag;
    [SerializeField] uint m_millisecondToCheck;

    public BooleanInverseTag GetInverseTag() { return m_inverseTag; }
    public string GetName() { return m_name; }
    public BooleanChangeType GetChangeType() { return m_changeType; }

    public BooleanValueChangeRef(string name, BooleanChangeType changeType, BooleanInverseTag inverseTag , uint millisecondToCheck)
    {
        m_name = name;
        m_changeType = changeType;
        m_inverseTag = inverseTag;
        m_millisecondToCheck = millisecondToCheck;
    }

    public uint GetMillisecondToCheck() { return m_millisecondToCheck; }
    //private static string m_pattern= "!+[a-zA-Z]+[↑↓]";

    public static bool HasArrow(string txt)
    {
        return txt.IndexOf('↑') >= 0 || txt.IndexOf('↓') >= 0;

    }
    public static bool CreateFrom(string [] txt, out List<BooleanValueChangeRef> created)
    {
        BooleanValueChangeRef br;
        created = new List<BooleanValueChangeRef>();
        for (int i = 0; i < txt.Length; i++)
        {
            if (CreateFrom(txt[i], out br))
                created.Add(br);
        }
        return created.Count > 0;
    }
    private static char[] arrowsUpDown = new char[] { '↑', '↓' };
    public static bool CreateFrom(string txt, out BooleanValueChangeRef created)
    {
        txt = txt.ToLower();
        created = null;
        txt = Regex.Replace(txt, "[^!a-zA-Z0-9\\s↑↓]", "");
        txt = txt.Trim();
        if (!HasArrow(txt) || txt.Length <= 0)
            return false;
        int indexOfLastArrow = txt.LastIndexOfAny(arrowsUpDown);
        if (indexOfLastArrow < 0)
            return false;
        char arrow = txt[indexOfLastArrow];
        uint timeInMs = ConverToMs(txt.Substring(indexOfLastArrow));
        string arrowsOrder= Regex.Replace(txt, "[^↑↓]", "");

       
        bool inverse = txt[0] == '!';
        string word = Regex.Replace(txt, "[^a-zA-Z]", "");
        created = new BooleanValueChangeRef(word , arrow == '↑'? BooleanChangeType.SetFalse:BooleanChangeType.SetTrue, inverse? BooleanInverseTag.Inverse : BooleanInverseTag.None, timeInMs);
        return true;
    }

    private static uint ConverToMs(string text)
    {
        text=Regex.Replace(text, "[^0-9]", "");
        if (text.Length <= 0)
            return 50;
        return uint.Parse(text);
    }

    public override string ToString()
    {
        return GetNameWithDesciption();
    }

    public static BooleanGroup GetAsGroup(List<BooleanValueRef> observedState)
    {
        return new BooleanGroup(observedState.Select(k => k.GetName()).ToArray());
    }

    public string GetNameWithDesciption()
    {
        return  ((m_inverseTag==BooleanInverseTag.None)?"":"!")+ m_name + (m_changeType == BooleanChangeType.SetTrue ? '↓' : '↑');
    }

    public bool IsRequestingInverse()
    {
        return m_inverseTag == BooleanInverseTag.Inverse;
    }

    public bool IsRequestingActive()
    {
        return m_inverseTag == BooleanInverseTag.None;
    }

    public float GetSecondToCheck()
    {
        return GetMillisecondToCheck() / 1000f;
    }
}

[System.Serializable]
public class NamedBooleanChangeTimed {
    string m_named;
    TimedBooleanChange m_whenChanged;

    public NamedBooleanChangeTimed(string name, TimedBooleanChange whenChanged)
    {
        m_named = name;
        m_whenChanged = whenChanged;
    }

    public string GetName() {
        return m_named; }

    public TimedBooleanChange GetWhenChanged() {
        return m_whenChanged;
    }

    public BooleanChangeType GetChangeType()
    {
        return m_whenChanged.GetChange();
    }
   
}