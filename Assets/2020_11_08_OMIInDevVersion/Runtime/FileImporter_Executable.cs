using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class FileImporter_Executable : MonoBehaviour
{
    public string m_folderOfFiles="LaunchableFile";
     public List<string> pathOfFiles= new List<string>();
    public FilesPathEvent m_onFilesLoad;

    void Start()
    {
        ImportLaunchableFiles();
    }

    public void ImportLaunchableFiles()
    {
        string path = UnityDirectoryStorage.GetAbsolutePathFor(m_folderOfFiles, true);
        Directory.CreateDirectory(path);
        pathOfFiles.Clear();
        string[] paths;
        ExecutableLauncher.GetExeFiles(path, true, out paths);
        pathOfFiles.AddRange(paths);
        ExecutableLauncher.GetAutoHotKeyFiles(path, true, out paths);
        pathOfFiles.AddRange(paths);
        ExecutableLauncher.GetBatchFiles(path, true, out paths);
        pathOfFiles.AddRange(paths);
        ExecutableLauncher.GetJarFiles(path, true, out paths);
        pathOfFiles.AddRange(paths);

        m_onFilesLoad.Invoke(pathOfFiles.ToArray());
    }
}

