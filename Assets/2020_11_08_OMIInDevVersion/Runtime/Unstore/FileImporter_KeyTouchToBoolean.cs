using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_KeyTouchToBoolean : MonoBehaviour
{
    public KeyStrokeToBooleanStateMachine m_keyboardToBoolean;
  
    public void ClearRegister()
    {

        m_keyboardToBoolean.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<KeyboardToBooleanState> found;
                LoadKeyboardToBoolean(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_keyboardToBoolean.Add(found[y]);
                }
            }
        }
    }

    public  void LoadKeyboardToBoolean(string textToLoad, out List<KeyboardToBooleanState> found)
    {
        found = new List<KeyboardToBooleanState>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        KeyboardToBooleanState tmpState = null;
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

                        KeyboardTouch kt = (KeyboardTouch)Enum.Parse(typeof(KeyboardTouch), tokens.GetValue(0).Trim());
                        tmpState = new KeyboardToBooleanState(kt, tokens.GetValue(1).Trim());
                        found.Add(tmpState);
                    }

                }
                catch (Exception) { Debug.LogWarning("Exception: " + textToLoad); }
            }
            if (tmpState != null && tokens.GetCount() > 2)
            {
                List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(tokens.GetValue(2));
                if (ShogiParameter.HasParam(optionsFoundInProcess, "ToggleOnTrue"))
                {

                    tmpState.m_toggleOnTrue = true;
                }
                if (ShogiParameter.HasParam(optionsFoundInProcess, "ToggleOnFalse"))
                {

                    tmpState.m_toggleOnFalse = true;
                }

            }
        }

    }

}
