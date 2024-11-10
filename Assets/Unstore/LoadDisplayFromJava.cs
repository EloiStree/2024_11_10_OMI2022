
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadDisplayFromJava : MonoBehaviour
{

    public string m_path;
    public float m_timeBetweenReload=1;
    public LoadedDisplay[] m_displays = new LoadedDisplay[10];


    public  string[] m_files;
    public List<FileChangeObserver> m_filesObserved = new List<FileChangeObserver>();
    public NewDisplayLoadedEvent m_onNewImport;

    public void SetWithPath(string path)
    {
        m_path = path ;
        LoadImageToObserve();
    }

    public bool IsDisplayDefined(int displayIndex) {
        return displayIndex > -1 && displayIndex < m_displays.Length &&
            m_displays[displayIndex].m_texture != null;
    
    }

    public Color GetPixel(int displayIndex, float botTopPourcent, float leftRightPourcent)
    {
        Texture2D t = m_displays[displayIndex].m_texture;
        int x= (int)(botTopPourcent *t.width), y= (int)(leftRightPourcent*t.height);
        return t.GetPixel(x, y);
    }

    void Start()
    {

        LoadImageToObserve();
        InvokeRepeating("CheckForChange", 0, m_timeBetweenReload);
    }

    public void CheckForChange() {

        for (int i = 0; i < m_filesObserved.Count; i++)
        {
            if (m_filesObserved[i] != null)
            {
                if(m_filesObserved[i].HasChanged(true)){
                    FileModification(m_filesObserved[i]);
                }
            }
        }
    }


    public void LoadImageToObserve() {

        if (Directory.Exists(m_path))
        {
            
            m_files = Directory.GetFiles(m_path, "*", SearchOption.TopDirectoryOnly).Where(k => k.ToLower().EndsWith(".png")).ToArray();
            m_filesObserved.Clear();
            for (int i = 0; i < m_files.Length; i++)
            {
                m_filesObserved.Add(new FileChangeObserver(m_files[i]));
            }
            
        }
        else m_files = new string[0];

    }
    public void FileModification(FileChangeObserver file) {

        int indexOfFile; 
        bool hasIndex;    
        GetIndexOfFIle(file, out hasIndex, out indexOfFile);

        if (hasIndex && indexOfFile< m_displays.Length) {
             LoadPNG(ref m_displays[indexOfFile].m_texture ,file.GetUseFilePath());
            Texture2D t = m_displays[indexOfFile].m_texture;
            if (t != null) { 
                m_displays[indexOfFile].m_texture = t;
                if(m_displays[indexOfFile].m_debug != null)
                    m_displays[indexOfFile].m_debug.texture = t;
            }
            m_lastpath = file.GetUseFilePath();
            m_lastModification = DateTime.Now.ToString();
            NewDisplayLoaded loaded = new NewDisplayLoaded();
            loaded.m_displayIndex = indexOfFile;
            loaded.m_imageRef = t;
            loaded.m_loadedTime = DateTime.Now;
            m_onNewImport.Invoke(loaded);
        }
    }
    private string m_lastpath;
    public string m_lastModification;

    private void GetIndexOfFIle(FileChangeObserver file,  out bool isvalide,out  int index)
    {
        index = 0;
        isvalide = false;
        string[] token = file.GetNameWithoutExtension().Split('_');
        if (token.Length >= 2)
        {
            if (int.TryParse(token[0], out index))
            {
                isvalide = true;
            }
        }
    }

    public  Texture2D LoadPNG(ref Texture2D texture, string filePath)
    {

        byte[] fileData;

        try
        {
            if (File.Exists(filePath))
            {
              
                fileData = File.ReadAllBytes(filePath);
                if(texture==null)
                    texture = new Texture2D(2, 2);
                texture.LoadImage(fileData); 
            }
        }
        catch (Exception e) {
            m_fileConflict++;
        }
        return texture;
    }
    public int m_fileConflict=0;


   
}

[System.Serializable]
public class LoadedDisplay {

    public RawImage m_debug;
    [Header("Debug")]
    public Texture2D m_texture;

}


[System.Serializable]
public class NewDisplayLoadedEvent : UnityEvent<NewDisplayLoaded> { 

}
[System.Serializable]
public class NewDisplayLoaded {
    public int m_displayIndex;
    public Texture2D m_imageRef;
    public DateTime m_loadedTime;

    public Color GetPixel(float leftRightPourcent, float botTopPourcent)
    {
            Texture2D t = m_imageRef;
            int x = (int)(leftRightPourcent * t.width), y = (int)((botTopPourcent) * t.height);
            return t.GetPixel(x, y);
        
    }
}