using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_Loop : MonoBehaviour
{

    public LooperRegisterAndHandlerMono m_loopHandler;

    public void ClearRegister()
    {
        m_loopHandler.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<NamedLooperBean> found;
                LoadLoopOfFile(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {
                    m_loopHandler.Create(found[y]);
                }

            }
        }
    }
    private void LoadLoopOfFile(string textToLoad, out List<NamedLooperBean> found)
    {

        found = new List<NamedLooperBean>();

        List<TileLine> tileLines;
        FileTileUtility.GetTile(textToLoad, out tileLines);
        foreach (TileLine tokens in tileLines)
        {
            int count = tokens.GetCount();
            if (count > 2 )
            {
                string name = tokens.GetValue(0).Trim();
                uint millisecond;
                if (uint.TryParse(tokens.GetValue(1).Trim(), out millisecond)) { 
                    if (name.Length > 0 && millisecond > 0) { 
                        List<CommandLine> cl = new List<CommandLine>();
                        for (int i = 2; i < count ; i++)
                        {
                            cl.Add(new CommandLine(tokens.GetValue(i).Trim())); 
                        }
                        found.Add(new NamedLooperBean()
                        {
                            m_name = name,
                            m_timeBetweenInMs = millisecond,
                            m_linkedAction= cl
                        }) ;

                    }
                }
            }
        }

    }
}
