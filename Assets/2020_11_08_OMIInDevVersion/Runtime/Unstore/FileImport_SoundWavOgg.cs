using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImport_SoundWavOgg : MonoBehaviour
{
    public SoundFileRegister m_register;

    public void ClearRegister()
    {
        m_register.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {

                m_register.Add(filePath[i]);
            }
        }
    }
}
