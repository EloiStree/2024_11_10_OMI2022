using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CoroutineReadFilesContentToStringActionMono : MonoBehaviour
{
    public PrimitiveUnityEvent_StringArray m_lineLoadedFromFile;
    public void TryTriggerActonBasedOnFileName(string filePath)
    {
        TryTriggerActonBasedOnFileName(new MetaAbsolutePathFile(filePath));
    }

    [ContextMenu("Test")]
    public void Test() {
        TryTriggerActonBasedOnFileName("C:\\Users\\moi\\Dropbox\\FileDropping\\Order 66.txt");
    }

    public void TryTriggerActonBasedOnFileName( ObservedFileEvent fileEvent){
        if (fileEvent != null)
            TryTriggerActonBasedOnFileName(fileEvent.m_fileReference);
    }
    public void TryTriggerActonBasedOnFileName(Eloi.IMetaAbsolutePathFileGet file)
    {
        StartCoroutine(TryTriggerActonBasedOnFileNameCoroutine(file));
    }
    public IEnumerator  TryTriggerActonBasedOnFileNameCoroutine(Eloi.IMetaAbsolutePathFileGet file)
    {
        if (file == null)
            yield break;
        yield return Eloi.E_FileAndFolderUtility.LoadFileWithCoroutine(file, PushTextAsLine);
      
    }

    public void PushTextAsLine(string text) {
        m_lineLoadedFromFile.Invoke(text.Split('\n'));
    }
}

