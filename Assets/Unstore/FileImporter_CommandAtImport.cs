using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_CommandAtImport : MonoBehaviour
{

    public GroupOfCommandLineMono m_groupOfLines;

    public void Clear() {
        m_groupOfLines.Clear();
    }


    public void LoadFile(string filetext, bool flushBeforeLoad=true)
    {

        if (flushBeforeLoad)
            m_groupOfLines.Clear();
        List<TileLine> tileLines;
        FileTileUtility.GetTile(filetext, out tileLines);
        //foreach (TileLine tokens in tileLines)
        //{
        //    int count = tokens.GetCount();
        //    if (count > 1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
        //    {
        //        string[] commands = new string[count - 1];
        //        for (int i = 0; i < commands.Length; i++)
        //        {
        //            commands[i] = tokens.GetValue(i + 1).Trim();
        //        }
        //        StringToCommands rc = new StringToCommands(tokens.GetValue(0).Trim(), commands);
        //        m_groupOfLines.Add(rc);

        //    }
        //}

    }

    public void LoadFiles(params string[] pathOfFiles)
    {

        for (int i = 0; i < pathOfFiles.Length; i++)
        {
            if (pathOfFiles[i] != null && File.Exists(pathOfFiles[i]))
                LoadFile(File.ReadAllText(pathOfFiles[i]), false);
        }
    }

}
