using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SlowReadFilesContentToStringActionMono : MonoBehaviour
{
    public PrimitiveUnityEvent_StringArray m_lineLoadedFromFile;
    public void TryTriggerActonBasedOnFileName(string  filePath)
    {
        TryTriggerActonBasedOnFileName(new MetaAbsolutePathFile(filePath));
    }

    public void TryTriggerActonBasedOnFileName(Eloi.IMetaAbsolutePathFileGet file)
    {
        string path = file.GetPath();
        if (File.Exists(path)) {
            m_lineLoadedFromFile.Invoke(File.ReadAllLines(path));
        }
    }


}
