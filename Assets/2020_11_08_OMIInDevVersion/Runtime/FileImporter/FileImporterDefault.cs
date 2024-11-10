using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileImporterDefault : MonoBehaviour
{
    public ObservedSyncFile m_defaultFileUse;
    public float m_timeBeforeFirstImport=1f;
    public float m_timeBetweenFileCheck = 3;

    public string m_lastLoadedTime="";
    [TextArea(2,6)]
    public string m_lastLoadedText="";
    
    private IEnumerator Start()
    {
        m_defaultFileUse.Init();
        m_defaultFileUse.StartListeningToChange(FileChanged);
        yield return new WaitForSeconds(m_timeBeforeFirstImport);
        m_defaultFileUse.ForceChangeNotification();
        InvokeRepeating("CheckFile", 0, m_timeBetweenFileCheck);
    }

    private void FileChanged(string textInFile)
    {
        m_lastLoadedTime = DateTime.Now.ToString();
        m_lastLoadedText = textInFile;
    }
    public void CheckFile()
    {
        m_defaultFileUse.CheckForFileChange();
    }
}
