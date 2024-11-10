using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_ScreenZone : MonoBehaviour
{
    public ScreenZoneAndPointRegisterMono m_screenlocations;
    public ScreenZoneToBooleans m_screenObserved;
    public void Clear()
    {
        m_screenlocations.Clear();
    }

    public void LoadFiles(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                Load(File.ReadAllText(filePath[i]));

            }
        }
    }
    private void Load(string textToLoad)
    {
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;

        List<string> zoneobserved = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            try
            {
                if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                    continue;

                tokens = new TileLine(lines[i]);
                if (tokens.GetCount() == 3)
                {
                    string name = tokens.GetValue(0).Trim().ToLower();
                    float lr,bt;
                    if(Converter.ParseToFloat(tokens.GetValue(1).Trim(), out lr) 
                        && Converter.ParseToFloat(tokens.GetValue(2).Trim(), out bt))
                       m_screenlocations.Add(new NamedScreenPourcentPosition(name, lr, bt));
                }
                if (tokens.GetCount() == 5)
                {
                    string name = tokens.GetValue(0).Trim().ToLower();
                    float lrMin ,btMin ,lrMax , btMax ;

                    if (Converter.ParseToFloat(tokens.GetValue(1).Trim(), out lrMin)
                        && Converter.ParseToFloat(tokens.GetValue(2).Trim(), out btMin)
                        && Converter.ParseToFloat(tokens.GetValue(3).Trim(), out lrMax)
                        && Converter.ParseToFloat(tokens.GetValue(4).Trim(), out btMax))

                        m_screenlocations.Add(new NamedScreenPourcentZone(name, btMin, btMax, lrMin, lrMax));
                }
                if (tokens.GetCount() == 1 )
                {
                    string t = tokens.GetValue(0).ToLower().Trim();
                    if (t.IndexOf("☗observedasboolean") == 0)
                    {
                        t = t.Replace("☗observedasboolean", "");
                        string[] words = t.Split(' ');
                        
                        zoneobserved.AddRange(words);
                    }
                }

            }
            catch (Exception e) { Debug.Log("E:" + e.StackTrace); }


        }

        bool isfound;
        NamedScreenPourcentZone zone;
        for (int i = 0; i < zoneobserved.Count; i++)
        {
            m_screenlocations.Get(zoneobserved[i], out isfound, out zone);
            if (isfound)
            {
                m_screenObserved.AddScreenZone(zone);
            }
        }
    }
}
