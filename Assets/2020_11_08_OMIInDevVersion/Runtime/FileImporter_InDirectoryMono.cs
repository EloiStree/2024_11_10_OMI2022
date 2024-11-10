using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FileImporter_InDirectoryMono : MonoBehaviour
{
    public FileImporter_InDirectory m_observed;
    public void ImportAllFile() {
        m_observed.ImportAllFile();
    }

    public void SetAbsolutePath(string absolutePath)
    {
        m_observed.SetAbsolutePathOfDirectory(absolutePath);
    }
}

[System.Serializable]
public class FileImporter_InDirectory {


    public string m_pathOfDirectory = "";
    public string[] m_extension = new string[] { ".txt" };
    [Header("Event")]
    public FilesPathEvent m_onFilesLoad;
    [Header("Debug")]
    public List<string> pathOfFiles = new List<string>();

    void Start()
    {
        ImportAllFile();
    }
    public static void GetFiles(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath)
    {
        filesAbsolutePath = Directory.GetFiles(absolutepath, "*", checkChildfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }

    public static string[] FilterExtension(string[] filesAbsolutePath, string extension)
    {
        return filesAbsolutePath.Where(k => k.ToLower().Trim().EndsWith(extension.ToLower())).ToArray();
    }

    public void ImportAllFile()
    {
        if (m_pathOfDirectory == null || m_pathOfDirectory.Length == 0)
            return;

        string path = m_pathOfDirectory;
        Directory.CreateDirectory(path);
        pathOfFiles.Clear();
        string[] paths;
        GetFiles(path, true, out paths);
        if (m_extension.Length > 0)
        {
            List<string> pathsFound = new List<string>();
            for (int i = 0; i < m_extension.Length; i++)
            {
                pathsFound.AddRange(FilterExtension(paths, m_extension[i]));

            }
            m_onFilesLoad.Invoke(pathsFound.ToArray());
        }
        else
            m_onFilesLoad.Invoke(paths);
    }

    public void SetAbsolutePathOfDirectory(string absolutePath)
    {
        m_pathOfDirectory = absolutePath;
    }
}


