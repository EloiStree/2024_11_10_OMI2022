using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileNameToStringActionMono : MonoBehaviour
{

    public List<FileToFound> m_fileToActions;
    public Eloi.PrimitiveUnityEvent_StringArray m_groupOfActionsFound;
    [System.Serializable]
    public class FileToFound {
        public string m_textToFound;
        public bool m_ignoreCase=true;
        public bool m_trim = true;

        public List<string> m_stringActions = new List<string>();
    }
    public void TryTriggerActonBasedOnFileName(string filePath)
    {
        TryTriggerActonBasedOnFileName(new MetaAbsolutePathFile(filePath));
    }

    public void TryTriggerActonBasedOnFileName(Eloi.IMetaAbsolutePathFileGet file) {
        Eloi.E_FileAndFolderUtility.GetFileNameFrom(in file, out Eloi.IMetaFileNameWithExtensionGet fileName);
        fileName.GetFileNameWithoutExtension(out string filename);
        for (int i = 0; i < m_fileToActions.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(in filename, in m_fileToActions[i].m_textToFound, in m_fileToActions[i].m_ignoreCase, in m_fileToActions[i].m_trim))
            {
                m_groupOfActionsFound.Invoke(m_fileToActions[i].m_stringActions.ToArray());
            }
        }

    }
    
}
