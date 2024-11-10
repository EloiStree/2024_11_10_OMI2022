using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObservedFilesInDirectoryMono : MonoBehaviour
{
    public void AddDirectoryToObserve(string directoryPath)
    {
        ObservedFilesInDirectory o = new ObservedFilesInDirectory(directoryPath);
        o.Refresh(false);
        o.m_onRefreshed.AddListener(m_onRefresh.Invoke);
        m_observerDirectoriesRuntime.Add(o);
    }
    public void AddDirectoryToObserve(Eloi.IMetaAbsolutePathDirectoryGet directoryPath)
    {
        ObservedFilesInDirectory o = new ObservedFilesInDirectory(directoryPath.GetPath());
        o.Refresh(false);
        o.m_onRefreshed.AddListener(m_onRefresh.Invoke);
        m_observerDirectoriesRuntime.Add(o);
    }
    public void Clear() {
        m_observerDirectoriesRuntime.Clear();
    }
   

    public ObservedFilesInDirectory.OnValueChanged m_onRefresh;

    public void RefreshAll() {
        for (int i = 0; i < m_observerDirectoriesRuntime.Count; i++)
        {
            m_observerDirectoriesRuntime[i].Refresh();
        }
        for (int i = 0; i < m_observerDirectoriesFromEditor.Length; i++)
        {
            m_observerDirectoriesFromEditor[i].Refresh();
        }
    }

    public List<ObservedFilesInDirectory> m_observerDirectoriesRuntime = new List<ObservedFilesInDirectory>();
    public ObservedFilesInDirectory[] m_observerDirectoriesFromEditor;

}

[System.Serializable]
public class ObservedFilesInDirectory
{
    public Eloi.MetaAbsolutePathDirectory m_observerDirectory;
    public bool m_onlyInParent=false;
    public string[] m_filesCurrent= new string[0];
    public string[] m_filesPrevious = new string[0];
    public string[] m_newFiles = new string[0];
    public string[] m_stayedFiles = new string[0];
    public string[] m_deletedFiles = new string[0];
    public OnValueChanged m_onRefreshed = new OnValueChanged();

    public ObservedFilesInDirectory(string directoryPath)
    {
        m_observerDirectory = new Eloi.MetaAbsolutePathDirectory(directoryPath);
    }

    public void Refresh(bool andNotify=true)
    {
        m_filesPrevious = m_filesCurrent;
        if (m_observerDirectory == null)
        {
            m_filesCurrent = new string[0];
        }
        else
        {
            string path = m_observerDirectory.GetPath();
            if (Directory.Exists(path))
                m_filesCurrent = Directory.GetFiles(path, "*", m_onlyInParent ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
            else m_filesCurrent = new string[0];
        }
        m_newFiles = m_filesCurrent.Except(m_filesPrevious).ToArray();
        m_deletedFiles = m_filesPrevious.Except(m_filesCurrent).ToArray();
        m_stayedFiles = m_filesCurrent.Except(m_newFiles).ToArray();
        if(andNotify)
        m_onRefreshed.Invoke(this);
    }
    [System.Serializable]
    public class OnValueChanged : UnityEvent<ObservedFilesInDirectory> { }
}