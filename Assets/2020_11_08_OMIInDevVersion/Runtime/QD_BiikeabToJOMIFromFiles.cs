using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using UnityEngine;

public class QD_BiikeabToJOMIFromFiles : MonoBehaviour
{

    [Header("Regex to JOMI")]
    public RegexBooleanToActionObserved m_regexToJomi;
    public ObservedSyncFile m_regexToJomiFile;
 
    [Header("To Code later")]
    public TextAsset m_booleansRegexLnToJomi;
    public TextAsset m_booleansRegexNlToJomi;
    public TextAsset m_linearStateMachineToJomi;


   

 
   
  
    

  
   
}



public class ActionAsString {
    public string m_action;

    public ActionAsString(string shortcut)
    {
        m_action = shortcut;
    }
}

public class TileLine {

    private string m_rawReceived;
    private string[] m_tokens;
    public TileLine(string line) {
        m_rawReceived = line;
        m_tokens = line.Split('♦');
    }
    public int GetCount() { return m_tokens.Length; }
    public string GetValue(int index) {
        return m_tokens[ index];
    }

    public List<string> GetAsList()
    {
        return m_tokens.ToList();
    }
}

[System.Serializable]
public class ShogiParameter
{
    public static Regex m_acceptedFormat = new Regex("(☗[A-Za-z0-9]+)(\\s[A-Za-z0-9]+)?");
    [SerializeField] private string m_optionName="";
    [SerializeField] private string m_value="";

    public ShogiParameter()
    {}

    public ShogiParameter(string optionName): this(optionName,"")
    {
    }

    public ShogiParameter(string optionName, string value)
    {
        if (optionName.Length > 0 && optionName[0] == '☗')
            optionName = optionName.Substring(1);
        this.m_optionName = optionName;
        this.m_value = value;
    }

    public string GetParamName() { return m_optionName; }
    public string GetValue() { return m_value; }

    public bool HasValue() { return string.IsNullOrEmpty(m_value); }
    public bool IsAtomic() { return !HasValue(); }

    public string GetAsString() { return string.Format("☗{0} {1}", m_optionName, m_value); }


    public static List<ShogiParameter> FindParametersInString(string processedOption)
    {
        List<ShogiParameter> result = new List<ShogiParameter>();
        MatchCollection collections = m_acceptedFormat.Matches(processedOption);
        for (int a = 0; a < collections.Count; a++)
        {
            string[] t = collections[a].Value.Split(' ');
            if (t.Length == 1)
                result.Add(new ShogiParameter(t[0]));
            if (t.Length == 2)
                result.Add(new ShogiParameter(t[0], t[1]));
        }
        return result;
    }

    public static bool HasParam(List<ShogiParameter> paramsToCheck, string paramToFound)
    {
        ShogiParameter tmp;
        return HasParam(paramsToCheck, paramToFound, out tmp);
    }
    public static bool HasParam(List<ShogiParameter> paramsToCheck, string paramToFound, out ShogiParameter found)
    {
        found = null;
        if (paramsToCheck == null)
            return false;
        for (int i = 0; i < paramsToCheck.Count; i++)
        {
            if (paramsToCheck[i].GetParamName() == paramToFound)
            {
                found = paramsToCheck[i];
                return true;
            }
        }
        return false;
    }
}
