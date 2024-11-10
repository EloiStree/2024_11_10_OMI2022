using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Eloi;
using System;

public class Experiment_AllPossibleStorage : MonoBehaviour
{
    public string m_persistentDataPath;
    public InputField m_persistentDataUI;
    public string m_temporaryCachePath;
    public InputField m_temporaryCachePathUI;
    public string m_currentDirectory;
    public InputField m_currentDirectoryUI;
    public string m_dataPath;
    public InputField m_dataPathUI;


    public class FileTag {
        public string m_description;
        public string m_fileName;
        [Header("Debug")]
        public string m_pathUsed;

    }
 
    void Start()
    {
        m_persistentDataPath ="PersistentData: " + Application.persistentDataPath;
        if (m_persistentDataUI != null)
            m_persistentDataUI.text = Application.persistentDataPath;

        m_temporaryCachePath ="Temorary:" + Application.dataPath;
        if (m_temporaryCachePathUI != null)
            m_temporaryCachePathUI.text = Application.dataPath;

        m_dataPath="Data Path:"+ Application.temporaryCachePath;
        if (m_dataPathUI != null)
            m_dataPathUI.text = Application.temporaryCachePath;

        m_currentDirectory ="Current Dir:" + Directory.GetCurrentDirectory();
        if(m_currentDirectoryUI!=null)
        m_currentDirectoryUI.text = Directory.GetCurrentDirectory();

        //Application.persistentDataPath;
        //Application.dataPath;
        //Application.temporaryCachePath;
        //Application.
    }

}
public interface IConventionalFileStorage
{
    void GetClassicNameOfIt(out string usualNameOfIt);
    void GetShortLineDescriptionOfIt(out string shortDescription);
    void GetPath(out IMetaAbsolutePathFileGet path);
    void TryToOpenFile();
}



public abstract class SpecificFileStorage : IConventionalFileStorage
{

    public abstract void GetClassicNameOfIt(out string usualNameOfIt);
    public abstract void GetShortLineDescriptionOfIt(out string shortDescription);
    public abstract void GetPath(out IMetaAbsolutePathFileGet path);

    public void TryToOpenFile()
    {
        GetPath(out IMetaAbsolutePathFileGet path);
        Application.OpenURL(path.GetPath());
    }
}

public interface IOwnsConventionalFolderStorage
{
    void GetFolderStorage(out IConventionalFolderStorage storageChoose);
}
public interface IOwnsConventionalFileStorage
{
    void GetFolderStorage(out IConventionalFileStorage storageChoose);
}

public interface IConventionalFolderStorage
{
    void GetClassicNameOfIt(out string usualNameOfIt);
    void GetShortLineDescriptionOfIt(out string shortDescription);
    void GetPath(out IMetaAbsolutePathDirectoryGet path);
    void  TryToOpenFolder();
}

public abstract class SpecificStorage : IConventionalFolderStorage, IMetaAbsolutePathDirectoryGet
{

    public abstract void GetClassicNameOfIt(out string usualNameOfIt);
    public abstract void GetShortLineDescriptionOfIt(out string shortDescription);
    public abstract void GetPath(out IMetaAbsolutePathDirectoryGet path);
    public virtual void TryToOpenFolder()
    {
        GetPath(out IMetaAbsolutePathDirectoryGet path);
        Application.OpenURL(path.GetPath());
    }

    public void GetPath(out string path)
    {
        GetPath(out IMetaAbsolutePathDirectoryGet p);
        path = p.GetPath();
    }

    public string GetPath()
    {
        GetPath(out IMetaAbsolutePathDirectoryGet p);
        return p.GetPath();
    }
}



public abstract class SpecificStorageNoTimeForDescription : SpecificStorage
{

    public override void GetClassicNameOfIt(out string usualNameOfIt)
     => usualNameOfIt = this.GetType().ToString();

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        GetPath(out IMetaAbsolutePathDirectoryGet path);
        shortDescription = this.GetType().ToString() + ":" + path.GetPath();
    }
}

public abstract class SpecificFileStorageNoTimeForDescription : SpecificFileStorage
{

    public override void GetClassicNameOfIt(out string usualNameOfIt)
     => usualNameOfIt = this.GetType().ToString();

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        GetPath(out IMetaAbsolutePathFileGet path);
        shortDescription = this.GetType().ToString() + ":" + path.GetPath();
    }


}



public class WebGlDataStorageDirectory : UnityDataPathDirectory { }

