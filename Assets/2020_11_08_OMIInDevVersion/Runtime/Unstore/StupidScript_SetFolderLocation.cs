using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StupidScript_SetFolderLocation : MonoBehaviour
{
    public string m_relativeConfigurationFolder="Configuration";
    public FileImporter_InDirectoryMono m_fileImporter;
    public ObserveFileCreatedAndDeletedMono m_fileCreateDeleteObserver;
    public UnityEvent m_applicationStart;

    [Header("Debug")]
    public string m_absolutePath;
    public void Awake()
    {
        m_absolutePath = UnityDirectoryStorage.GetPathOf(m_relativeConfigurationFolder, "", true);
        m_fileImporter.SetAbsolutePath(m_absolutePath);
        m_fileCreateDeleteObserver.SetAbsolutePath(m_absolutePath);
        m_applicationStart.Invoke();
    }

 
}
