using System;
using UnityEngine;
using UnityEngine.Events;

public class DirectoryStorageLoadAndSave : MonoBehaviour , I_UseHarddriveSave
{
    public string m_folderName="DirectoryStorage";
    public string m_fileName="default.txt";
    public bool m_saveOnHarddrive = false;
    public MonoBehaviour m_savableTargeted;
    public I_TextSavable m_saver;

    [Header("When to load")]
    public bool m_loadOnAwake = true;
    public bool m_loadOnStart = true;
    public bool m_loadOnEnable = true;
    [Header("When to save")]
    public bool m_loadOnDisable = false;
    public bool m_loadOnFocusLeave = true;
    public bool m_loadOnFocusQuit = true;
    public bool m_loadOnDestroy = false;
    [Header("Late Load")]
    public bool m_useLateLoad;
    public float m_timeToLateLoad = 0.3f;

    public AccessToSaveEvent m_onLoadEvent;
    public AccessToSaveEvent m_onSaveEvent;

    private void Awake()
    {
        if (m_loadOnAwake)
            LoadSavableOnDeviceAndNotify();
        if(m_useLateLoad)
            Invoke("LoadSavableOnDeviceAndNotify", m_timeToLateLoad);
    }
    private void Start()
    {
        if (m_loadOnStart)
            LoadSavableOnDeviceAndNotify();
    }
    private void OnEnable()
    {
        if (m_loadOnEnable)
            LoadSavableOnDeviceAndNotify();
    }

    private void OnDisable()
    {
        if (m_loadOnDisable)
            LoadSavableOnDeviceAndNotify();

    }

    private void OnApplicationFocus(bool focus)
    {
        if (m_loadOnFocusLeave)
            SaveSavableOnDevice();

    }
    private void OnApplicationQuit()
    {
        if (m_loadOnFocusQuit)
            SaveSavableOnDevice();
    }

    private void OnDestroy()
    {
        if (m_loadOnDestroy)
            SaveSavableOnDevice();
    }



    public string LoadTextOnDevice() {
        return UnityDirectoryStorage.LoadFile(m_folderName, m_fileName, m_saveOnHarddrive);
    }


    public void SaveTextOnDevice(string text)
    {
        UnityDirectoryStorage.SaveFile(m_folderName, m_fileName, text, m_saveOnHarddrive);
    }

    private void Reset()
    {
        m_fileName = UnityDirectoryStorage.GetRandomFileName(".txt");
        
        m_savableTargeted = GetComponent<I_TextSavable>() as MonoBehaviour;

    }

    public void SetSwitchTo(bool useHarddrive)
    {
        m_saveOnHarddrive = useHarddrive;
    }

    public bool IsUsingHarddriveSave()
    {
        return m_saveOnHarddrive;
    }


    private void LoadSavableOnDeviceAndNotify()
    {
        string t = LoadTextOnDevice();
        if (t == null || t.Length <= 0)
            t = GetDefaultTextFromUnity();
        m_onLoadEvent.Invoke(t);
        SetUnitySavableText(t);
    }

  
    private void SaveSavableOnDevice()
    {
        string t = GetUnitySavableText();
        m_onSaveEvent.Invoke(t);
        SaveTextOnDevice(t);


    }

    private void SetUnitySavableText(string text)
    {
        m_saver = m_savableTargeted as I_TextSavable;
        if (m_saver != null)
            m_saver.SetTextFromLoad(text);
    }
    private string GetUnitySavableText()
    {
        m_saver = m_savableTargeted as I_TextSavable;
        if (m_saver != null)
            return m_saver.GetSavableText();
        return "";
    }


    private string GetDefaultTextFromUnity()
    {
        m_saver = m_savableTargeted as I_TextSavable;
        if (m_saver != null)
            return m_saver.GetSavableDefaultText();
        return "";
    }

    public void ResetToDefault(bool reload) {

        SaveTextOnDevice(GetDefaultTextFromUnity());
        if(reload)
            LoadSavableOnDeviceAndNotify();
    }
    [System.Serializable]
    public class AccessToSaveEvent : UnityEvent<string> { }
}
