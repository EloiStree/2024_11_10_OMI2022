using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class FileChangeObserverMono : MonoBehaviour
{
    public UnityEvent m_onFileChanged;
    public FileChangeObserver m_fileObserved;
    public string m_lastModification;


    public IEnumerator Start()
    {
        while (true) {
            yield return new WaitForSeconds(1);
            m_lastModification=m_fileObserved.m_dateTime.ToString();
            if (m_fileObserved.HasChanged(true)) {
                m_onFileChanged.Invoke();
            }
        }
    }

}

[System.Serializable]
public class FileChangeObserver
{
    public string m_fileAbsolutePath;
    public DateTime m_dateTime;
    public bool m_wasExistingAtLastRefresh = false;
    public string m_debugLastModification;

    public FileChangeObserver(string fileAbsolutePath)
    {
        SetNewFilePath( fileAbsolutePath);
    }

    public bool IsFileDefined() {
        if (string.IsNullOrEmpty(m_fileAbsolutePath)) return false;
        if (!File.Exists(m_fileAbsolutePath)) return false;
        return true;
    }

    public void SetNewFilePath(string fileAbsolutePath)
    {
        m_fileAbsolutePath = fileAbsolutePath;
        RefreshInformation();
    }
    public string GetUseFilePath() {
        return m_fileAbsolutePath;
    }
    public void GetModificationTime(out bool isValide, out DateTime modificationTime) {
        isValide = false;
        modificationTime = DateTime.Now;
        if (!IsFileDefined()) return;
        isValide = true;
        modificationTime= File.GetLastWriteTime(m_fileAbsolutePath);
    }

    public bool HasChanged(bool andRefreshDateOfCheck) {
        DateTime newTime;
        bool isValide;
        bool fileExist= Exists();
        bool hasChange = false;
        if (m_wasExistingAtLastRefresh != fileExist)
        {
            hasChange = true;
            if (andRefreshDateOfCheck)
                RefreshInformation();
        }
        else { 
            GetModificationTime(out isValide, out newTime);
            if (isValide && newTime != m_dateTime) { 
            
                hasChange = true;
                if (andRefreshDateOfCheck)
                    RefreshInformation();
            }
        }
        return hasChange;
    }

    public string GetNameWithoutExtension()
    {
        return Path.GetFileNameWithoutExtension(GetUseFilePath());
    }

    public void OpenFile()
    {
        Application.OpenURL(GetUseFilePath());
    }

    public void OpenDirectory()
    {
       Application.OpenURL(Path.GetDirectoryName( GetUseFilePath()));
    }

    public bool Exists()
    {
       return  File.Exists(GetUseFilePath());
    }

    public void RefreshInformation() {

        m_wasExistingAtLastRefresh = Exists() ;
        DateTime newTime;
        bool isValide;
        GetModificationTime(out isValide, out newTime);
        if(isValide)
            m_dateTime = newTime;

    }

}

