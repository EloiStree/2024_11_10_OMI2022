using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ExecutableFileRegister : MonoBehaviour
{
    public List<ExecutableFile> m_executableFiles;
    public Dictionary<string, ExecutableFile> m_registerByName = new Dictionary<string, ExecutableFile>();
    public Dictionary<string, ExecutableFile> m_registerByNameWithExtension = new Dictionary<string, ExecutableFile>();


    public bool GetExecutableByName(string id, out ExecutableFile file)
    {
        file = null;
        if (!m_registerByName.ContainsKey(id))
            return false;
        file = m_registerByName[id];
        return true;
    }
    public bool GetExecutableByNameWithExtension(string id, out ExecutableFile file)
    {
        file = null;
        if (!m_registerByNameWithExtension.ContainsKey(id))
            return false;
        file = m_registerByNameWithExtension[id];
        return true;
    }

    public void Add(string [] absolutePath)
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
        if (!m_registerByName.ContainsKey(file.m_name)) { 
            m_registerByName.Add(file.m_name, file);
            m_executableFiles.Add(file);
            if (!m_registerByNameWithExtension.ContainsKey(file.m_nameWithExtension))
                m_registerByNameWithExtension.Add(file.m_nameWithExtension,file);
        }
    }

    public void Clear() {
        m_executableFiles.Clear();
        m_registerByName.Clear();
        m_registerByNameWithExtension.Clear();
    }
}

[System.Serializable]
public class ExecutableFile {

    public ExecutableFile(string absolutePath) {

        m_absoluteFile = absolutePath;
        m_name = Path.GetFileNameWithoutExtension(absolutePath);
        m_nameWithExtension = Path.GetFileName(absolutePath);
    }

    public string m_absoluteFile;
    public string m_name;
    public string m_nameWithExtension;

    public string GetFileName() { return m_name; }
    public string GetFileNameWithExtension() { return m_nameWithExtension; }

    public string GetAbsolutePath()
    {
        return m_absoluteFile;
    }

    public bool Exists()
    {
        return File.Exists(GetAbsolutePath());
    }

    public string GetText()
    {
        if (Exists())
            return File.ReadAllText(GetAbsolutePath());
        else return "";
    }
}
