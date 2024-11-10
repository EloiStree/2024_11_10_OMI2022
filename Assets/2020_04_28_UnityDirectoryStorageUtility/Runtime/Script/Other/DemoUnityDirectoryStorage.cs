using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoUnityDirectoryStorage : MonoBehaviour
{
    public InputField m_debugDemo;

    void Start()
    {
        m_debugDemo.text = "";
        m_debugDemo.text += string.Format("Run: {0}Editor {1}Window {2}Android\n",
            UnityDirectoryStorage.IsRunningInEditor(),
            UnityDirectoryStorage.IsRunningInOnWindowComputer(),
            UnityDirectoryStorage.IsRunningInOnAndroidPhone());


        if (UnityDirectoryStorage.IsRunningInEditor())
        {
            m_debugDemo.text += "\nPersistance: " + UnityDirectoryStorage.UnityEditor.GetOperationSystemwDocumentFolder();
            m_debugDemo.text += "\nAsset: " + UnityDirectoryStorage.UnityEditor.GetAssetFolder();
            m_debugDemo.text += "\nRoot: " + UnityDirectoryStorage.UnityEditor.GetRootFolder();


        }
        if (UnityDirectoryStorage.IsRunningInOnAndroidPhone())
        {
            m_debugDemo.text += "\nPersistance: " + UnityDirectoryStorage.Android.GetTemporaryFolder_ResetAtInstall();
            m_debugDemo.text += "\nSdCard: " + UnityDirectoryStorage.Android.GetSdcardDirectory();



        }
        if (UnityDirectoryStorage.IsRunningInOnWindowComputer())
        {
            m_debugDemo.text += "\nExe File: " + UnityDirectoryStorage.WindowExe.GetExeFilePath();
            m_debugDemo.text += "\nExe Folder: " + UnityDirectoryStorage.WindowExe.GetExeFolderPath();
            m_debugDemo.text += "\nDate Near Exe: " + UnityDirectoryStorage.WindowExe.GetDataStoreFolderNearExe();
            m_debugDemo.text += "\nData on PC: " + UnityDirectoryStorage.WindowExe.GetWindowAppFolder();
        }
        m_debugDemo.text += "Propose Project Folder:" + UnityDirectoryStorage.GetPersistantThroughResintallFolder();

        try
        {
            m_debugDemo.text += "\nTry to save ABC Hello.txt...";
            UnityDirectoryStorage.SaveFile("ABC", "Hello.txt", "Hello world!",true);
            m_debugDemo.text += "\nTry to Load ABC Hello.txt...";
            string text = UnityDirectoryStorage.LoadFile("ABC", "Hello.txt",true) ;
            m_debugDemo.text += "\nText:"+text;
            m_debugDemo.text += "\nWrite and read succeed";
        }
        catch (Exception e) {
            m_debugDemo.text += "\nFail to write or read file ABC Hello.txt";
        }

    }

   
}
