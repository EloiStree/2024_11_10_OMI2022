using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_ApplicationToBoolean : MonoBehaviour
{
    public ApplicationFocusToBooleanState m_register;


    public void ClearRegister()
    {
        m_register.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<AppFocusRegexToBoolean> found;
                LoadApplicationFocusToBoolean(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_register.AddAppToListen(found[y]);
                }
            }
        }
    }

    private void LoadApplicationFocusToBoolean(string textToLoad, out List<AppFocusRegexToBoolean> focusFound )
    {
        focusFound = new List<AppFocusRegexToBoolean>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 2)
            {
                    if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim()) && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
                    {
                        focusFound.Add( new AppFocusRegexToBoolean( tokens.GetValue(0).Trim(), tokens.GetValue(1).Trim()));
                    }
            }
            else
            if (tokens.GetCount() > 2)
            {
                if (string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
                    continue;
                if (string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
                    continue;

                string condition = tokens.GetValue(1).Trim().ToUpper();

                if (!(condition == "OR" || condition == "AND"))
                    continue;
                List<string> regex = new List<string>();
                for (int j = 2; j < tokens.GetCount(); j++)
                {
                    regex.Add(tokens.GetValue(j).Trim());
                }
                AppFocusRegexToBoolean tmp =
                    new AppFocusRegexToBoolean(
                        tokens.GetValue(0).Trim(),
                        regex,
                        condition == "AND" ?
                        AppFocusRegexToBoolean.ConditionType.And :
                        AppFocusRegexToBoolean.ConditionType.Or);
                focusFound.Add(tmp);
            }
        }
    }

}
