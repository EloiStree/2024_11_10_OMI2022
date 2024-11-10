using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;




public class FileImporter_RegexToCommands : MonoBehaviour
{
    public RegexToCommandsRegisterMono m_regexRegister;

    public void ClearRegister() {
        m_regexRegister.Clear();
    }
    public void LoadFile(string[] paths) {
        for (int i = 0; i < paths.Length; i++)
        {
            if (File.Exists(paths[i]))
                LoadFile(File.ReadAllText(paths[i]));

        }
    }
    public void LoadFile(string filetext) {

        List<TileLine> tileLines;
        FileTileUtility.GetTile(filetext, out tileLines);
        foreach (TileLine tokens in tileLines)
        {
            if (tokens.GetCount() >1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()) )
            {
                string[] commands = new string[tileLines.Count-1];
                for (int i = 1; i < tileLines.Count; i++)
                {
                    commands[i-1] = tileLines[i].GetValue(i);
                }
                RegexLinkedToCommandLines rc = new RegexLinkedToCommandLines(tokens.GetValue(0),commands);
                m_regexRegister.AddRegexToCommands(rc);

            }
        }
       
    }
   
}
