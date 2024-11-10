using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using UnityEngine;

public class CopyPastFileRegisterMono : MonoBehaviour
{
    public FileDictionnary<CopyPastFile> m_register = new FileDictionnary<CopyPastFile>();
    public int count;


    public void Clear() {
        m_register.Clear();
        count = 0;
    }
    public void Add(CopyPastFile past) {
        m_register.AddByOverriding(past.m_file, past);
        count = m_register.GetCount();

    }

    internal bool GetFile(string name, out CopyPastFile file)
    {
        file =m_register.GetByNameWithOrWithoutExtension(name);
        return file != null;
    }
}

[System.Serializable]
public class CopyPastFile {
    public ExecutableFile m_file;
    private string m_stayLoadedText="";

    public CopyPastFile(string filePath)
    {
        this.m_file = new ExecutableFile(filePath);
    }
    public string GetText()
    {
        return m_file.GetText();
    }
    public void LoadPermanently()
    {
        m_stayLoadedText = m_file.GetText();
    }
    public bool HasLoadPermanelty() { return m_stayLoadedText.Length > 0; }
}

public class  FileDictionnary<T> where T:class{

    public void Clear() { m_byName.Clear(); m_byNameWithExtension.Clear(); }

    public void AddByOverriding(ExecutableFile file, T data)
    {
        AddByOverriding(file.GetFileName(), file.GetFileNameWithExtension(), data);
    }
    public void AddByOverriding(string filename, string filenamewithextension, T data)
    {
        if (m_byNameWithExtension.ContainsKey(filenamewithextension))
            m_byNameWithExtension[filenamewithextension] = data;
        else m_byNameWithExtension.Add(filenamewithextension, data);

        if (m_byName.ContainsKey(filename))
            m_byName[filename] = data;
        else m_byName.Add(filename, data);
    }

    public T Get(ExecutableFile fileInfo)
    {
        if (m_byNameWithExtension.ContainsKey(fileInfo.GetFileNameWithExtension()))
        {
            return m_byNameWithExtension[fileInfo.GetFileNameWithExtension()];
        }
        return null;
    }
    public T GetByNameWithOrWithoutExtension(string name) {
        T result = null;
        result = GetByNameWithExtension(name);
        if (result == null)
            result = GetByName(name);
        return result;

    }

    public T GetByName(string name)
    {
        if (m_byNameWithExtension.ContainsKey(name))
        {
            return m_byNameWithExtension[name];
        }
        return null;
    }
    public T GetByNameWithExtension(string name)
    {
        if (m_byName.ContainsKey(name))
        {
            return m_byName[name];
        }
        return null;
    }

    internal int GetCount()
    {
        return m_byNameWithExtension.Keys.Count;
    }

    public Dictionary<string, T> m_byName = new Dictionary<string, T>();
    public Dictionary<string, T> m_byNameWithExtension = new Dictionary<string, T>();

}
