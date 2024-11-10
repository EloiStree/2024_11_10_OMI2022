using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_StringWithCommands : MonoBehaviour
{
    public StringToCommandLinesRegister m_stringToCmdsRegister; 
    public void Flush()
    {
        m_stringToCmdsRegister.Clear();
    }
    public void FlushAndLoadFile(string filetext)
    {
        LoadFile(filetext, true);
    }
    public void LoadFile(string filetext, bool flushBeforeLoad)
    {

        if(flushBeforeLoad)
        m_stringToCmdsRegister.Clear();
        List<TileLine> tileLines;
        FileTileUtility.GetTile(filetext, out tileLines);
        foreach (TileLine tokens in tileLines)
        {
            int count = tokens.GetCount();
            if (count > 1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
            {
                string[] commands = new string[count - 1];
                for (int i = 0; i < commands.Length; i++)
                {
                    commands[i] = tokens.GetValue(i+1).Trim();
                }
                StringToCommands rc = new StringToCommands(tokens.GetValue(0).Trim(), commands);
                m_stringToCmdsRegister.AddToCommands(rc);

            }
        }

    }

    public void LoadFiles(string [] pathOfFiles) {

        for (int i = 0; i < pathOfFiles.Length; i++)
        {
            if(pathOfFiles[i]!=null && File.Exists(pathOfFiles[i]))
            LoadFile(File.ReadAllText(pathOfFiles[i]), false);
        }
    }

}
