using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchAndPushShortcutFromRelativeFileMono : MonoBehaviour
{
    public Eloi.AbstractMetaAbsolutePathFileMono m_tddFile;
    public ShortcutsGroupEvent m_linesFound;
    public bool m_loadAtStart;

    void Start()
    {
        if (m_loadAtStart)
        {
            LoadAndPushFileAsShortcutGroup();
        }
    }

    private void LoadAndPushFileAsShortcutGroup()
    {
        Eloi.E_FileAndFolderUtility.ImportFileAsText(m_tddFile, out string text, "");
        ShortcutTextUtility.GetShortcutGroupsFromText(text, out ShortcutsGroup[] group);
        foreach (var line in group)
        {
            m_linesFound.Invoke(line);
        }
    }
}
