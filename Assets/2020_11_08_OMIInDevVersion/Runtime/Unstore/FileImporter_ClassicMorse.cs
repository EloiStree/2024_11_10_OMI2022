using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_ClassicMorse : MonoBehaviour
{
    public ConditionBooleanMachineMorseStacker m_morseStacker;

    public void ClearRegister()
    {
        m_morseStacker.Clear();
    }

    public void LoadClassicMorse(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<MorseToStringCommand> found;
                LoadMorseToAction(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_morseStacker.AddMorseToListen(found[y]);
                }
            }
        }
    }

    private void LoadMorseToAction(string textToLoad, out List<MorseToStringCommand> foundMorse)
    {
        foundMorse = new List<MorseToStringCommand>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 4)
            {

                try
                {
                    if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(1).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(2).Trim())
                        && !string.IsNullOrEmpty(tokens.GetValue(3).Trim()))
                    {
                        ClassicBoolState found;

                        if (TextToBoolStateMachineParser.IsClassicParse(tokens.GetValue(0), out found))
                        {

                        }

                        MorseValue morse = new MorseValue(tokens.GetValue(1).Trim());

                        List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(tokens.GetValue(2).ToLower());
                        //ShogiParameter timeOfLongInMs;
                        //ShogiParameter.HasParam(optionsFoundInProcess, "LongTime", out timeOfLongInMs);
                        MorseToStringCommand morseInfo = new MorseToStringCommand(lines[i], found, morse, tokens.GetValue(3));
                        ShogiParameter tmp;

                        morseInfo.m_triggerOnFound = ShogiParameter.HasParam(optionsFoundInProcess, "triggeronfound");
                        morseInfo.m_triggerOnExit = ShogiParameter.HasParam(optionsFoundInProcess, "triggeronexit");

                        if (ShogiParameter.HasParam(optionsFoundInProcess, "longtime", out tmp))
                            morseInfo.m_longTimerInSecond = uint.Parse(tmp.GetValue()) / 1000f;
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "exittime", out tmp))
                            morseInfo.m_exitTimerInSecond = uint.Parse(tmp.GetValue()) / 1000f;
                        foundMorse.Add(morseInfo);
                    }

                }
                catch (Exception) { }
            }

        }
    }

}
