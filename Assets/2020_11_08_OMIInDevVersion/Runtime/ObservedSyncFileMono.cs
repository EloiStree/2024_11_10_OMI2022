using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObservedSyncFileMono : MonoBehaviour
{

   public  ObservedSyncFile m_fileInfo;
    public float m_delayToCheck=1;

    private IEnumerator Start()
    {
        while (true) {

            yield return new WaitForSeconds(m_delayToCheck);
            m_fileInfo.CheckForFileChange();
            
        }
    }

}

[System.Serializable]
public class ObservedSyncFile {

    public SyncFile m_syncFileInfo;
    public FileChangeObserver m_fileObserved;
    public FileChangedEvent m_fileChangeListener;
    public void Init()
    {
        if(!m_syncFileInfo.IsLocalFileExist())
            m_syncFileInfo.CreateFileFromWebOrUnityIfNotAccess();
        m_fileObserved = new FileChangeObserver(m_syncFileInfo.GetFilePath());
        m_fileObserved.RefreshInformation();
    }

    public void StartListeningToChange(UnityAction<string> listener)
    {
        m_fileChangeListener.AddListener(listener);
    }
    public void StopListeningToChange(UnityAction<string> listener)
    {
        m_fileChangeListener.RemoveListener(listener);
    }


    public void CheckForFileChange() {

        if (!m_syncFileInfo.IsLocalFileExist())
            Init();
        if (m_fileObserved.HasChanged(true))
        {
            SendNewFilesToListener();
        }

    }

    private void SendNewFilesToListener()
    {
        bool hasFile;
        string text;
        m_syncFileInfo.GetFileFromComputer(out text, out hasFile);
        if (hasFile)
            m_fileChangeListener.Invoke(text);
    }

    public void OpenDirectory() { m_fileObserved.OpenDirectory(); }
    public void OpenFile() { m_fileObserved.OpenFile(); }

    public void ForceChangeNotification()
    {
        SendNewFilesToListener();
    }

}

