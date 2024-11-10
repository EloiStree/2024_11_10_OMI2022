using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MonitorsAliasRegister : MonoBehaviour
{

    public WindowMonitorsMono m_windowMonitor;
    public List<MonitorAliasBaseOnRegex> m_regexToAlias = new List<MonitorAliasBaseOnRegex>();
    public List<MonitorAliasWithDirection> m_directionToAlias = new List<MonitorAliasWithDirection>();

    public List<MonitorShortcutNameToIdName> m_monitorShortcut = new List<MonitorShortcutNameToIdName>();
    public void RefreshMonitorTarget() {

        m_monitorShortcut.Clear();
        m_windowMonitor.Refresh();


        foreach (var item in m_directionToAlias)
        {
            m_windowMonitor.Get(item.m_monitorReadDirection, item.m_monitorIndex, out bool found, out string screenName);
            if (found)
                m_monitorShortcut.Add(new MonitorShortcutNameToIdName(item.m_aliasName, screenName));
        }

        m_windowMonitor.GetDisplayNames(out string[] displayNames);
        foreach (var item in m_regexToAlias)
        {
            foreach (var name in displayNames)
            {
                if(Regex.IsMatch(name, item.m_regexName))
                    m_monitorShortcut.Add(new MonitorShortcutNameToIdName(item.m_aliasName, name));

            }

            

        }


    }

    internal void Clear()
    {
        m_regexToAlias.Clear();
        m_directionToAlias.Clear();
    }

   

    [System.Serializable]
    public class MonitorAliasBaseOnRegex
    {
        public string m_regexName = "";
        public string m_aliasName = "";

        public MonitorAliasBaseOnRegex(string aliasName, string regexName)
        {
            m_regexName = regexName;
            m_aliasName = aliasName;
        }
    }

    [System.Serializable]
    public class MonitorAliasWithDirection
    {
        public string m_aliasName = "";
        public int m_monitorIndex = 0;
        public WindowMonitorsMono.MonitorDirection m_monitorReadDirection ;

        public MonitorAliasWithDirection(string aliasName, int monitorIndex, WindowMonitorsMono.MonitorDirection monitorReadDirection)
        {
            m_aliasName = aliasName;
            m_monitorIndex = monitorIndex;
            m_monitorReadDirection = monitorReadDirection;
        }
    }
    [System.Serializable]
    public class MonitorShortcutNameToIdName
    {
        public string m_shortcutName = "";
        public string m_idName = "";

        public MonitorShortcutNameToIdName(string shortcutName, string idName)
        {
            m_shortcutName = shortcutName;
            m_idName = idName;
        }
    }



}
