using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_TimeOfDayToCommands : MonoBehaviour
{
    public TimedOfDayMacroRegister m_timeMacros;
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
              
                Load(File.ReadAllText(filePath[i]));
               
            }
        }
    }

    private void Load(string textToLoad)
    {
        bool hasfound = false;
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        List<string> commands = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() >1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
            {
                string command="";
                string condition="";
                if (tokens.GetCount() == 3)
                {
                    command = tokens.GetValue(2);
                    condition = tokens.GetValue(1);
                }
                if (tokens.GetCount() == 2)
                {

                    command = tokens.GetValue(1);
                }

                List<string> commandtmp = tokens.GetAsList();
                if(commandtmp.Count>0)
                    commandtmp.RemoveAt(0);

                string context = tokens.GetValue(0).Trim().ToLower();
                TimeOfDayFrequence frequence = TimeOfDayFrequence._1Minute;
                if (context.StartsWith("every"))
                {
                    hasfound = false;
                    context = context.Substring(5).Trim();
                  
                    
                     if (context.IndexOf("15 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._15Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("5 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._5Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("10 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._10Minutes;
                        hasfound = true;
                    }
                   
                    else if (context.IndexOf("20 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._20Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("30 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._30Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("2 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._2Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("4 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._4Minutes;
                        hasfound = true;
                    }
                    else if (context.IndexOf("1 minute") > -1)
                    {
                        frequence = TimeOfDayFrequence._1Minute;
                        hasfound = true;
                    }
                    else if (context.IndexOf("1 hour") > -1)
                    {
                        frequence = TimeOfDayFrequence._1Hour;
                        hasfound = true;
                    }
                    else if (context.IndexOf("2 hour") > -1)
                    {
                        frequence = TimeOfDayFrequence._2Hours;
                        hasfound = true;
                    }
                    else if (context.IndexOf("3 hour") > -1)
                    {
                        frequence = TimeOfDayFrequence._3Hours;
                        hasfound = true;
                    }
                    else if (context.IndexOf("4 hour") > -1)
                    {
                        frequence = TimeOfDayFrequence._4Hours;
                        hasfound = true;
                    }
                    else if (context.IndexOf("6 hour") > -1)
                    {
                        frequence = TimeOfDayFrequence._6Hours;
                        hasfound = true;
                    }
                    else if (context.IndexOf("30 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._30Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("20 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._20Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("10 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._10Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("5 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._5Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("4 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._4Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("3 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._3Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("2 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._2Seconds;
                        hasfound = true;
                    }
                    else if (context.IndexOf("1 second") > -1)
                    {
                        frequence = TimeOfDayFrequence._1Second;
                        hasfound = true;
                    }



                    if (hasfound)
                        m_timeMacros.AddFrequence(frequence,condition, commandtmp.ToArray());


                    //Every 1 minute + (t)♦
                    //Every 2 minutes+ (t) ♦
                    //Every 3 minutes + (t)♦
                    //Every 4 minutes + (t)♦
                    //Every 5 minutes + (t)♦
                    //Every 10 minutes + (t)♦
                    //Every 15 minutes + (t)♦
                    //Every 20 minutes + (t)♦
                    //Every 30 minutes + (t)♦
                    //Every 1 hour + (t)♦
                    //Every 2 hour + (t)♦
                    //Every 3 hour + (t)♦
                    //Every 4 hour + (t)♦
                    //Every 6 hour + (t)♦

                }
                if (context.StartsWith("at"))
                {
                    //At 23:00♦
                    //At 12h 30m 50s 500ms♦

                    context = context.Substring(2).Trim();
                    int hour=0, minute = 0, second = 0, millisecond = 0; 
                    hasfound = false;

                    string[] commatoken = context.Split(':');
                    if (commatoken.Length == 2)
                    {
                        int.TryParse(commatoken[0].Trim(), out hour);
                        int.TryParse(commatoken[1].Trim(), out minute);
                        hasfound = true;
                    }
                    else if (commatoken.Length == 3)
                    {
                        int.TryParse(commatoken[0].Trim(), out hour);
                        int.TryParse(commatoken[1].Trim(), out minute);
                        int.TryParse(commatoken[2].Trim(), out second);
                        hasfound = true;
                    }
                    else if (commatoken.Length == 4)
                    {
                        int.TryParse(commatoken[0].Trim(), out hour);
                        int.TryParse(commatoken[1].Trim(), out minute);
                        int.TryParse(commatoken[2].Trim(), out second);
                        int.TryParse(commatoken[3].Trim(), out millisecond);
                        hasfound = true;
                    }

                    string[] spacetoken = context.Split(' ');
                    for (int j = 0; j < spacetoken.Length; j++)
                    {
                            string tt = spacetoken[j];
                        if (tt.Length > 0 && tt[tt.Length - 1] == 'h')
                        {

                            int.TryParse(tt.Replace("h","").Trim(), out hour);
                            hasfound = true;
                        }
                        if (tt.Length > 0 && tt[tt.Length - 1] == 'm')
                        {
                            int.TryParse(tt.Replace("m", "").Trim(), out minute);
                            hasfound = true;

                        }
                        if (tt.Length > 1 && tt[tt.Length - 2] != 'm' && tt[tt.Length - 1] == 's')
                        {
                            int.TryParse(tt.Replace("s", "").Trim(), out second);
                            hasfound = true;

                        }
                        if (tt.Length > 1 && tt[tt.Length - 2] == 'm' && tt[tt.Length-1] == 's')
                        {

                            int.TryParse(tt.Replace("ms", "").Trim(), out millisecond);
                            hasfound = true;
                        }
                    }

                    if(hasfound)
                    m_timeMacros.AddAt(hour, minute, second, millisecond, condition, commandtmp.ToArray());
                }

            }

        }
        
    }
}
