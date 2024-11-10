using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObservedFileEvent : I_ObservedFileEvents
{
    public IMetaAbsolutePathFileGet m_fileReference;
    public void GetFileReference(out IMetaAbsolutePathFileGet filePath)
    {
        filePath = m_fileReference;
    }

    public void SetFileReference(IMetaAbsolutePathFileGet filePath)
    {
        m_fileReference = filePath;
    }

    public ObservedFileEvent()
    {
        m_fileReference = null;
    }
    public ObservedFileEvent(string absoluteFilePath)
    {
        SetFileReference(new Eloi.MetaAbsolutePathFile(absoluteFilePath));
    }
    public ObservedFileEvent(IMetaAbsolutePathFileGet absoluteFilePath)
    {
        SetFileReference(absoluteFilePath);
    }

}


public interface I_ObservedFileEvents {
    void GetFileReference(out Eloi.IMetaAbsolutePathFileGet filePath);
    void SetFileReference(Eloi.IMetaAbsolutePathFileGet filePath);
}

public class NewFileDetectedEvent : ObservedFileEvent
{
    public NewFileDetectedEvent(string absoluteFilePath) : base(absoluteFilePath) { }
    public NewFileDetectedEvent(IMetaAbsolutePathFileGet absoluteFilePath) : base(absoluteFilePath) { }
}
public class ExistingFileChangedEvent : ObservedFileEvent
{
    public ExistingFileChangedEvent(string absoluteFilePath) : base(absoluteFilePath) { }
    public ExistingFileChangedEvent(IMetaAbsolutePathFileGet absoluteFilePath) : base(absoluteFilePath) { }
}
public class DeletedFileDetectedEvent : ObservedFileEvent {

    public DeletedFileDetectedEvent(string absoluteFilePath) : base(absoluteFilePath) { }
    public DeletedFileDetectedEvent(IMetaAbsolutePathFileGet absoluteFilePath) : base(absoluteFilePath) { }
}


[System.Serializable]
public class NewFileDetectedUnityEvent : UnityEvent<NewFileDetectedEvent> { }
[System.Serializable]
public class ExistingFileChangedUnityEvent : UnityEvent<ExistingFileChangedEvent> { }
[System.Serializable]
public class DeletedFileDetectedUnityEvent : UnityEvent<DeletedFileDetectedEvent> { }

