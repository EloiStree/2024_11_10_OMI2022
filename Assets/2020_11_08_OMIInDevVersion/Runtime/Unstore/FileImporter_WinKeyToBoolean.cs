using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WindowsInput.Native;

public class FileImporter_WinKeyToBoolean : MonoBehaviour
{
    public WindowKeyToBooleanStateMachine m_register;
    public void ClearRegister()
    {

        m_register.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<WindowKeyToListenTo> found;
                LoadKeyboardToBoolean(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_register.AddKey(found[y]);
                }
            }
        }
    }

    private void LoadKeyboardToBoolean(string textToLoad, out List<WindowKeyToListenTo> found)
    {
      
            found = new List<WindowKeyToListenTo>();
            string[] lines = textToLoad.Split('\n');
            TileLine tokens;
            WindowKeyToListenTo tmpState = null;
            for (int i = 0; i < lines.Length; i++)
            {
                char[] l = lines[i].Trim().ToCharArray();
                if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                    continue;
                tokens = new TileLine(lines[i]);
                if (tokens.GetCount() > 1)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim()) && !string.IsNullOrEmpty(tokens.GetValue(1).Trim()))
                        {

                            VirtualKeyCode kt = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), tokens.GetValue(0).ToUpper().Trim());
                            tmpState = new WindowKeyToListenTo( tokens.GetValue(1).Trim(), kt);
                            found.Add(tmpState);
                        }

                    }
                    catch (Exception) { Debug.LogWarning("Exception: " + textToLoad); }
                }
                
            }

        
    }
}
