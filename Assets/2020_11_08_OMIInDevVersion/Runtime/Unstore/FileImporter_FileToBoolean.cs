using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_FileToBoolean : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public List<FileValueToBoolean> m_loaded = new List<FileValueToBoolean>();

    public void ClearRegister() {
        m_loaded.Clear();
    }

    public void Load(params string [] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i])) {
                List<FileValueToBoolean> found;
                LoadFileToBoolean(File.ReadAllText(filePath[i]), out found );
                m_loaded.AddRange(found);
            }
        }
    }

    public void ApplyToBooleanStateMachine() {
        if (m_register == null) return;
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        if (reg == null) return;
        for (int i = 0; i < m_loaded.Count; i++)
        {
            reg.Set(m_loaded[i].m_booleanName, m_loaded[i].m_wantedValue);
        }

    }

    private void LoadFileToBoolean(string textToLoad, out List<FileValueToBoolean> found)
    {
         found = new List<FileValueToBoolean>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 2 && !string.IsNullOrEmpty(tokens.GetValue(0).Trim()) && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
            {
                string isTrueAsString = tokens.GetValue(1).Trim().ToLower();
                bool isTrue = isTrueAsString == "1" ||
                    isTrueAsString == "true";
                found.Add(new FileValueToBoolean(tokens.GetValue(0).Trim(), isTrue));

            }
        }
    }

}

[System.Serializable]
public class FileValueToBoolean {
    public string m_booleanName;
    public bool m_wantedValue;

    public FileValueToBoolean(string booleanName, bool wantedValue)
    {
        this.m_booleanName = booleanName;
        this.m_wantedValue = wantedValue;
    }
}
