using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_TimedCommandLines : MonoBehaviour
{

    public TimedMacroRegister m_timeMacros;
    public void Clear() {
        m_timeMacros.Clear();
    }

    public void LoadFiles(params string[] filePath) {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<TimedCommandLines> found;
                LoadTimedcommandLines(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_timeMacros.Add(found[y]);
                }
            }
        }
    }
    private void LoadTimedcommandLines(string textToLoad, out List<TimedCommandLines> found)
    {
        found = new List<TimedCommandLines>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        string name, callId, description;

        TimedCommandLines macro = new TimedCommandLines();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
            {
                string v = tokens.GetValue(0).Trim();
                if (v.ToLower().StartsWith("☗name"))
                {
                    macro.m_name = v.Substring(5).Trim();
                }
                if (v.ToLower().StartsWith("☗callid"))
                {
                    macro.m_callId = v.Substring(7).Trim();
                }
            }
            if (tokens.GetCount() == 2)
            {
                try
                {
                    if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
                    {
                        ulong ms = ulong.Parse(tokens.GetValue(0).Trim());
                        string jomi = tokens.GetValue(1).Trim();
                        macro.AddKey(ms, jomi);

                    }

                }
                catch (Exception) { }
            }

        }
        found.Add(macro);
    }

}
