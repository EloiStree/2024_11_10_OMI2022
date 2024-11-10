using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FileToObserveRegister : MonoBehaviour
{
    public List<FileObserved> m_registered = new List<FileObserved>();
    public float m_timeBetweenChangeCheck = 3;
    public FileObservedChangeEvent m_fileObservedChanged;
    public string m_lastFileChange;

  
    public void ClearRegister() {
        m_registered.Clear();
    }

    public string[] GetFilesPathWithExtension(string extension)
    {
         return   m_registered.Where(k => k.GetExtension() == extension).Select(k => k.GetAbsolutePath()).ToArray();
    }

    internal string[] GetFilesEndingBy(string extension)
    {
        //BOF
        return m_registered.Where(k => k.GetName(true).IndexOf(extension)>-1).Select(k => k.GetAbsolutePath()).ToArray();
    }

    IEnumerator Start()
    {
        while (true) { 
            for (int i = 0; i < m_registered.Count; i++)
            {
                bool hasChange;
                m_registered[i].CheckForFileChange(out hasChange);
                if (hasChange) {
                    m_fileObservedChanged.Invoke(m_registered[i]);
                   
                    m_lastFileChange = DateTime.Now.ToString();
                }
            }
            yield return new WaitForSeconds(m_timeBetweenChangeCheck);
        }
    }

   
    public void CheckPathOfThoseFile(string path)
    {
        if (Contain(path))
            return;
        AddFile( new FileObserved(path));
    }
    public void CheckPathOfThoseFiles(string[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            AddFile(new FileObserved(path[i]));
        }

    }
    public void AddFile(FileObserved file)
    {
        if (Contain(file.GetAbsolutePath()))
            return;
        m_registered.Add(file);
    }
    public void AddFile(FileObserved[] file)
    {
        for (int i = 0; i < file.Length; i++)
        {
            AddFile(file[i]);
        }
    }

    public bool Contain(string path) {
        return m_registered.Where(k => k.GetAbsolutePath() == path).Count() > 0;
    }


    public bool Get(string path, out FileObserved file) {
        file = null;
        for (int i = 0; i < m_registered.Count; i++)
        {
            if (m_registered[i].GetAbsolutePath() == path) {
                file = m_registered[i];
                return true;
            }
        }
        return false;
    }

}
[System.Serializable]
public class FileObserved {

    public FileChangeObserver m_fileObserved = null;
    public FileChangedEvent m_fileChangeListener= new FileChangedEvent();
    
    public FileObserved(string absolutePath)
    {
        m_fileObserved = new FileChangeObserver(absolutePath);
        m_fileObserved.RefreshInformation();
    }
    public string GetAbsolutePath() { return m_fileObserved.GetUseFilePath(); }
    public string GetName(bool withExtension) {
        if (withExtension)
            return Path.GetFileName(GetAbsolutePath());
        return Path.GetFileNameWithoutExtension(GetAbsolutePath());
    }
    public string GetExtension (){
        return Path.GetExtension(GetAbsolutePath());
    }

    public void AddListener(UnityAction<string> listener)
    {
        m_fileChangeListener.AddListener(listener);
    }
    public void RemoveListener(UnityAction<string> listener)
    {
        m_fileChangeListener.RemoveListener(listener);
    }
    public bool IsFileStillExist() {
        return File.Exists(GetAbsolutePath());
    }
    private bool m_isFirstCheck = true;
    public void CheckForFileChange(out bool hasChange)
    {

         hasChange = false;
       // if (IsFileStillExist()) {
            if (m_isFirstCheck)
            {
                m_isFirstCheck = false;
                SendNewFileContentToListener();
                hasChange = true;
            }
            else if (m_fileObserved.HasChanged(true))
            {
                SendNewFileContentToListener();
                hasChange = true;
            }
       // }
    }

    public string GetFileContent(){
        if (IsFileStillExist()) {
            
            return File.ReadAllText(GetAbsolutePath());
        }
        return "";
    }
    private void SendNewFileContentToListener()
    {
       m_fileChangeListener.Invoke(GetFileContent());
    }

    public void OpenDirectory() { m_fileObserved.OpenDirectory(); }
    public void OpenFile() { m_fileObserved.OpenFile(); }

    public void ForceChangeNotification()
    {
        SendNewFileContentToListener();
    }

}

[System.Serializable]
public class FileObservedChangeEvent : UnityEvent<FileObserved> { }
[System.Serializable]
public class FileChangedEvent : UnityEvent<string> { }