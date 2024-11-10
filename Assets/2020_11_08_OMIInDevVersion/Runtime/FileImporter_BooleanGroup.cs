using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class NamedBooleanGroup {

    

    public string m_groupName="";
    public List<string> m_targetNames = new List<string>();

    public NamedBooleanGroup(string groupName, List<string> targetName)
    {
        m_groupName = groupName;
        m_targetNames = targetName;
    }


    public string GetGroupName() { return m_groupName; }
    public List<string> GetAllBooleanNames()
    {
        return m_targetNames.ToList();
    }
}
[System.Serializable]
public class NamedBooleanRefGroup
{

    public NamedBooleanGroup m_booleanGroup;
    public BooleanStateRegisterMono m_register;



    public NamedBooleanRefGroup(BooleanStateRegisterMono register, NamedBooleanGroup group)
    {
        m_register = register;
        m_booleanGroup = group;
    }

    public void SwitchToTrueElseFalse(string[] setAsTrue)
    {
        List<string> names = m_booleanGroup.GetAllBooleanNames();
        List<string> toSetTrue = names.Intersect(setAsTrue).ToList() ;
        List<string> toSetFalse = names.Except(setAsTrue).ToList() ;

        for (int i = 0; i < toSetTrue.Count; i++)
        {
            SetBooleanValueOrCreateIt(names[i], true);
        }
        for (int i = 0; i < toSetFalse.Count; i++)
        {
            SetBooleanValueOrCreateIt(names[i], false);
        }
    }

    public void SetAllTo(bool value)
    {
        if (m_register == null)
            return;
        List<string> names = m_booleanGroup.GetAllBooleanNames();
        for (int i = 0; i < names.Count; i++)
        {
            SetBooleanValueOrCreateIt (names[i], value);
        }
    }

    private void SetBooleanValueOrCreateIt( string name, bool value)
    {
        m_register.Set(name, value, true);
    }

    public void SetAllFalse()
    {
        SetAllTo(false);
    }
    public void SetAllTrue()
    {
        SetAllTo(true);
    }

}

public class FileImporter_BooleanGroup : MonoBehaviour
{

    public BooleanGroupRegister m_booleanRegister;
    public BooleanStateRegisterMono m_booleanMono;

    public void Clear()
    {
        m_booleanRegister.Clear();
    }

    public void LoadFiles(params string[] filePath)
    {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<NamedBooleanGroup> found;
                Load(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {
                    m_booleanRegister.Add(found[y]);
                }
            }
        }
    }
    private void Load(string textToLoad, out List<NamedBooleanGroup> found)
    {
        found = new List<NamedBooleanGroup>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;

        TimedCommandLines macro = new TimedCommandLines();
        List<string> commands = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() >= 2 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()) && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
            {
                string name = tokens.GetValue(0).Trim().ToLower();
                List<string> names = tokens.GetValue(1).Trim().Split(' ').Where(k=>!string.IsNullOrEmpty(k)).ToList();
                found.Add( new NamedBooleanGroup(name, names) );


                if (tokens.GetCount() >= 3 && !string.IsNullOrEmpty(tokens.GetValue(2).Trim()))
                {

                    string processedOption = tokens.GetValue(2);
                    while (processedOption.IndexOf("  ") > -1)
                        processedOption = processedOption.Replace("  ", " ");

                    List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(processedOption);

                    ShogiParameter tmpFound;
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "AllFalseByDefault", out tmpFound))
                    {
                        m_booleanMono.Set(false, names);
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "AllTrueByDefault", out tmpFound))
                    {
                        m_booleanMono.Set(true, names);
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "SetFalse", out tmpFound))
                    {
                        m_booleanMono.Set(tmpFound.GetValue(),false,true);
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "SetTrue", out tmpFound))
                    {
                        m_booleanMono.Set(tmpFound.GetValue(), true, true);
                    }


                }

            }
            

        }
       
    }

}
