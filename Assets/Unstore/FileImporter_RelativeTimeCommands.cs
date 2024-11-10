using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_RelativeTimeCommands : MonoBehaviour
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
        string name="", callId="";

        List<RelativeTimeCommand> relativeMacro = new List<RelativeTimeCommand>();

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
                    name = v.Substring(5).Trim();
                }
                if (v.ToLower().StartsWith("☗callid"))
                {
                    callId = v.Substring(7).Trim();
                }
            }
            if (tokens.GetCount() == 2)
            {
                try
                {
                    if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim())    )
                    {
                        
                        string[] timetokens = tokens.GetValue(0).Trim().Split(' ');

                        ulong beforeMs = 0; 
                        ulong afterMs = 0; 
                        foreach (var timetoken in timetokens)
                        {
                            if (timetoken.Length > 2) {
                                if (timetoken[0] == '↑')
                                {
                                    beforeMs = ulong.Parse(timetoken.Replace("↑", "").Trim());
                                }

                                else if (timetoken[0] == '↓') {
                                    afterMs = ulong.Parse(timetoken.Replace("↓", "").Trim());
                                }
                            
                            }
                        }
                        string jomi = tokens.GetValue(1).Trim();
                        relativeMacro.Add(new RelativeTimeCommand(beforeMs, afterMs, jomi));

                    }

                }
                catch (Exception) { }
            }

        }

        TimedCommandLines timeMacro = new TimedCommandLines(name, callId);
        ulong timerInMs = 0;
        for (int i = 0; i < relativeMacro.Count; i++)
        {
            timerInMs += relativeMacro[i].m_beforeMs;
            timeMacro.AddKey(timerInMs, relativeMacro[i].m_commandline);
            timerInMs += relativeMacro[i].m_afterMs;
        }

        found.Add(timeMacro);
    }

    public class RelativeTimeCommand {

        public ulong m_beforeMs;
        public ulong m_afterMs;
        public string m_commandline;

        public RelativeTimeCommand(ulong beforeMs, ulong afterMs, string commandline)
        {
            this.m_beforeMs = beforeMs;
            this.m_afterMs = afterMs;
            this.m_commandline = commandline;
        }
    }
}