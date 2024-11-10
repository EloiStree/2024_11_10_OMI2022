using Eloi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Experiment_DesignerSubStorage : MonoBehaviour
{

    public DesignerPlatformStorageChooseMono m_whereToStore;
    public Eloi.MetaRelativePathDirectory m_whereToStoreRelatively;
    public Eloi.MetaFileNameWithExtension m_fileName;

    public Text m_debugState;
    public Text m_nameDebug;
    public Text m_descriptionDebug;
    public Text m_pathDebug;
    public Text m_filePathDebug;

    void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        m_debugState.text = "";

        m_whereToStore.GetFolderStorage(out IConventionalFolderStorage whereToStore);
        m_debugState.text = "Storage:" + whereToStore + "\n" + m_debugState.text;

        if (whereToStore == null)
            return;
        whereToStore.GetClassicNameOfIt(out string name);
        m_nameDebug.text = name;
        whereToStore.GetShortLineDescriptionOfIt(out string description);
        m_nameDebug.text = description;
        whereToStore.GetPath(out Eloi.IMetaAbsolutePathDirectoryGet mPath);

        if (mPath != null)
        {
            mPath.GetPath(out string path);
            m_debugState.text = "Path:" + path + "\n" + m_debugState.text;
            m_pathDebug.text = path;
        }
        else
            m_debugState.text = "Path: No Path\n" + m_debugState.text;
    }

    public void OpenCurrentSelection() {

        if (m_whereToStore != null) { 
            m_whereToStore.GetFolderStorage(out IConventionalFolderStorage whereToStore);
            if(whereToStore != null)
                whereToStore.TryToOpenFolder();
        }
    }

    public void CreateTextFileAndOpenIt() {
        m_whereToStore.GetFolderStorage(out IConventionalFolderStorage whereToStore);
        if (whereToStore == null)
            return;
          whereToStore.GetPath(out Eloi.IMetaAbsolutePathDirectoryGet mPath);
        if (mPath != null)
        {
            mPath.GetPath(out string path);
           IMetaAbsolutePathFileGet file = Eloi.E_FileAndFolderUtility.Combine(in mPath, new Eloi.IMetaRelativePathDirectoryGet[]{ m_whereToStoreRelatively}, m_fileName);
            if (file != null) {
                string p = file.GetPath();
                if (E_StringUtility.IsFilled(p)) {
                    Directory.CreateDirectory(Path.GetDirectoryName(p));
                    m_filePathDebug.text = file.GetPath();

                   File.WriteAllText(file.GetPath(), "Test");
                    Application.OpenURL(file.GetPath());
                    if (Application.platform == RuntimePlatform.Android) { 
                    Application.OpenURL("file:///" + file.GetPath());
                    AndroidOpenUrl.OpenFile(file.GetPath());
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) { 
            m_whereToStore.SwitchStorageRandomlyForDebugTest();
            RefreshUI();
        }
    }
}
