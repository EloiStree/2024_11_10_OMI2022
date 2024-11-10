using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupOfAlias<T>
{
    public List<EnumAlias<T>> m_alias = new List<EnumAlias<T>>();
    public static T m_default;

    public GroupOfAlias(params EnumAlias<T>[] alias)
    {
        m_alias.AddRange(alias);
    }

    public void Get(string enumName, out bool enumFound, out T enumType)
    {
        enumType = m_default;
        enumFound = false;
        enumName = enumName.ToLower();

        for (int y = 0; y < m_alias.Count; y++)
        {
            m_alias[y].GetBoolable(enumName, out enumFound, out enumType);
            if (enumFound)
                return;
        }
    }

}

[System.Serializable]
public class EnumAlias<T>
{
    public EnumAlias(T e, params string[] alias)
    {
        m_enum = e;
        m_alias.AddRange(alias);
        m_alias.Add(e.ToString());
    }
    public T m_enum;
    public List<string> m_alias = new List<string>();
    public static T m_default;

    public void GetBoolable(string buttonName, out bool buttonNameFound, out T button)
    {
        button = m_default;
        buttonNameFound = false;
        buttonName = buttonName.ToLower();

        for (int y = 0; y < m_alias.Count; y++)
        {
            if (m_alias[y] == buttonName)
            {
                buttonNameFound = true;
                button = m_enum;
                return;
            }
        }
    }
}