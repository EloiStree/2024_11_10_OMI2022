using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectMultiSourceTextAppendObserverMono : MonoBehaviour
{
    public  MultiSourceTextAppendObserverMono m_target;


    public void ReadAndPush(ObservedFileEvent fileEvent)
    {
        fileEvent.GetFileReference(out Eloi.IMetaAbsolutePathFileGet file);
        StartCoroutine(Eloi.E_FileAndFolderUtility.LoadFileWithCoroutine(file, (string text) => Push(file, text)));
    }
  

    public void Push(Eloi.IMetaAbsolutePathFileGet fileId, string text) {
        fileId.GetPath(out string path);
        m_target.PushNewText(path, text, true);
    }

    public void Push(string id, string text)
    {
        m_target.PushNewText(id, text, true);
    }
}
