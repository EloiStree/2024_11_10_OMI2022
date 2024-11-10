using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_SequenceCommandLines : MonoBehaviour
{

    public TimedMacroRegister m_timeMacros;
    public void Clear()
    {
        m_timeMacros.Clear();
    }

    public void LoadFiles(params string[] filePath)
    {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<TimedCommandLines> found;
                Load(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_timeMacros.Add(found[y]);
                }
            }
        }
    }
    private void Load(string textToLoad, out List<TimedCommandLines> found)
    {
        found = new List<TimedCommandLines>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;

        TimedCommandLines macro = new TimedCommandLines();
        long m_delaybetweenlines=200;
        List<string> commands = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
            {
                string v = tokens.GetValue(0).Trim();
                if(v.ToLower().StartsWith("☗name"))
                {
                    macro.m_name = v.Substring(5).Trim();
                }
                else if(v.ToLower().StartsWith("☗callid"))
                {
                    macro.m_callId = v.Substring(7).Trim();
                }
                else if(v.ToLower().StartsWith("☗delay"))
                {

                    long.TryParse( v.Substring(6).Trim(),out m_delaybetweenlines) ;
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
                        {
                            commands.Add(tokens.GetValue(0).Trim());
                        }

                    }
                    catch (Exception) { }
                }
            }

        }
        for (long i = 0; i < commands.Count; i++)
        {
            macro.AddKey((ulong)(m_delaybetweenlines * i), commands[(int)i]);
        }
        found.Add(macro);
    }

}
