using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class StringKeyPropertyStatic
{
    public static Action m_onScriptNotifyStaticMajorChange;
    public static Action m_onAnyChangeStaticChange;
    public static Dictionary<string, StringKeyPropertyString> m_stringKey = new Dictionary<string,StringKeyPropertyString>();
    public static void PushIn(List<StringKeyPropertyString> stringKeys)
    {
        for (int i = 0; i < stringKeys.Count; i++)
        {
            string id = stringKeys[i].m_key.ToLower().Trim();
            if (!m_stringKey.ContainsKey(id))
                m_stringKey.Add(id, stringKeys[i]);
            else
                m_stringKey[id] = stringKeys[i];
        }
        if(m_onAnyChangeStaticChange!=null)
            m_onAnyChangeStaticChange.Invoke();
        if (m_onScriptNotifyStaticMajorChange != null)
            m_onScriptNotifyStaticMajorChange.Invoke();
    }

    public void NotifyMajorStaticChange()
    {
        m_onScriptNotifyStaticMajorChange.Invoke();
    }

    public static void Get( in string id, out bool found, out string value, string defaultValue="")
    {
        string idl = id.ToLower().Trim();
        found = m_stringKey.ContainsKey(idl);
        value = defaultValue;
        if (found)
            value = m_stringKey[idl].m_value;

    }

    public static void GetFromStringCollectionAsDouble(in string stringId, out bool foundAndConverted, out double value)
    {
        value = 0;
        Get(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && double.TryParse(tValue, out value));
    }

    private static string mTrue = "true";
    private static string mFalse = "false";
    private static string m1 = "1";
    private static string m0 = "0";
    public static void GetFromStringCollectionAsBool(in string stringId, out bool foundAndConverted, out bool value)
    {
        value = false;
        Get(in stringId, out bool found, out string tValue, "");
        if (Eloi.E_StringUtility.AreEquals(in tValue, in mTrue, true, true)
            && Eloi.E_StringUtility.AreEquals(in tValue, in m1, true, true))
        {
            foundAndConverted = true; value = true;
        }
        else if (Eloi.E_StringUtility.AreEquals(in tValue, in mFalse, true, true)
           && Eloi.E_StringUtility.AreEquals(in tValue, in m0, true, true))
        {
            foundAndConverted = true; value = false;
        }
        else
        {
            foundAndConverted = (found && bool.TryParse(tValue, out value));
        }
    }

    public static void GetFromStringCollectionAsFloat(in string stringId, out bool foundAndConverted, out float value)
    {
        value = 0;
        Get(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && float.TryParse(tValue, out value));
    }
    public static void GetFromStringCollectionAsInt(in string stringId, out bool foundAndConverted, out int value)
    {
        value = 0;
        Get(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && int.TryParse(tValue, out value));
    }
    public static void GetFromStringCollectionAsUlong(in string stringId, out bool foundAndConverted, out ulong value)
    {
        value = 0;
        Get(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && ulong.TryParse(tValue, out value));
    }

   
}

public class DefaultApplicationConfigurationStringKeyValueMono : MonoBehaviour
{
    public Eloi.AbstractMetaAbsolutePathFileMono m_filePath;
    //public string m_configNameId = "DefaultApplicationName";
    //public string m_fileRelativePathDev = "configuration/defaultapplication.keyproperty";
    public TextAsset m_defaultConfigIfFileDontExist;

    public bool autoImportAwake=true;

    public bool pushValueInStaticAccess=true;

    [Header("Debug")]
    public StringKeyPropertyGroup m_application = new StringKeyPropertyGroup("DefaultApplicationConfig");
    public StringKeyPropertyGroupEvent m_onImportDetected;
    public StringKeyPropertyGroupEvent m_onExportDetected;

    public void Awake()
    {
        if (autoImportAwake) {
            Import();
        }
        if (pushValueInStaticAccess) { 
        
            StringKeyPropertyFacade.SetOrOverride(in m_application);
            
            StringKeyPropertyStatic.PushIn(m_application.m_stringKeys);
             
        }

    }


    [ContextMenu("Import")]
    public  void Import()
    {
        string path = GetPathToUse();
        string directoryName = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
        if (!File.Exists(path)) {
            //StringKeyPropertyImport.ImportFromText( m_defaultConfigIfFileDontExist.text, m_configNameId,out bool converted, out m_application);
            //StringKeyPropertyImport.Export(in path, in m_application);
            File.WriteAllText(path, m_defaultConfigIfFileDontExist.text);

        }
        StringKeyPropertyImport.Import(in path, out bool found, out m_application);
        if (!found && m_defaultConfigIfFileDontExist != null)
        {
            File.WriteAllText(path, m_defaultConfigIfFileDontExist.text);
        }
        if (!found && m_defaultConfigIfFileDontExist == null)
        {
            File.WriteAllText(path,"");
        }
        m_onImportDetected.Invoke(m_application);
    }

    [ContextMenu("OpenFolder")]
    public void OpenFolder()
    {

        Application.OpenURL(Path.GetDirectoryName(GetPathToUse()));
    }
    [ContextMenu("OpenFile")]
    public void OpenFile()
    {

        Application.OpenURL(GetPathToUse());
    }
    private string GetPathToUse()
    {
      //  Eloi.E_FilePathUnityUtility.MeltPathTogether(out string path, Directory.GetCurrentDirectory(), m_fileRelativePathDev);
        return m_filePath.GetPath();
    }

    public void OnDestroy()
    {
        if (pushValueInStaticAccess) {
            m_application = StringKeyPropertyFacade.Get(m_application.m_name);
        }
        
    }

    [ContextMenu("Export")]
    public void Export()
    {
        string path = GetPathToUse();
        StringKeyPropertyImport.Export(in path, in m_application);
        m_onExportDetected.Invoke(m_application);
    }

    public void DebugLogExport(StringKeyPropertyGroup group)
    {
        Debug.Log("Export: " + group.m_name);
    }
    public void DebugLogImport(StringKeyPropertyGroup group)
    {
        Debug.Log("Import: " + group.m_name);
    }

    public StringKeyPropertyGroup GetConfigurationRef() { return m_application; }
}
[System.Serializable]
public class StringKeyPropertyGroupEvent : UnityEvent<StringKeyPropertyGroup>
{

}