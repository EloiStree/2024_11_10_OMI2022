using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static WindowMonitorsMono;

public class FileImport_MonitorsNaming : MonoBehaviour
{

    public MonitorsAliasRegister m_monitorRegister;

    public void ClearRegister()
    {
        m_monitorRegister.Clear();

    }

  

    public void Load(params string[] filePath)
    {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                    ImportMonitors(File.ReadAllText(filePath[i]));
            }
        }
        m_monitorRegister. RefreshMonitorTarget();
    }

    private void ImportMonitors(string text)
    {
       


        string[] lines = text.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            //MPK Mini Play♦Shorten♦6321♦FootSustain♦SetFalse
            if (tokens.GetCount() == 3) {
            

                MonitorDirection direction = MonitorDirection.None;
                string type = tokens.GetValue(0).Trim().ToLower();
                string name = tokens.GetValue(2).Trim();
                int index = 0;
                if (int.TryParse(tokens.GetValue(1), out index)) {

                    if (type == "→") direction = MonitorDirection.Left2Right;
                    else if (type == "←") direction = MonitorDirection.Right2Left;
                    else if (type == "↑") direction = MonitorDirection.Bot2Top;
                    else if (type == "↓") direction = MonitorDirection.Top2Bot;
                    else if (type == "↗") direction = MonitorDirection.FromBotLeft;
                    else if (type == "↖") direction = MonitorDirection.FromBotRight;
                    else if (type == "↘") direction = MonitorDirection.FromTopLeft;
                    else if (type == "↙") direction = MonitorDirection.FromTopRight;

                    else if (Eloi.E_StringUtility.AreEquals("left2right", type)
                        || Eloi.E_StringUtility.AreEquals("l2r", type))
                        direction = MonitorDirection.Left2Right;
                    else if (Eloi.E_StringUtility.AreEquals("right2left", type)
                        || Eloi.E_StringUtility.AreEquals("r2l", type))
                        direction = MonitorDirection.Right2Left;
                    else if (Eloi.E_StringUtility.AreEquals("bot2top", type)
                        || Eloi.E_StringUtility.AreEquals("d2u", type)
                        || Eloi.E_StringUtility.AreEquals("b2t", type))
                        direction = MonitorDirection.Bot2Top;
                    else if (Eloi.E_StringUtility.AreEquals("top2bot", type)
                        || Eloi.E_StringUtility.AreEquals("u2d", type)
                        || Eloi.E_StringUtility.AreEquals("t2b", type))
                        direction = MonitorDirection.Top2Bot;
                    m_monitorRegister.m_directionToAlias.Add(new MonitorsAliasRegister.MonitorAliasWithDirection(name, index, direction));
                }
                		
            }
            if (tokens.GetCount() == 2)
            {
                string regex = tokens.GetValue(0).Trim();
                string aliasName = tokens.GetValue(1).Trim();
                m_monitorRegister.m_regexToAlias.Add(new MonitorsAliasRegister.MonitorAliasBaseOnRegex( aliasName, regex));

            }




        }

    }
}
