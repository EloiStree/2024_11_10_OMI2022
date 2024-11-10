using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UnityDirectoryStorage
{
    public static bool IsRunningInEditor() {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
    public static bool IsRunningInOnAndroidPhone() {
        if (IsRunningInEditor())
            return false;
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

    public static string GetRandomFileName(string endFileName=".txt")
    {
        return (int)(UnityEngine.Random.value * 100000000f) + "_" +(int)(UnityEngine.Random.value * 100000000f)+endFileName;
    }

    public static bool IsRunningInOnWindowComputer() {
        if (IsRunningInEditor())
            return false;
#if UNITY_STANDALONE_WIN
        return true;
#else
        return false;
#endif
    }

    public static string GetProjectNameInFolderFormat() { return 
            Application.productName.Trim().Replace(" ",""); }
    public static string GetPersistantThroughResintallFolder() {
        if (IsRunningInEditor())
            return UnityEditor.GetRootFolder();
        if (IsRunningInOnAndroidPhone())
            return Android.GetSdcardDirectory();
        if (IsRunningInOnWindowComputer())
            return WindowExe.GetExeFolderPath();
        throw new System.NotImplementedException("No path take in charge in this operating system. Please contact me if you required it.");
    }
    public static string GetAbsolutePath(bool saveOnHarddrive)
    {
        if (saveOnHarddrive) return GetPersistantThroughResintallFolder();
        else return GetClassicUnityDataFolder();
    }

    public static string GetAbsolutePathFor(string folderName, bool saveOnHarddrive)
    {
        return GetAbsolutePathFor(folderName, "", saveOnHarddrive);
    }
    public static string GetAbsolutePathFor(string folderName, string fileNameWithExt, bool saveOnHarddrive)
    {
        folderName = RemoveSlashOrBackStashAtStart(folderName);
        fileNameWithExt = RemoveSlashOrBackStashAtStart(fileNameWithExt);
        string path = GetAbsolutePath(saveOnHarddrive);
        path = RemoveSlashOrBackStashAtEnd(path);
        if (!string.IsNullOrEmpty(folderName))
            path += "/" + folderName;
        if (!string.IsNullOrEmpty(fileNameWithExt))
            path += "/" + fileNameWithExt;
        return path;
    }

    public static string RemoveSlashOrBackStashAtStart(string value)
    {
        if (value.Length > 1)
            if (value[0] == '/' || value[0] == '\\')
                return value.Substring(1);
        return value;
    }
    public static string RemoveSlashOrBackStashAtEnd(string value)
    {
        int l = value.Length;
        if (value.Length > 1)
            if (value[l-1] == '/' || value[l-1] == '\\')
                return value.Substring(0, l-1);
        return value;
    }
    public static void SaveFile(string projectName, string fileNameWithExcetion, string text, bool saveOnharddrive) {

        string path = "";
        if (saveOnharddrive) path = GetPersistantThroughResintallFolder();
        else path = GetClassicUnityDataFolder();
        if (projectName != null && projectName.Length > 0)
        {
            path += "/" + projectName;
            Directory.CreateDirectory(path);
        }
        if (fileNameWithExcetion == null || fileNameWithExcetion.Length < 0 || fileNameWithExcetion.LastIndexOf(".") < 0)
        {
            throw new System.ArgumentException("Not good formate of file name:" + fileNameWithExcetion); 
        }

        if (text == null)
            text = "";
        path += "/" + fileNameWithExcetion;
        File.WriteAllText(path,text);
    }

    private static string GetClassicUnityDataFolder()
    {
        if (IsRunningInEditor())
            return UnityEditor.GetRootFolder();
        if (IsRunningInOnAndroidPhone())
            return Android.GetTemporaryFolder_ResetAtInstall();
        if (IsRunningInOnWindowComputer())
            return WindowExe.GetDataStoreFolderNearExe();
        throw new System.NotImplementedException("No path take in charge in this operating system. Please contact me if you required it.");
    }

    public static string LoadFile(string projectName, string fileNameWithExtension, bool saveOnharddrive)
    {
        string path = "";
        if (saveOnharddrive) path = GetPersistantThroughResintallFolder();
        else path = GetClassicUnityDataFolder();
        if (projectName != null && projectName.Trim().Length > 0)
        {
            path += "/" + projectName;
            if (!Directory.Exists(path)) return null;
        }
        if (fileNameWithExtension == null || fileNameWithExtension.Length < 0 || fileNameWithExtension.LastIndexOf(".") < 0)
        {
            throw new System.ArgumentException("Not good formate of file name:" + fileNameWithExtension);
        }

        path += "/" + fileNameWithExtension;

        if (!File.Exists(path)) return null;
        return File.ReadAllText(path);
        
    }
    public static string GetPathOf(string projectName, bool saveOnharddrive)
    {
        return GetPathOf(projectName, "", saveOnharddrive);
    }
    
    public static string GetPathOf (string projectName, string fileNameWithExtension, bool saveOnharddrive)
    {
            string path = "";
            if (saveOnharddrive) path = GetPersistantThroughResintallFolder();
            else path = GetClassicUnityDataFolder();
       // Debug.Log("P:" + path);
            if (projectName != null && projectName.Trim().Length > 0)
            {
                path += "/" + projectName;
            }
            path += "/" + fileNameWithExtension;
       // Debug.Log("PP:" + path);
        return path;

     }

    

    public static class Android {

        public static string GetTemporaryFolder_ResetAtInstall() {
            return Application.persistentDataPath ;
        }

        public  static string GetSdcardDirectory()
        {
            string[] potentialDirectories = new string[]
            {
                "/mnt/sdcard",
                "/sdcard",
                "/storage/sdcard0",
                "/storage/sdcard1"
            };

            if (Application.platform == RuntimePlatform.Android)
            {
                for (int i = 0; i < potentialDirectories.Length; i++)
                {
                    if (Directory.Exists(potentialDirectories[i]))
                    {
                        return potentialDirectories[i];
                    }
                }
            }
            return "";
        }

        public static bool CheckIfProjectIsSetWithInternalWriteAllow() {
            throw new System.NotImplementedException("I should code that but I too tired for the moment to watch on StackOverflow for reflexion C# Ways");
        }

    }


    public static class UnityEditor {
        public static string GetOperationSystemwDocumentFolder()
        {
            return Application.persistentDataPath;
        }
        public static string GetAssetFolder()
        {
            return Application.dataPath;
        }
        public static string GetRootFolder(bool endWithSlash=true)
        {
            return Application.dataPath+"/..";
        }
    }
    public static class WindowExe {

        public static string GetExeFolderPath()
        {
            return Path.GetDirectoryName(Application.dataPath); // TO TEST
        }
        public static string GetExeFilePath()
        {
            return Path.GetFullPath("."); // TO TEST
        }

        public static string GetWindowAppFolder()
        {
            return Application.persistentDataPath;// TO VERIFY
        }
        public static string GetDataStoreFolderNearExe()
        {
            return Application.dataPath;// TO VERIFY
        }
        //public static string GetImageFolder()
        //{
        //    return Application.dataPath;// TO VERIFY
        //}
        //public static string GetVideoFolder()
        //{
        //    return Application.dataPath;// TO VERIFY
        //}
        //public static string GetSoundFolder()
        //{
        //    return Application.dataPath;// TO VERIFY
        //}
    }
}

