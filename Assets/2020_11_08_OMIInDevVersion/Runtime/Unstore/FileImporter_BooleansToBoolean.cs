using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_BooleansToBoolean : MonoBehaviour
{
    public BooleansToBooleanMono m_register;

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
                List<BooleansToBoolean> found;
                LoadAndBooleanToState(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_register.Add(found[y]);
                }

            }
        }
    }
    private void LoadAndBooleanToState(string textToLoad, out List<BooleansToBoolean> found)
    {

        found = new List<BooleansToBoolean>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        string tmpCondition;
        string tmpBooleanToChange;
        ClassicBoolState tmpState;
        BooleansToBoolean tmpBoolToState;
        for (int i = 0; i < lines.Length; i++)
        {
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() > 1)
            {
                tmpBoolToState = new BooleansToBoolean();
                tmpCondition = tokens.GetValue(0).Trim();
                tmpBooleanToChange = tokens.GetValue(1).Trim();
                if (tmpCondition.Length > 0 && tmpBooleanToChange.Length > 0) { 
                    if (TextToBoolStateMachineParser.IsClassicParse(tmpCondition, out tmpState))
                    {
                        tmpBoolToState.m_andBooleanState = tmpState;
                        tmpBoolToState.m_booleanToAffect = tmpBooleanToChange;

                        found.Add(tmpBoolToState);
                    }
                }

            }

        }

    }
}
