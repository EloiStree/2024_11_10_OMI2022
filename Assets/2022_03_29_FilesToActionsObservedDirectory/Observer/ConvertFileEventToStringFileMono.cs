using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertFileEventToStringFileMono : MonoBehaviour
{
    public Eloi.PrimitiveUnityEvent_String m_filePathEvent;

    public void Push(ObservedFileEvent fileEvent) {
        if (fileEvent == null)
            return;
        fileEvent.GetFileReference(out Eloi.IMetaAbsolutePathFileGet fp);
        if (fp == null)
            return;
        fp.GetPath(out string sfp); 
        if (sfp == null)
            return;
        m_filePathEvent.Invoke(sfp);
    }
}