public class AndroidRootFileSystemDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    => usualNameOfIt = "Android Emulator System Directory";

    public string GetAndroidExternalStoragePath()
    {
        string path = "";
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
            path = jc.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");
            return path;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return path;
    }

    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
        //string[] potentialDirectories = new string[]
        //{
        //        "file:///storage/emulated/0",
        //        "file://storage/emulated/0",
       //           "/mnt/sdcard/",
        //        "/storage/emulated/0",
        //        "/"
        //};

        //path = null;
        //for (int i = 0; i < potentialDirectories.Length; i++)
        //{
        //    if (Directory.Exists(potentialDirectories[i]))
        //    {
        //        path = new MetaAbsolutePathDirectory(potentialDirectories[i]);
        //        return;
        //    }
        //}
        path = new MetaAbsolutePathDirectory(GetAndroidExternalStoragePath());
    }


    public override void GetShortLineDescriptionOfIt(out string shortDescription)
     => shortDescription = "The 'root' folder of Android depending of the device.";

    public override void TryToOpenFolder() {
        GetPath(out IMetaAbsolutePathDirectoryGet path);
        Application.OpenURL("file:///" + path.GetPath());
        Application.OpenURL( (Path.GetDirectoryName(path.GetPath())));

        //try
        //{
        //    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //    AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        //    AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

        //    string packageName = unityContext.Call<string>("getPackageName");
        //    string authority = packageName + ".fileprovider";

        //    AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");
        //    string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
        //    AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);

        //    int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
        //    int FLAG_GRANT_READ_URI_PERMISSION = intentObj.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION");

        //    AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", savePath);
        //    AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content.FileProvider");
        //    AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);

        //    print(uri.Call<string>("toString"));

        //    intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
        //    intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
        //    intent.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);
        //    currentActivity.Call("startActivity", intent);

        //    print("Success");
        //}
        //catch (System.Exception e)
        //{
        //    print("Error: " + e.Message);
        //}
    }

    
}
public static class AndroidOpenUrl
{
    public static void OpenFile(string url, string dataType = "application/pdf")
    {
        AndroidJavaObject clazz = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = clazz.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("addFlags", intent.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION"));
        intent.Call<AndroidJavaObject>("setAction", intent.GetStatic<string>("ACTION_VIEW"));

        var apiLevel = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");

        AndroidJavaObject uri;
        if (apiLevel > 23)
        {
            AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content.FileProvider");
            AndroidJavaObject file = new AndroidJavaObject("java.io.File", url);

            AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            string packageName = unityContext.Call<string>("getPackageName");
            string authority = packageName + ".fileprovider";

            uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, file);
        }
        else
        {
            var uriClazz = new AndroidJavaClass("android.net.Uri");
            var file = new AndroidJavaObject("java.io.File", url);
            uri = uriClazz.CallStatic<AndroidJavaObject>("fromFile", file);
        }

        intent.Call<AndroidJavaObject>("setType", dataType);
        intent.Call<AndroidJavaObject>("setData", uri);

        currentActivity.Call("startActivity", intent);
    }
}
public class AndroidSDCardFileSystemDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    => usualNameOfIt = "Android SDCard Directory";

    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
           string[] potentialDirectories = new string[]
           {
                "/mnt/sdcard",
                "/sdcard",
                "/storage/sdcard0",
                "/storage/sdcard1",
                "/"
           };

        path = null;
        for (int i = 0; i < potentialDirectories.Length; i++)
        {
            if (Directory.Exists(potentialDirectories[i]))
            {
                path = new MetaAbsolutePathDirectory(potentialDirectories[i]);
                return;
            }
        }
        path = null;
    }
    public override void GetShortLineDescriptionOfIt(out string shortDescription)
     => shortDescription = "Directory of the SDCard on Android. Path is dependant of the Android";
}



public class EditorWindowCurrentDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    => usualNameOfIt = "Get Current Directory (Window in Editor)";
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    => path = new MetaAbsolutePathDirectory(Directory.GetCurrentDirectory());

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
     => shortDescription = "Get Directory in Unity Editor on Window is near the project root  near Assets";
}
public class UnityDataPathDirectory: SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
=> usualNameOfIt = "Classic Data Path of Unity";
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    => path = new MetaAbsolutePathDirectory(Application.dataPath);

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
     => shortDescription = "This folder is heavily dependant of the platform or the editor";
}

public class UnityPersistentDataPathDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
=> usualNameOfIt = "Classic Persistent Data Path of Unity";

public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
=> path = new MetaAbsolutePathDirectory(Application.persistentDataPath);

public override void GetShortLineDescriptionOfIt(out string shortDescription)
 => shortDescription = "This folder is heavily dependant of the platform or the editor";
}

public class UnityTempDataDirectory  : SpecificStorage
{
public override void GetClassicNameOfIt(out string usualNameOfIt)
=> usualNameOfIt = "Classic Data Temporary cach folder of Unity";
public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
=> path = new MetaAbsolutePathDirectory(Application.temporaryCachePath);

public override void GetShortLineDescriptionOfIt(out string shortDescription)
 => shortDescription = "This folder is heavily dependant of the platform or the editor";
}

