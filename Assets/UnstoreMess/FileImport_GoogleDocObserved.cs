using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImport_GoogleDocObserved : MonoBehaviour
{

    public GoogleDocsAccessorMono m_googleDocRegularUpdateChar;
    public GoogleDocsAccessorMono m_googleDocRegularUpdateLine;
    public GoogleDocsLoopAccessorMono m_googleDocObservers;

    public void ClearRegister()
    {
        m_googleDocRegularUpdateChar.Clear();
        m_googleDocRegularUpdateLine.Clear();
        m_googleDocObservers.Clear();

    }



    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                ImportFileText(File.ReadAllText(filePath[i]));
            }
        }
    }

    private void ImportFileText(string text)
    {
        string[] lines = text.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            //GoogleDocChar♦id
            //GoogleDocLine♦id
            if (tokens.GetCount() == 2)
            {
                if (Eloi.E_StringUtility.AreEquals(tokens.GetValue(0), "GoogleDocChar", true, true))
                {
                    m_googleDocRegularUpdateChar.AddGoogleDocToObserve(tokens.GetValue(1).Trim(), true);
                }
                else if (Eloi.E_StringUtility.AreEquals(tokens.GetValue(0), "GoogleDocLine", true, true))
                {
                    m_googleDocRegularUpdateLine.AddGoogleDocToObserve(tokens.GetValue(1).Trim(),true);
                }
            }

            //GoogleDocChar♦seconds♦id
            //GoogleDocLine♦seconds♦id
             if (tokens.GetCount() == 3)
            {
                if (Eloi.E_StringUtility.AreEquals(tokens.GetValue(0), "GoogleDocChar", true, true))
                {
                    if (int.TryParse(tokens.GetValue(1), out int seconds))
                    {
                        m_googleDocObservers.Add(tokens.GetValue(2).Trim(),  seconds);
                    }
                }
                 if (Eloi.E_StringUtility.AreEquals(tokens.GetValue(0), "GoogleDocLine", true, true))
                {
                    if (int.TryParse(tokens.GetValue(1), out int seconds))
                    {
                        m_googleDocObservers.Add(tokens.GetValue(2).Trim(),  seconds);
                    }
                }
            }

        }

    }
}
