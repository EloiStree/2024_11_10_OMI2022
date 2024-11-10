using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_ReimportCommands : MonoBehaviour
{
    public GroupOfCommandLineMono m_atReimport, m_afterReimport;
    public GroupOfCommandLineMono m_atFirstReimport, m_afterFirstReimport;


    public void Clear()
    {
        m_atReimport.Clear();
        m_afterReimport.Clear();
        m_atFirstReimport.Clear();
        m_afterFirstReimport.Clear();
    }

    public void LoadFiles(params string[] filePath)
    {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<IpSingleAlias> alias;
                List<AliasToGroupOfAlias> groupofalias;
                Load(File.ReadAllText(filePath[i]));
              
            }
        }
    }
    private void Load(string textToLoad)
    {
       
        string[] lines = textToLoad.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            try
            {
                if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                    continue;
                if (lines[i].IndexOf("☗BeforeFirstImport") == 0)
                {
                    m_atFirstReimport.Add(lines[i].Substring("☗BeforeFirstImport".Length));
                }
                if (lines[i].IndexOf("☗AfterFirstImport") == 0)
                {
                    m_afterFirstReimport.Add(lines[i].Substring("☗AfterFirstImport".Length));
                }
                if (lines[i].IndexOf("☗BeforeReload") == 0)
                {
                    m_atReimport.Add(lines[i].Substring("☗BeforeReload".Length));
                }
                if (lines[i].IndexOf("☗AfterReload")==0)
                {
                    m_afterReimport.Add(lines[i].Substring("☗AfterReload".Length));
                }
            }
            catch (Exception) { }

        }
    }
}
