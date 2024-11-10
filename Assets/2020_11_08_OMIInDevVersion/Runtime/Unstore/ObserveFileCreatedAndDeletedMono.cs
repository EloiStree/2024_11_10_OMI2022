using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ObserveFileCreatedAndDeletedMono : MonoBehaviour
{
    [Range(0.1f,60)]
    public float m_checkTimer=3;
    public ObserveFileCreatedAndDeleted m_observed= new ObserveFileCreatedAndDeleted();

    private void Start()
    {
        InvokeRepeating("Check", 0, m_checkTimer);
    }
    public void Check() {
        m_observed.CheckForFileChanges();
    }

    public void SetAbsolutePath(string absolutePath)
    {
        m_observed.SetAbsolutePathOfDirectory(absolutePath);
    }
}

[System.Serializable]
public class ObserveFileCreatedAndDeleted
{
    public string m_directoryObserved = "";
    [Header("Events")]
    public FilesPathEvent m_onFilesAtStartTracking;
    public FilesPathEvent m_onFilesCreated;
    public FilesPathEvent m_onFilesDeleted;
    [Header("Debug")]
    public List<string> m_currentFilesInDirectory = new List<string>();
    public List<string> m_previousFilesInDirectory = new List<string>();
    List<string> newFile = new List<string>();
    List<string> deleteFile = new List<string>();

    public string[] m_ingoreIfContain = new string[] { ".git" };

   

    public void CheckForFileChanges()
    {

        if (m_directoryObserved == null || m_directoryObserved.Length == 0)
            return;

       List<string> filesPath = Directory.GetFiles(m_directoryObserved, "*", SearchOption.AllDirectories).ToList();
        for (int i = filesPath.Count-1; i >=0; i--)
        {
            for (int y = 0; y < m_ingoreIfContain.Length; y++)
            {
                if (filesPath[i].IndexOf(m_ingoreIfContain[y]) > -1) { 
                    filesPath.RemoveAt(i);
                    break;
                }
            }
        }

        if (m_currentFilesInDirectory.Count == 0 && m_previousFilesInDirectory.Count == 0)
        {
            m_currentFilesInDirectory = filesPath.ToList();
            m_onFilesAtStartTracking.Invoke(filesPath.ToArray());
        }
        else
        {
            m_currentFilesInDirectory = filesPath.ToList();

            newFile.Clear();
            deleteFile.Clear();

            for (int i = 0; i < m_currentFilesInDirectory.Count; i++)
            {
                if (!m_previousFilesInDirectory.Contains(m_currentFilesInDirectory[i]))
                    newFile.Add(m_currentFilesInDirectory[i]);
            }
            for (int i = 0; i < m_previousFilesInDirectory.Count; i++)
            {
                if (!m_currentFilesInDirectory.Contains(m_previousFilesInDirectory[i]))
                    deleteFile.Add(m_previousFilesInDirectory[i]);
            }
            if (newFile.Count > 0)
                m_onFilesCreated.Invoke(newFile.ToArray());
            if (deleteFile.Count > 0)
                m_onFilesDeleted.Invoke(deleteFile.ToArray());
        }

        m_previousFilesInDirectory = m_currentFilesInDirectory;
    }

    public  void SetAbsolutePathOfDirectory(string absolutePath)
    {
        m_directoryObserved = absolutePath;
    }
}