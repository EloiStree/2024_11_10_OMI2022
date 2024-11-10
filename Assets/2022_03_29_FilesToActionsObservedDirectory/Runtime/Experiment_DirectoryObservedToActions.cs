using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Experiment_DirectoryObservedToActions : MonoBehaviour
{
    public Eloi.PrimitiveUnityEvent_String m_createFilePathFound;
    public bool m_deleteAfterInvoke;
   
    public void PushActionsBasedOnRefresh(ObservedFilesInDirectory refreshInfo)
    {
        for (int i = 0; i < refreshInfo.m_newFiles.Length; i++)
        {
            m_createFilePathFound.Invoke(refreshInfo.m_newFiles[i]);
            if (m_deleteAfterInvoke)
                DeleteFile(refreshInfo.m_newFiles[i]);
        }
    }

    public List<string> m_toDelete = new List<string>();

    public void DeleteFile(string filePath) {

        m_toDelete.Add(filePath);
    }

    public void Update()
    {
        for (int i = m_toDelete.Count-1; i >=0 ; i--)
        {
            try
            {
                if (File.Exists(m_toDelete[i])) { 
                    File.Delete(m_toDelete[i]);
                    m_toDelete.RemoveAt(i);
                }
                else
                { 
                    m_toDelete.RemoveAt(i);
                
                }
            }
            catch (Exception) { }
        }
    }
}
