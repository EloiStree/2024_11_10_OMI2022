using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_RegexBooleanCondition : MonoBehaviour
{

    public RegexBooleanToActionMono m_regexRegister;

    public void ClearRegister()
    {
        m_regexRegister.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<RegexBooleanToActionObserved> found;
                LoadRegexToJomi(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_regexRegister.AddRegexToListen(found[y]);
                }

            }
        }
    }


    private void LoadRegexToJomi(string textToLoad, out List<RegexBooleanToActionObserved> foundregex)
    {
        foundregex = new List<RegexBooleanToActionObserved>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 5)
            {

                try
                {
                    if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(1).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(2).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(3).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(4).Trim()))
                    {
                        //Regex♦NewToOld♦1000♦Observed Boolean♦JOMI
                        RegexBooleanToActionObserved found = new RegexBooleanToActionObserved();
                        found.m_regex = tokens.GetValue(0).Trim();
                        string orderType = tokens.GetValue(1).ToLower().Trim();
                        if (orderType == "nl" || orderType == "no" || orderType == "newtolatest" || orderType == "newtoold")
                            found.m_observeType = RegexBooleanToActionObserved.ObserveType.NewToOld;
                        if (orderType == "ln" || orderType == "on" || orderType == "latesttonew" || orderType == "oldtonew")
                            found.m_observeType = RegexBooleanToActionObserved.ObserveType.OldToNew;

                        try
                        {
                            found.m_timeObserved = uint.Parse(tokens.GetValue(2).Trim()) / 1000f;
                        }
                        catch (Exception) { }

                        found.m_boolGroup = new BooleanGroup(tokens.GetValue(3).Trim().Split(' '));
                        found.m_actionAsString = tokens.GetValue(4).Trim();


                        foundregex.Add(found);
                    }

                }
                catch (Exception) { }
            }

        }
    }
}
