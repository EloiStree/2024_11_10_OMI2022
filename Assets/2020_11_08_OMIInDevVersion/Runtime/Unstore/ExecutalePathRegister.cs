using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExecutalePathRegister : MonoBehaviour
{

    public List<ExecutablePathAccessInfo> m_launchableFile = new List<ExecutablePathAccessInfo>();

    public void Clear()
    {
        m_launchableFile.Clear();
    }

    internal void TryToGetExecutableFrom(string textToLookFor, out bool found, out ExecutablePathAccessInfo toExecute)
    {
        List<ExecutablePathAccessInfo> list = m_launchableFile.OrderByDescending(k => k.m_aliasName.Length).ToList();
        textToLookFor = textToLookFor.ToLower().Trim();
        foreach (ExecutablePathAccessInfo item in list)
        {
            if (Eloi.E_StringUtility.AreEquals(in textToLookFor, in item.m_aliasName))
            {
                found = true;
                toExecute = item;
                return;
            }
        }
        foreach (ExecutablePathAccessInfo item in list)
        {
            if (item.m_aliasName.Trim().ToLower().IndexOf(textToLookFor) > -1)
            {
                found = true;
                toExecute = item;
                return;
            }
        }
        found = false;
        toExecute = null;
    }

    public void AddFromPath(string path, string alias)
    {
        ExecutablePathAccessInfo exe = new ExecutablePathAccessInfo(path, alias);

        AddExecutabe(exe);
    }

    public void AddExecutabe(ExecutablePathAccessInfo info) {
        if (info != null ){
            
            for (int i = m_launchableFile.Count-1; i >=0 ; i--)
            {
                if (Eloi.E_StringUtility.AreEquals(info.GetPath(), m_launchableFile[i].GetPath())) {
                    m_launchableFile.RemoveAt(i);
                }
            }
            m_launchableFile.Add(info);
        }
    }
    public void AddFromPath(string path, string alias, IEnumerable<string> parameters)
    {
        ExecutablePathAccessInfoWithParams exeParams = new ExecutablePathAccessInfoWithParams(path, alias, parameters.ToArray());
        AddExecutabe(exeParams);
    }
}

[System.Serializable]
public class ExecutablePathAccessInfo {
    public string m_aliasName;
    public string m_pathGiven;
    public MetaAbsolutePathFile m_filePath;
    public IMetaFileNameWithExtensionGet m_fileInfo;

    public ExecutablePathAccessInfo(string pathGiven, string aliasName )
    {
       
        SetWithPath(pathGiven, aliasName);
    }

    public string GetPath()
    {
        return m_filePath.GetPath();
    }

    public void SetWith(ExecutablePathAccessInfo info)
    {
        SetWithPath(info.m_pathGiven, info.m_aliasName);
    }
    public void SetWithPath(string path, string alias)
    {
        m_aliasName = alias;
        m_pathGiven = path;
        m_filePath = new MetaAbsolutePathFile(path);
        E_FileAndFolderUtility.GetFileInfoFromPath(m_filePath, out m_fileInfo);

        if (alias == null || alias.Trim().Length <= 0)
            m_fileInfo.GetFileNameWithExtension(out m_aliasName);
        else
            m_aliasName = alias;

    }
}


[System.Serializable]
public class ExecutablePathAccessInfoWithParams : ExecutablePathAccessInfo
{
    public List<string> m_params = new List<string>();

    public ExecutablePathAccessInfoWithParams(string pathGiven, string aliasName, params string [] parameters) : base(pathGiven, aliasName)
    {
        m_params.AddRange(parameters);
    }

    public void Add( string value) { m_params.Add(value); }
    public void GetParams(out string [] parameters) { parameters = m_params.ToArray(); }
}

