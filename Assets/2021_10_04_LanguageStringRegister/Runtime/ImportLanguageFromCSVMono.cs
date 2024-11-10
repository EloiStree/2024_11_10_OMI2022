using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImportLanguageFromCSVMono : MonoBehaviour
{
    public TextAsset m_defaultIfNotExisting;
    public string m_relativePathOfLanguageFolder = "configuration";
    public string m_relativePathOfLanguageFileName = "language.csv";

    public Debug m_debug= new Debug();
    [System.Serializable]
    public class Debug { 
        public bool m_fromUnityConverted;
        public string m_fromUnityError;
        public string m_pathUsed;
    }

    public void ImportLanguageCSV()
    {
        string dirPath = GetFolderPath();
        ImportLanguageCSV(dirPath);
    }
        
    public void ImportLanguageCSV(string dirPath)     
    {
        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = GetFilePath();
            if (!File.Exists(filePath) && m_defaultIfNotExisting != null)
            {
                File.WriteAllText(filePath, m_defaultIfNotExisting.text);
            }

            if (File.Exists(filePath))
            {
                m_debug.m_pathUsed = filePath;
                ImportExcelToLanguageRegister.Import(File.ReadAllText(filePath), ref LanguageRegister.Set, out m_debug.m_fromUnityConverted, out m_debug.m_fromUnityError);
                LanguageRegister.NotifyRegisterChange();
            }
        }
        catch (Exception e) {
            string t = m_defaultIfNotExisting.text;
            ImportExcelToLanguageRegister.Import(t, ref LanguageRegister.Set, out m_debug.m_fromUnityConverted, out m_debug.m_fromUnityError);
            LanguageRegister.NotifyRegisterChange();
            Eloi.E_DebugLog.C("->");
        }
    }

    [ContextMenu("Open Folder")]
    public void OpenFolder()
    {
        Application.OpenURL(GetFolderPath());
    }
    [ContextMenu("Open File")]
    public void OpenFile() {

        Application.OpenURL(GetFilePath());
    }


    public string GetFolderPath()
    {
        return GetFolderPath(GetAbsolutePathDependingOfPlatform());
    }
    public string GetFilePath()
    {
        return GetFilePath(GetAbsolutePathDependingOfPlatform()); 
    }
    public string GetFolderPath(string rootFolderPath)
    {
         Eloi.E_FilePathUnityUtility.MeltPathTogether(out string path, rootFolderPath, m_relativePathOfLanguageFolder);
        return path;
    }
    public string GetFilePath(string rootFolderPath)
    {
        Eloi.E_FilePathUnityUtility.MeltPathTogether(out string path, rootFolderPath, m_relativePathOfLanguageFolder, m_relativePathOfLanguageFileName);
        return path;
    }

    public static string GetAbsolutePathDependingOfPlatform()
    {

        return Directory.GetCurrentDirectory();
    }
}
