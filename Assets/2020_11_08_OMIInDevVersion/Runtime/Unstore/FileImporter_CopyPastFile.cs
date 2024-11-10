using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_CopyPastFile : MonoBehaviour
{
    public CopyPastFileRegisterMono m_copyPastRegister;

    public void ClearRegister()
    {
        m_copyPastRegister.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                m_copyPastRegister.Add(new CopyPastFile(filePath[i]));
            }
        }
    }
}
