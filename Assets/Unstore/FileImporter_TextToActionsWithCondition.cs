using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_TextToActionsWithCondition : MonoBehaviour
{
    public TextToCommandLinesWithCondition m_textToAction;

    public void Clear() {
        m_textToAction.Clear();
    }

    public void LoadFiles(params string[] paths) {

        for (int i = 0; i < paths.Length; i++)
        {
            List<TextToActionsWithCondition> found;
            LoadFilesCondition(File.ReadAllText(paths[i]), out found);
            for (int j = 0; j < found.Count; j++)
            {
                m_textToAction.Add(found[j]);

            }

        }
    }

    private void LoadFilesCondition(string text, out List<TextToActionsWithCondition> found)
    {
        found = new List<TextToActionsWithCondition>();

        List<TileLine> tileLines;
        FileTileUtility.GetTile(text, out tileLines, true);

        for (int i = 0; i < tileLines.Count; i++)
        {
            if (tileLines[i].GetCount() >= 3) {
                string triggerText = tileLines[i].GetValue(0).Trim();
                string condition = tileLines[i].GetValue(1).Trim();
                
                ClassicBoolState cnd;
                if (TextToBoolStateMachineParser.IsClassicParse(condition, out cnd)) {}
        
                List<ICommandLine> cmds = new List<ICommandLine>();
                for (int j = 2; j < tileLines[i].GetCount(); j++)
                {
                    cmds.Add(
                        new CommandLine(
                        tileLines[i].GetValue(j).Trim())
                        );

                }
                found.Add(new TextToActionsWithCondition(triggerText, cnd, cmds.ToArray()));
            }

        }

    }
}
