using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExperimentFileToClipboard : MonoBehaviour
{
    public TextAsset m_text;
    public string m_packageManifest;

    public void Reset()
    {
           m_packageManifest = Application.dataPath+ "/../Packages/manifest.json";
    }
    public void PushTextFileToClipboard()
    {

        ClipboardUtility.PushFileFromTextAsset(m_text);
    }
    public void PushFilePathContentToClipboard()
    {

        ClipboardUtility.PushFileFromAbsolutePath(m_packageManifest);

    }
}

public class ClipboardUtility
{

    public static void PushFileFromAbsolutePath(string fileAbsolutPath)
    {
        if (string.IsNullOrEmpty(fileAbsolutPath))
            throw new System.ArgumentException("No path set to copy in clipboard");
        if (!File.Exists(fileAbsolutPath))
            throw new System.ArgumentException("No file found at: "+fileAbsolutPath);
        
            Set(File.ReadAllText(fileAbsolutPath));
        
    }

    public static void PushFileFromDataProjectRelativePath(string relativePathFile)
    {
        PushFileFromAbsolutePath(Application.dataPath + "/" + relativePathFile);

    }

    public static void PushFileFromTextAsset(TextAsset file)
    {
        if (file != null)
        {
            Set(file.text);
        }
    }

    public static void Set(string value)
    {
        UnityClipboard.Set(value);
    }
    public static string Get()
    {
       return UnityClipboard.Get();
    }

}