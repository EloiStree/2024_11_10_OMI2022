using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class QAD_LaunchJOMIAndQuit : MonoBehaviour
{
    public string m_jomiJarFileName= "JOMI.jar";

    public void LaunchJarFile() {
        string path = UnityDirectoryStorage.GetPathOf("", m_jomiJarFileName, true);
        if (File.Exists(path))
        {
            ExecutableLauncher.LaunchExecutable(path, "");
        }
    }
 
    public void KillCurrentApp() {
        Application.Quit();
    }
    public string GetUnityExePath()
    {
        return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    }
    public void RelaunchTheApplication() {


        Application.OpenURL(GetUnityExePath());
        KillCurrentApp();
    }

    private string GetDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

   
  
}
