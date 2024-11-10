using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class DesignerPlatformStorageChooseMono : DesignerPlatformStorageChoose
{

    private void Awake()
    {
        RefreshStoragePointer();
    }

    private void RefreshStoragePointer()
    {
        switch (m_windowEditorSelection)
        {
            case DefaultDesignerStorageChoose.WindowEditor.InRoot:
                m_inWindowEditor = new EditorUnityProjectRoot();
                break;
            case DefaultDesignerStorageChoose.WindowEditor.NearRoot:
                m_inWindowEditor = new EditorUnityProjectRootParent();
                break;
            case DefaultDesignerStorageChoose.WindowEditor.InAsset:
                m_inWindowEditor = new EditorAssetsDirectory();
                break;
            case DefaultDesignerStorageChoose.WindowEditor.InTemp:
                m_inWindowEditor = new UnityTempDataDirectory();
                break;
            default:
                break;
        }

        switch (m_macEditorSelection)
        {
            case DefaultDesignerStorageChoose.MacEditor.InRoot:
                m_inAppleEditor = new EditorUnityProjectRoot();
                break;
            case DefaultDesignerStorageChoose.MacEditor.NearRoot:
                m_inAppleEditor = new EditorUnityProjectRootParent();
                break;
            case DefaultDesignerStorageChoose.MacEditor.InAsset:
                m_inAppleEditor = new EditorAssetsDirectory();
                break;
            case DefaultDesignerStorageChoose.MacEditor.InTemp:
                m_inAppleEditor = new UnityTempDataDirectory();
                break;
            default:
                break;
        }

        switch (m_windowBuildSelection)
        {
            case DefaultDesignerStorageChoose.WindowBuild.DefaultData:
                m_onWindowBuild = new UnityDataPathDirectory();
                break;
            case DefaultDesignerStorageChoose.WindowBuild.Persistent:
                m_onWindowBuild = new UnityPersistentDataPathDirectory();
                break;
            case DefaultDesignerStorageChoose.WindowBuild.Temporary:
                m_onWindowBuild = new UnityTempDataDirectory();
                break;
            case DefaultDesignerStorageChoose.WindowBuild.NearExe:
                m_onWindowBuild = new WindowRuntimeNearExecutableDirectory();
                break;
            case DefaultDesignerStorageChoose.WindowBuild.InDataNearExe:
                m_onWindowBuild = new WindowRuntimeNearExecutableDataDirectory();
                break;
            default:
                break;
        }

        switch (m_androidBuildSelection)
        {
            case DefaultDesignerStorageChoose.AndroidBuild.ExternalSDCard:
                m_onAndroidBuild = new AndroidSDCardFileSystemDirectory();
                break;
            case DefaultDesignerStorageChoose.AndroidBuild.FileSystem:
                m_onAndroidBuild = new AndroidRootFileSystemDirectory();
                break;
            case DefaultDesignerStorageChoose.AndroidBuild.DataPath:
                m_onAndroidBuild = new UnityDataPathDirectory();
                break;
            case DefaultDesignerStorageChoose.AndroidBuild.PersistentDataPath:
                m_onAndroidBuild = new UnityPersistentDataPathDirectory();
                break;
            default:
                break;
        }
        switch (m_webGlBuildSelection)
        {
            case DefaultDesignerStorageChoose.WebGl.DefaultData:
                m_onWebglBuild = new WebGlDataStorageDirectory();
                break;
            default:
                break;
        }
    }

    public void SwitchStorageRandomlyForDebugTest()
    {
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_windowEditorSelection);
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_macEditorSelection);
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_windowBuildSelection);
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_macBuildSelection);
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_androidBuildSelection);
        Eloi.E_UnityRandomUtility.GetRandomOfEnum( out m_webGlBuildSelection);
        RefreshStoragePointer();
    }
}


public abstract class DesignerPlatformStorageChoose :
    MonoBehaviour, IMetaAbsolutePathDirectoryGet, IOwnsConventionalFolderStorage
{

    public DefaultDesignerStorageChoose.WindowEditor m_windowEditorSelection;
    public IConventionalFolderStorage m_inWindowEditor;
    public DefaultDesignerStorageChoose.MacEditor m_macEditorSelection;
    public IConventionalFolderStorage m_inAppleEditor;

    public DefaultDesignerStorageChoose.WindowBuild m_windowBuildSelection;
    public IConventionalFolderStorage m_onWindowBuild;
    public DefaultDesignerStorageChoose.MacBuild m_macBuildSelection;
    public IConventionalFolderStorage m_onAppleBuild;

    public DefaultDesignerStorageChoose.AndroidBuild m_androidBuildSelection;
    public IConventionalFolderStorage m_onAndroidBuild;
    public DefaultDesignerStorageChoose.WebGl m_webGlBuildSelection;
    public IConventionalFolderStorage m_onWebglBuild;

    public void GetFolderStorage(out IConventionalFolderStorage storageChoose)
    {
            storageChoose = null;
            if (Application.isEditor)
            {
                Eloi.E_CodeTag.ToCodeLaterWhenCodeIsReady.Info("Add a compatibility for Mac editor");
                storageChoose = m_inWindowEditor;
                return;
            }
#if UNITY_STANDALONE_WIN
        storageChoose = m_onWindowBuild;
#elif UNITY_ANDROID
        storageChoose =m_onAndroidBuild;
#elif UNITY_WEBGL
       storageChoose = m_onWebglBuild;
#endif
        
    }

    public void GetPath(out IMetaAbsolutePathDirectoryGet path)
    {
        GetFolderStorage(out IConventionalFolderStorage sourcePath);
        if (sourcePath != null)
            sourcePath.GetPath(out path);
        else path = null;
    }
    public void GetPath(out string path)
    {
        GetPath(out IMetaAbsolutePathDirectoryGet p);
        if (p != null)
            p.GetPath(out path);
        else path = "";
    }

    public string GetPath()
    {
        GetPath(out string p);
        return p;
    }
}

public class DefaultDesignerStorageChoose
{
    public enum WindowBuild { DefaultData, Persistent, Temporary, NearExe, InDataNearExe }
    public enum MacBuild { DefaultData, Persistent, Temporary }
    public enum AndroidBuild { ExternalSDCard, FileSystem, DataPath, PersistentDataPath }
    public enum MacEditor { InRoot, NearRoot, InAsset, InTemp }
    public enum WindowEditor { InRoot, NearRoot, InAsset, InTemp}
    public enum WebGl { DefaultData }
 

}