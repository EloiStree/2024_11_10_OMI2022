using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class FileImporter_NetworkUDPSetting : MonoBehaviour
{
    public UDPThreadSender m_senderToSet;

    // Start is called before the first frame update
    void Start()
    {
        m_senderToSet.ResetTheTargetFromAlias();


    }

    public void Clear()
    {
        m_senderToSet.Clear();
    }

    public void LoadFiles(params string[] filePath)
    {

        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<IpSingleAlias> alias;
                List<AliasToGroupOfAlias> groupofalias;
                Load(File.ReadAllText(filePath[i]), out alias, out groupofalias);
                for (int y = 0; y < alias.Count; y++)
                {

                    m_senderToSet.Add(alias[y]);
                }
                for (int y = 0; y < groupofalias.Count; y++)
                {

                    m_senderToSet.Add(groupofalias[y]);
                }
            }
        }
        m_senderToSet.ResetTheTargetFromAlias();
    }
   
    string ipv4regex = "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    private void Load(string textToLoad, out List<IpSingleAlias> alias,
         out List<AliasToGroupOfAlias> groupofalias)
    {
        alias = new List<IpSingleAlias>();
        groupofalias = new List<AliasToGroupOfAlias>();
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
            bool isipv4format = tokens.GetCount() == 3 && Regex.IsMatch(tokens.GetValue(1), ipv4regex);
                if (isipv4format)
                {
                    int port = 0;
                    if (int.TryParse(tokens.GetValue(2), out port)) {

                        alias.Add(new IpSingleAlias(tokens.GetValue(0).Trim().ToLower(),
                            tokens.GetValue(1).Trim().ToLower(),
                            port));
                    }
                }
                else {
                    List<string> v = new List<string>();
                    for (int j = 1; j < tokens.GetCount(); j++)
                    {
                        v.Add(tokens.GetValue(j).ToLower().Trim());
                    }

                    groupofalias.Add(new AliasToGroupOfAlias(tokens.GetValue(0).Trim().ToLower(),v.ToArray()));
                
                }
            }
            catch (Exception ) { }

        }
    }
}
