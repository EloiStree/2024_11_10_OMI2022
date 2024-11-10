using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class SyncFileMono : MonoBehaviour
{
    public SyncFile m_synchFile= new SyncFile("https://gist.githubusercontent.com/EloiStree/4803640622aeb52c4431bc66b5e55859/raw/3cf31ee3c929502ca3b49c85ff3c572a4fbaac26/HeyMonAmi.txt", null, "LocalFile","HeyMonAmi.txt", true);

    private void Start()
    {
        m_synchFile.CreateFileFromWebOrUnityIfNotAccess();
    }

    [ContextMenu("Open web version")]
    public void OpenToWebVersion()
    {
        UnityEngine.Application.OpenURL(m_synchFile.m_urlOfOnlineDefaultFile);
    }

    [ContextMenu("Open save directory")]
    public void OpenToLocalFolder()
    {
        UnityEngine.Application.OpenURL(m_synchFile.GetFolderPath());
    }
    [ContextMenu("Open save file")]
    public void GoToLocalFile()
    {
        UnityEngine.Application.OpenURL(m_synchFile.GetFilePath());
    }
    [ContextMenu("Override user file with update")]
    public void RefreshByOverridingUseFile()
    {
        m_synchFile.DeleteFile();
        m_synchFile.UpdateFileFromWebIfFound();
    }
  



}

[System.Serializable]
public class SyncFile {

    public string m_urlOfOnlineDefaultFile;
    public TextAsset m_defaultOfflineFile;
    public string m_folderName;
    public string m_fileNameWithExtension;
    public bool m_harddriveSave;
    

    public SyncFile(string urlDefaultFIle, TextAsset unityFile, string folderName, string fileNameWithExtension, bool useHarddriveSave)
    {
        this.m_urlOfOnlineDefaultFile = urlDefaultFIle;
        this.m_defaultOfflineFile = unityFile;
        this.m_folderName = folderName;
        this.m_fileNameWithExtension = fileNameWithExtension;
        this.m_harddriveSave = useHarddriveSave;
    }

    public void DownloadDefaultFileFromWeb(out string textFound, out bool succedToDownload) {
        succedToDownload = false;
        textFound = "";
        try { 
            WebClient client = new WebClient();
            textFound = client.DownloadString(m_urlOfOnlineDefaultFile);
            succedToDownload = true;
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);

        }
    }
    public void GetDefaultFileFromUnity(out string textFound) {

        textFound = m_defaultOfflineFile.text;
    }

    public void GetFileFromComputer(out string textFound, out bool hasFile) {
        hasFile=IsLocalFileExist();
        textFound= UnityDirectoryStorage.LoadFile(m_folderName, m_fileNameWithExtension, m_harddriveSave);
    }

    public void CreateFileFromUnityDefault() { }
    public void CreateFileFromWebOrUnityIfNotAccess() {
        string text = "";
        bool hasInternet=false;
        bool hasUrl = !string.IsNullOrEmpty(m_urlOfOnlineDefaultFile);
        if(hasUrl)
            DownloadDefaultFileFromWeb(out text, out hasInternet);
        if (!hasInternet || string.IsNullOrEmpty(text))
            GetDefaultFileFromUnity(out text);
        UnityDirectoryStorage.SaveFile(m_folderName, m_fileNameWithExtension,text, m_harddriveSave);
    }
    public void UpdateFileFromWebIfFound() {

        if (!IsLocalFileExist())
            CreateFileFromWebOrUnityIfNotAccess();
        else { 
            string text = "";
            bool hasInternet;
            DownloadDefaultFileFromWeb(out text, out hasInternet);
            if(hasInternet )
                UnityDirectoryStorage.SaveFile(m_folderName, m_fileNameWithExtension, text, m_harddriveSave);
        }
    }

    public bool IsLocalFileExist() {
        return File.Exists( UnityDirectoryStorage.GetPathOf(m_folderName, m_fileNameWithExtension, m_harddriveSave));
    }

    public string GetFolderPath()
    {
        return Path.GetDirectoryName( UnityDirectoryStorage.GetPathOf(m_folderName, m_fileNameWithExtension, m_harddriveSave));
    }

    public string GetFilePath()
    {
        return UnityDirectoryStorage.GetPathOf(m_folderName, m_fileNameWithExtension, m_harddriveSave);
    }

    public void DeleteFile()
    {
        string path = UnityDirectoryStorage.GetPathOf(m_folderName, m_fileNameWithExtension, m_harddriveSave);
        if (File.Exists(path))
            File.Delete(path);
    }
}