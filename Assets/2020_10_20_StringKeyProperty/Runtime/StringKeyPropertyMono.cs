using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class StringKeyPropertyMono : MonoBehaviour
{


    //public string 
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

public class StringKeyPropertyFacade {

   // private static StringKeyPropertyGroup m_all = new StringKeyPropertyGroup("All");
    private static List<StringKeyPropertyGroup> m_instances= new List<StringKeyPropertyGroup>();

    //public static StringKeyPropertyGroup All
    //{
    //    get { return m_all; }
    //    set { m_all = value; }
    //}


    public static void GetExistAndIsAlone(out bool isAlone, out bool existAtleastOne)
    {
        isAlone = m_instances.Count == 1;
        existAtleastOne = m_instances.Count > 0;
    }
    public static bool IsInstanceAlone()
    {
        return m_instances.Count == 1;
    }
    public static bool IsAtLeastOneInstance()
    {
        return m_instances.Count > 0;
    }
    public static void GetFirst(out bool isAlone, out bool existAtleastOne, out StringKeyPropertyGroup group)
    {

        isAlone = IsInstanceAlone();
        existAtleastOne = IsAtLeastOneInstance();
        if (existAtleastOne)
            group = m_instances[0];
        else group = null;
    }
    public static StringKeyPropertyGroup GetFirst()
    {
       bool existAtleastOne = IsAtLeastOneInstance();
        if (existAtleastOne)
        return m_instances[0];
        else 
        return   null;
    }

    public static StringKeyPropertyGroup Get(in string keyPropertyNameId) {
        if (m_instances == null)
            return null;
        foreach (var item in m_instances)
        {
            if (Eloi.E_StringUtility.AreEquals(in item.m_name, in keyPropertyNameId, true, true)) {
                return item;
            }
        }
        return null;
    }
    public static void SetOrOverride(in StringKeyPropertyGroup group) {
        for (int i = 0; i < m_instances.Count; i++)
        {
            if (group == m_instances[i] || Eloi.E_StringUtility.AreEquals(group.m_name, m_instances[i].m_name,true,true) ) {
                m_instances[i] = group;
                return;
            }
        }
        m_instances.Add(group);
    }
}


public class StringKeyPropertyImport {


    public static void Export(in string filePath, in StringKeyPropertyGroup toExport) {

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(">nameid:" + toExport.m_name);
        for (int i = 0; i < toExport.m_stringKeys.Count; i++)
        {
            sb.AppendLine(toExport.m_stringKeys[i].m_key + ":" + toExport.m_stringKeys[i].m_value);

        }
        File.WriteAllText(filePath, sb.ToString());
    }


    public static void ImportFromText(string text, string nameIfNotInFile,out  bool converted, out StringKeyPropertyGroup created)
    {
        created = new StringKeyPropertyGroup(nameIfNotInFile);

        int i = 0;
        string leftPart = "", rightPart = "";
        foreach (string line in text.Split('\n','\r'))
        {
            if (Eloi.E_StringUtility.StartWith(line.Trim(), "\\", true, true))
            {
            }
            else if (Eloi.E_StringUtility.StartWith( line, ">nameid:", true, true))
            {
                created.m_name = line.Substring(">nameid:".Length);
            }
            else
            {
                int indexOfDoubleComma = line.IndexOf(':');

                if (indexOfDoubleComma > 0)
                {
                    Eloi.E_StringUtility.SplitInTwo(in line, in indexOfDoubleComma, out leftPart, out rightPart);
                    leftPart = leftPart.Trim();
                    rightPart = rightPart.Trim();
                    PushIn(in created, in leftPart, in rightPart);
                }
            }
            i++;
        }
        converted = true;

    }
    public static void Import(in string filePath,out bool found, out StringKeyPropertyGroup group) {

        if (!File.Exists(filePath))
        {
            found = false;
            group = null;
            return;
        }
        string text = File.ReadAllText(filePath);
        ImportFromText(text, Path.GetFileNameWithoutExtension(filePath),out found, out group);


    }


    public static void PushIn( in StringKeyPropertyGroup group, in string key, in string value)
    {
            if (Eloi.E_StringUtility.IsFilled(in key)) {
                group.AppendString(in key, in value);
            }
    }
}


[System.Serializable]
public class StringKeyPropertyGroup{
    public string m_name;
     public List<StringKeyPropertyString> m_stringKeys = new List<StringKeyPropertyString>();

    public void GetFromStringCollection(in string keyPropertyNameId, out bool found, out string value, string defaultValue = "")
    {

        foreach (var item in m_stringKeys)
        {
            if (Eloi.E_StringUtility.AreEquals(in item.m_key, in keyPropertyNameId, true, true))
            {
                found = true;
                value = item.m_value;
                return;
            }
        }
        found = false;
        value = defaultValue;
    }
   
   

    public void AppendString(in string key, in string value)
    {
        for (int i = 0; i < m_stringKeys.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(in m_stringKeys[i].m_key, in key, true, true))
            {
                m_stringKeys[i].m_value =value.ToString();
                return;
            }
        }
        m_stringKeys.Add(new StringKeyPropertyString(key, value));
    }

    public void GetFromStringCollectionAsDouble(in string stringId, out bool foundAndConverted, out double value)
    {
        value = 0;
        GetFromStringCollection(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && double.TryParse(tValue, out value));
    }

    private static string mTrue = "true";
    private static string mFalse = "false";
    private static string m1 = "1";
    private static string m0 = "0";
    public void GetFromStringCollectionAsBool(in string stringId, out bool foundAndConverted, out bool value)
    {
        value = false;
        GetFromStringCollection(in stringId, out bool found, out string tValue, "");
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
        else { 
            foundAndConverted = (found && bool.TryParse(tValue, out value));
        }
    }

    public void GetFromStringCollectionAsFloat(in string stringId, out bool foundAndConverted, out float value)
    {
        value = 0;
        GetFromStringCollection(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && float.TryParse(tValue, out value));
    }
    public void GetFromStringCollectionAsInt(in string stringId, out bool foundAndConverted, out int value)
    {
        value = 0;
        GetFromStringCollection(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && int.TryParse(tValue, out value));
    }
    public void GetFromStringCollectionAsUlong(in string stringId, out bool foundAndConverted, out ulong value)
    {
        value = 0;
        GetFromStringCollection(in stringId, out bool found, out string tValue, "");
        foundAndConverted = (found && ulong.TryParse(tValue, out value));
    }

    public void SetStringCollectionAsFloatInIfFound(in string stringId, ref float target)
    {
        GetFromStringCollectionAsFloat(in stringId, out bool valide, out float value);
        if (valide) target = value;
    }
    public void SetStringCollectionAsIntInIfFound(in string stringId, ref int target)
    {
        GetFromStringCollectionAsInt(in stringId, out bool valide, out int value);
        if (valide) target = value;
    }

    public void SetStringInCollectionAsTextIfFound(in string key, in string value)
    {
        for (int i = 0; i < m_stringKeys.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(
                in m_stringKeys[i].m_key, in key, true, true)) {
                m_stringKeys[i].m_value = value;
                return;
            }
        }
    }

    public void SetOrAddStringInCollectionAsText(in string key, in string value)
    {
        for (int i = 0; i < m_stringKeys.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(
                in m_stringKeys[i].m_key, in key, true, true))
            {
                m_stringKeys[i].m_value = value;
                return;
            }
        }
        m_stringKeys.Add(new StringKeyPropertyString(key, value));
    }

    public StringKeyPropertyGroup(string m_name)
    {
        this.m_name = m_name;
    }
}

public class StringKeyProperty <T>{
    public string m_key;
    public T m_value;

    public StringKeyProperty(string key, T value)
    {
        this.m_key = key;
        this.m_value = value;
    }
}
[System.Serializable]
public class StringKeyPropertyBoolean : StringKeyProperty<bool>
{
    public StringKeyPropertyBoolean(string key, bool value) : base(key, value)
    {
    }
}
[System.Serializable]
public class StringKeyPropertyDouble : StringKeyProperty<double>
{
    public StringKeyPropertyDouble(string key, double value) : base(key, value)
    {
    }
}
[System.Serializable]
public class StringKeyPropertyUlong : StringKeyProperty<ulong>
{
    public StringKeyPropertyUlong(string key, ulong value) : base(key, value)
    {
    }
}
[System.Serializable]
public class StringKeyPropertyString : StringKeyProperty<string>
{
    public StringKeyPropertyString(string key, string value) : base(key, value)
    {
    }
}
