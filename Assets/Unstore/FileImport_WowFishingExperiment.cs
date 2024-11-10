using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImport_WowFishingExperiment : MonoBehaviour
{
    public Experiment_WowFishing m_wowFishing;

    public void Clear()
    {
        m_wowFishing.Clear();
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

    private void Load(string text)
    {
        string[] lines = text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;

            if (line.IndexOf("☗fishingcastcommandline") == 0)
            {
                line = line.Replace("☗fishingcastcommandline", "");
                m_wowFishing.m_fishingCastCommand = line;
            }
            else if (line.IndexOf("☗fishingtriggercastcondition") == 0)
            {
                line = line.Replace("☗fishingtriggercastcondition", "");
                m_wowFishing.SetCastCondition(  line );

            }
            else if (line.IndexOf("☗fishingactivecondition") == 0)
            {
                line = line.Replace("☗fishingactivecondition", "");
                m_wowFishing.SetActiveCondition( line);

            }
            else if (line.IndexOf("☗fishingcastpause") == 0)
            {
                line = line.Replace("☗fishingcastpause", "");
                float v = 0;
                if (float.TryParse(line.Trim(), out v)) {
                    m_wowFishing.m_pauseDetectionTimeInSec = v / 1000f;
                }

            }
            else if (line.IndexOf("☗fishingrecallduration") == 0)
            {
                line = line.Replace("☗fishingrecallduration", "");
                float v = 0;
                if (float.TryParse(line.Trim(), out v))
                {
                    m_wowFishing.m_recallCastAfterTime = v/1000f;
                }
            }


            //☗fishingcastcommandline macro:wow: collectfishing
            //☗fishingtriggercastcondition wow +fishingsound
            //☗fishingactivecondition wow +isfishing
            //☗fishingcastpause 500
            //☗fishingrecallduration 22000
        }
    }
}
