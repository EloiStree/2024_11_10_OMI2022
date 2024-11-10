using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DirectoryFilesObserverRefreshedEvent : UnityEvent<AbstractDirectoryFilesObserver> { }

[System.Serializable]
public abstract class AbstractDirectoryFilesObserver 
{
    public NewFileDetectedUnityEvent m_onNewFiles = new NewFileDetectedUnityEvent();
    public ExistingFileChangedUnityEvent m_onFilesChanged = new ExistingFileChangedUnityEvent();
    public DeletedFileDetectedUnityEvent m_onDeletedFile = new DeletedFileDetectedUnityEvent();
    public DirectoryFilesObserverRefreshedEvent m_onRefreshed = new DirectoryFilesObserverRefreshedEvent();


    protected void NotifyNewFileDetected(string filePath)
    {
        NotifyNewFileDetected(new NewFileDetectedEvent(filePath));
    }
    protected void NotifyFileChanged(string filePath)
    {
        NotifyFileChanged(new ExistingFileChangedEvent(filePath));
    }
    protected void NotifyDeleteFileDetected(string filePath)
    {
        NotifyDeleteFileDetected(new DeletedFileDetectedEvent(filePath));
    }

    protected void NotifyNewFileDetected(NewFileDetectedEvent fileEvent)
    {
        m_onNewFiles.Invoke(fileEvent);
    }
    protected void NotifyFileChanged(ExistingFileChangedEvent fileEvent)
    {

        m_onFilesChanged.Invoke(fileEvent);
    }
    protected void NotifyDeleteFileDetected(DeletedFileDetectedEvent fileEvent)
    {
        m_onDeletedFile.Invoke(fileEvent);

    }
}

[System.Serializable]
public class  RelayDirectoryFilesObserver: AbstractDirectoryFilesObserver
{

    public void RelayNotifyNewFileDetected(NewFileDetectedEvent fileEvent)
    {
        NotifyNewFileDetected(fileEvent);
    }
    public void RelayNotifyFileChanged(ExistingFileChangedEvent fileEvent)
    {
        NotifyFileChanged(fileEvent);
    }
    public void RelayNotifyDeleteFileDetected(DeletedFileDetectedEvent fileEvent)
    {
        NotifyDeleteFileDetected(fileEvent);
    }

}

//File.GetLastWriteTime(m_fileAbsolutePath);


[System.Serializable]
public class DefaultDirectoryFilesObserver : AbstractDirectoryFilesObserver {


    public DefaultDirectoryFilesObserver(string directoryPath, bool observeChildrens)
    {
        m_observedChildrenInFolder = observeChildrens;
        if (File.Exists(directoryPath))
            directoryPath= Path.GetDirectoryName(directoryPath);
        m_directoryObserved = new Eloi.MetaAbsolutePathDirectory(directoryPath);
    }
    public DefaultDirectoryFilesObserver(Eloi.IMetaAbsolutePathDirectoryGet directory, bool observeChildrens)
    {
        m_observedChildrenInFolder = observeChildrens;
        m_directoryObserved = directory;
    }

    public Eloi.IMetaAbsolutePathDirectoryGet m_directoryObserved;
    public bool m_observedChildrenInFolder=true;

    public Dictionary<string, FileWriteChangedObserver> m_registeredFiles = new Dictionary<string, FileWriteChangedObserver>();
    public string[] m_previousFiles = new string[0];
    public string[] m_currentFiles = new string[0];
    public string[] m_deletedFiles = new string[0];
    public string[] m_stillFiles = new string[0];
    public string[] m_newFiles = new string[0];



    public void RefreshFilesState(in bool notifyEvent) {
        if (m_directoryObserved == null)
            return;
        m_directoryObserved.GetPath(out string folderPath);
        Eloi.E_CodeTag.SleepyCode.Info("Maybe it is better to let the option to create or not");
        Directory.CreateDirectory(folderPath);
        m_currentFiles = Directory.GetFiles(folderPath,"*", m_observedChildrenInFolder?SearchOption.AllDirectories: SearchOption.TopDirectoryOnly);
        m_newFiles = m_currentFiles.Except(m_previousFiles).ToArray();
        m_deletedFiles = m_previousFiles.Except(m_currentFiles).ToArray();
        m_stillFiles = m_currentFiles.Except(m_newFiles).ToArray();
        m_previousFiles = m_currentFiles;

        foreach (string p in m_newFiles)
        {
            if (!m_registeredFiles.ContainsKey(p))
            {
                m_registeredFiles.Add(p, new FileWriteChangedObserver(p));
            }
            if (notifyEvent)
                NotifyNewFileDetected(p);
        }
        foreach (string p in m_deletedFiles)
        {
            if (m_registeredFiles.ContainsKey(p))
            {
                m_registeredFiles.Remove(p);
            }
            if (notifyEvent)
                NotifyDeleteFileDetected(p);
        }

        foreach (string path in m_registeredFiles.Keys)
        {
            if (m_registeredFiles[path].HasChanged()) {
                m_registeredFiles[path].UpdateStoreObservedDate();
                if (notifyEvent)
                    base.NotifyFileChanged(path);
            }
        }
        m_onRefreshed.Invoke(this);
    }

    public void SetDirectoryTarget(string directoryPath, bool observeChildrens)
    {
        if (File.Exists(directoryPath))
            directoryPath = Path.GetDirectoryName(directoryPath);
        m_directoryObserved = new Eloi.MetaAbsolutePathDirectory(directoryPath);
        m_observedChildrenInFolder = observeChildrens;
        m_registeredFiles = new Dictionary<string, FileWriteChangedObserver>();
        m_previousFiles = new string[0];
        m_currentFiles = new string[0];
        m_deletedFiles = new string[0];
        m_stillFiles = new string[0];
        m_newFiles = new string[0];
}

    public class FileWriteChangedObserver {
        public Eloi.IMetaAbsolutePathFileGet m_observed;
        public DateTime m_previousTime;

        public  FileWriteChangedObserver(string filePath)
        {
            SetTargetAndUpdateDate(filePath);
        }
        public  FileWriteChangedObserver(Eloi.IMetaAbsolutePathFileGet filePath)
        {
            SetTargetAndUpdateDate(filePath);
        }

        public void SetTargetAndUpdateDate(string filePath)
        {
            m_observed = new Eloi.MetaAbsolutePathFile(filePath);
            UpdateStoreObservedDate();
        }
        public void SetTargetAndUpdateDate(Eloi.IMetaAbsolutePathFileGet filePath)
        {
            m_observed = filePath;
            UpdateStoreObservedDate();
        }

        public bool IsStillExisting() {
            m_observed.GetPath(out string path);
            return File.Exists(path);
        }
        public bool HasChanged() {
            m_observed.GetPath(out string path);
            DateTime fileModifyTime = File.GetLastWriteTime(path);
            return fileModifyTime > m_previousTime;
        }
        public void UpdateStoreObservedDate()
        {
            m_observed.GetPath(out string path);
            m_previousTime = File.GetLastWriteTime(path);
        }

    }
}