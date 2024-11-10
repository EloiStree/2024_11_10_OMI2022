using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFileRegister : MonoBehaviour
{

    public List<ExecutableFile> m_listOfSound = new List<ExecutableFile>();
    public Dictionary<string, ExecutableFile> m_registeredSound = new Dictionary<string, ExecutableFile>();
    public Dictionary<string, ExecutableFile> m_registeredByName = new Dictionary<string, ExecutableFile>();

    public void Add(string[] absolutePath)
    {
        for (int i = 0; i < absolutePath.Length; i++)
        {
            Add(new ExecutableFile(absolutePath[i]));

        }
    }

    public void Add(string absolutePath)
    {
        Add(new ExecutableFile(absolutePath));
    }
    public void Add(ExecutableFile file)
    {
        if (!m_registeredSound.ContainsKey(file.GetFileNameWithExtension()))
        {
            m_listOfSound.Add(file);
            m_registeredSound.Add(file.GetFileNameWithExtension(), file);
            if (!m_registeredByName.ContainsKey(file.GetFileName()))
            {
                m_registeredByName.Add(file.GetFileName(), file);
            }
            else m_registeredByName[file.GetFileName()] = file;
        }
        

    }
    public void Clear() {
        m_listOfSound.Clear();
        m_registeredSound.Clear();

    }

    public bool GetFile(string name, out ExecutableFile sound)
    {
        sound = null;
        if (m_registeredSound.ContainsKey(name))
        {
            sound = m_registeredSound[name];
        }

        if (sound!=null && !sound.Exists()) {
            sound = null;
        }

        if (sound ==null && m_registeredByName.ContainsKey(name))
        {
            sound = m_registeredByName[name];
        }

        if (sound != null && !sound.Exists())
        {
            sound = null;
        }

        return sound != null;
    }
}