public class EditorUnityProjectRoot : SpecificStorageNoTimeForDescription
{
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    => path = new MetaAbsolutePathDirectory(Directory.GetCurrentDirectory());
}
public class EditorUnityProjectRootParent : SpecificStorageNoTimeForDescription
{
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    => path = new MetaAbsolutePathDirectory(Directory.GetParent( Directory.GetCurrentDirectory()).FullName);
}

public class EditorRootSubFolder : SpecificStorageNoTimeForDescription
{
    public IMetaRelativePathDirectoryGet m_subFolder;
    public IMetaAbsolutePathDirectoryGet m_abstractFolder;

    public EditorRootSubFolder(IMetaRelativePathDirectoryGet subFolder)
    {
        IMetaAbsolutePathDirectoryGet root = new MetaAbsolutePathDirectory(Directory.GetCurrentDirectory());
        m_subFolder = subFolder;
        m_abstractFolder = E_FileAndFolderUtility.Combine(in root,  m_subFolder );


    }
    public EditorRootSubFolder(string subFolder) : this(new MetaRelativePathDirectory(subFolder))
    { }
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
        path = m_abstractFolder;
    }
}

public class EditorAssetsDirectory : EditorRootSubFolder
{ public EditorAssetsDirectory() : base("Assets") { } }
public class EditorTempFolder : EditorRootSubFolder
{ public EditorTempFolder() : base("Temp") { } }
public class EditorLibraryFolder : EditorRootSubFolder
{ public EditorLibraryFolder() : base("Library") { } }
public class EditorPackagesFolder : EditorRootSubFolder
{ public EditorPackagesFolder() : base("Packages") { } }
public class EditorDefaultCachePackagesStorageFolder : EditorRootSubFolder
{ public EditorDefaultCachePackagesStorageFolder() : base("Library/PackageCache") { }
    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        shortDescription = "Folder that contains all the packages use for the current project.";
    }
}
public class EditorProjectSettingFolder : EditorRootSubFolder
{ public EditorProjectSettingFolder() : base("ProjectSettings") { } }
public class EditorUserSettingFolder : EditorRootSubFolder
{public EditorUserSettingFolder() : base("UserSettings") {} }







public class EditorRootSubFolderFile : SpecificFileStorageNoTimeForDescription
{
    public IMetaFileNameWithExtensionGet m_fileName;
    public IMetaRelativePathDirectoryGet m_subFolder;
    public IMetaAbsolutePathFileGet m_abstractFilePath;


    public EditorRootSubFolderFile(string subFolder, string fileName, string extension) : this(new MetaRelativePathDirectory(subFolder)
        , new MetaFileNameWithExtension(fileName, extension))
    { }
    public EditorRootSubFolderFile(MetaRelativePathDirectory subFolder, MetaFileNameWithExtension fileName)
    {
        IMetaAbsolutePathDirectoryGet root = new MetaAbsolutePathDirectory(Directory.GetCurrentDirectory());
        m_fileName = fileName;
        m_subFolder = subFolder;
        m_abstractFilePath = E_FileAndFolderUtility.Combine(in root,new MetaRelativePathDirectory[] { subFolder }, m_fileName);
    }
    

    public override void GetPath(out IMetaAbsolutePathFileGet path)
    {
        path = m_abstractFilePath;
    }
}


public class EditorUnityPackageManifestFile : EditorRootSubFolderFile
{ public EditorUnityPackageManifestFile() : base("Packages", "manifest", "json") { } }

public class EditorUnityPackageManifestLockerFile : EditorRootSubFolderFile
{ public EditorUnityPackageManifestLockerFile() : base("Packages", "packages-lock", "json") { } }


public class EditorUnityProjectSettingsFile : EditorRootSubFolderFile
{ public EditorUnityProjectSettingsFile() : base("ProjectSettings", "ProjectSetting", "asset") { } }







public class WindowRuntimeExecutableFile : SpecificFileStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    {
        usualNameOfIt = "Application .Exe File";
    }

    public override void GetPath(out IMetaAbsolutePathFileGet path)
    {
        path = new MetaAbsolutePathFile(Application.dataPath.Replace("_Data", ".exe"));
    }

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        shortDescription = "The application start .exe file of Unity build";
    }
}
public class WindowRuntimeNearExecutableDataDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    {
        usualNameOfIt = "Application .Exe File";
    }
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
        path = new MetaAbsolutePathDirectory(Application.dataPath);
    }

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        shortDescription = "The application start .exe file of Unity build";
    }
}
public class WindowRuntimeNearExecutableDirectory : SpecificStorage
{
    public override void GetClassicNameOfIt(out string usualNameOfIt)
    {
        usualNameOfIt = "In Application .Exe File folder";
    }
    public override void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
        path = new MetaAbsolutePathDirectory(Application.dataPath+"/..");
    }

    public override void GetShortLineDescriptionOfIt(out string shortDescription)
    {
        shortDescription = "The folder containing the project";
    }
}





