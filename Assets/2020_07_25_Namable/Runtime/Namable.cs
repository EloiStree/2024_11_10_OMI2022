
using System.Collections.Generic;

public interface INamable
{
    string GetName();
    void SetName(string name);
}
[System.Serializable]
public class Namable : INamable
{
    public string m_name;

    public string GetName()
    {
        return m_name;
    }

    public void SetName(string name)
    {
        m_name = name;
    }

    public static T GetFirstInCollection<T>(string name, IEnumerable<T> names, bool ignoringCase, bool useTrim) where T : class, INamable
    {
        name = FormatName(name, ignoringCase, useTrim);
        foreach (T namable in names)
        {
            if (name == FormatName(namable.GetName(), ignoringCase, useTrim))
                return namable;
        }
        return null;
    }
    public static List<T> GetInCollection<T>(string name, IEnumerable<T> names, bool ignoringCase, bool useTrim) where T : INamable
    {
        List<T> result = new List<T>();
        name = FormatName(name, ignoringCase, useTrim);
        foreach (T namable in names)
        {
            if (name == FormatName(namable.GetName(), ignoringCase, useTrim))
                result.Add(namable);
        }
        return result;
    }

    private static string FormatName(string name, bool ignoringCase, bool useTrim)
    {
        if (ignoringCase)
            name = name.ToLower();
        if (useTrim)
            name = name.Trim();
        return name;
    }
}
