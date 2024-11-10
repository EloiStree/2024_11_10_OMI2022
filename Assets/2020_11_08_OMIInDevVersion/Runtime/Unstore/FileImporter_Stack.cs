using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_Stack : MonoBehaviour
{
    public StackerRegisterMono m_stackRegister;



    public void ClearRegister()
    {
        m_stackRegister.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<NamedCommandLineStackerRef> found;
                LoadStackerFromFile(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_stackRegister.Add(found[y].m_name,found[y].m_stacker);
                }

            }
        }
    }
    private void LoadStackerFromFile(string textToLoad, out List<NamedCommandLineStackerRef> found)
    {

        found = new List<NamedCommandLineStackerRef>();

        List<TileLine> tileLines;
        FileTileUtility.GetTile(textToLoad, out tileLines);
        foreach (TileLine tokens in tileLines)
        {
            int count = tokens.GetCount();
            if (count > 1 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()))
            {
                string[] commands = new string[count - 1];
                for (int i = 0; i < commands.Length; i++)
                {
                    
                    commands[i] = tokens.GetValue(i + 1).Trim();
                }
                CommandLineStacker rc = new CommandLineStacker( commands);
                found.Add(new NamedCommandLineStackerRef()
                {
                    m_name = tokens.GetValue(0).Trim(),
                    m_stacker= rc
                });

            }
        }
    }
}
