using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ExecutableLauncherDemo : MonoBehaviour
{
    public string m_currentDirectory;
    public string m_exeFile="Hello.exe";
    public string m_batFile = "Hello.bat";
    public string m_autoHotKeyFile = "Hello.ahk";
    public AudioSource m_bitTimeChecker;

    IEnumerator  Start()
    {
        m_bitTimeChecker.Play();
        ExecutableLauncher.LaunchExecutable(m_currentDirectory + "/" + m_batFile, "");
        yield return new WaitForSeconds(3);
        m_bitTimeChecker.Play();
        ExecutableLauncher.LaunchExecutable(m_currentDirectory + "/" + m_exeFile, "");
        yield return new WaitForSeconds(3);

        m_bitTimeChecker.Play();
        ExecutableLauncher.LaunchExecutable(m_currentDirectory + "/" + m_autoHotKeyFile, "");
        yield break;
    }

    void Reset()
    {
        m_currentDirectory = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(m_currentDirectory, "*", SearchOption.AllDirectories);
        string [] result =files.Where(k => k.EndsWith("ExecutableLauncherDemo.cs")).ToArray();
        if (result.Length > 0)
            m_currentDirectory = Path.GetDirectoryName(result[0]);
    }
}
