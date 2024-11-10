using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_ScreenZoneToBoolean : MonoBehaviour
{
    public ScreenZoneToBooleans m_register;
    public void ClearRegister()
    {
        m_register.ClearAll();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<NamedScreenPourcentZone> screenPourcent;
                LoadScreenZone(File.ReadAllText(filePath[i]), out screenPourcent);
                for (int y = 0; y < screenPourcent.Count; y++)
                {

                    m_register.AddScreenZone(screenPourcent[y]);
                }
               
            }
        }
    }

    private void LoadScreenZone(string textToLoad, out List<NamedScreenPourcentZone> found)
    {
        found = new List<NamedScreenPourcentZone>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 2 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()) && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
            {
                string v = tokens.GetValue(0).Trim();
                if (v.ToLower().StartsWith("square"))
                {
                    string[] tokensPct = v.Split(':');
                    if (tokensPct.Length == 5)
                    {
                        float downTopMinPourcent, downTopMaxPourcent;
                        float leftRightMinPourcent, LeftRightMaxPourcent;
                        try
                        {
                            downTopMinPourcent = float.Parse(tokensPct[3].Replace("%", "").Replace(",", ".").Trim());
                            downTopMaxPourcent = float.Parse(tokensPct[4].Replace("%", "").Replace(",", ".").Trim());
                            leftRightMinPourcent = float.Parse(tokensPct[1].Replace("%", "").Replace(",", ".").Trim());
                            LeftRightMaxPourcent = float.Parse(tokensPct[2].Replace("%", "").Replace(",", ".").Trim());

                            found.Add(new NamedScreenPourcentZone(
                                tokens.GetValue(1).Trim(),
                                downTopMinPourcent,
                                downTopMaxPourcent,
                                leftRightMinPourcent,
                                LeftRightMaxPourcent));
                        }
                        catch (Exception e)
                        {
                        }

                    }
                }

            }
        }


    }
}
