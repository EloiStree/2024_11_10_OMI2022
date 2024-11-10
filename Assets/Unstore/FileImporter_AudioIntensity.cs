using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileImporter_AudioIntensity : MonoBehaviour
{
    public MicInputObserverRanges m_rangesObserved;
    public MicInputRangeToBoolean m_observedToBoolean;


    public void Clear()
    {
        m_observedToBoolean.Clear();
        m_rangesObserved.Clear();
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

        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            try
            {
                if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                    continue;
               
                tokens = new TileLine(lines[i]);
                if (tokens.GetCount() == 4) { 
                    string deviceName= tokens.GetValue(0).Trim();
                    string booleanName= tokens.GetValue(1).Trim();
                    float minValue = float.Parse(tokens.GetValue(2).Trim());
                    float maxValue = float.Parse(tokens.GetValue(3).Trim());
                    m_observedToBoolean.Add(deviceName, booleanName, minValue, maxValue);
                }
            }
            catch (Exception e) { Debug.Log("T:"+e.StackTrace); }

        }
    }
}
