using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_LinearCondition : MonoBehaviour
{
    public LinearConditionRegister m_linearRegistered;
    public void ClearRegister()
    {
        m_linearRegistered.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<LinearCondition> found;
                LoadLinearCondition(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_linearRegistered.AddLinearCondition(found[y]);
                }

            }
        }
    }
    private void LoadLinearCondition(string textToLoad, out  List<LinearCondition> foundlinear)
    {
        //MouseN ➤ MouseO ➤ MouseS ➤ MouseE ♦ Tab↓ Tab↑ ♦ ☗ExitTime 500
        foundlinear = new List<LinearCondition>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() == 3)
            {


                if (!string.IsNullOrEmpty(tokens.GetValue(0).Trim())
                    && !string.IsNullOrEmpty(tokens.GetValue(1).Trim())
                    && !string.IsNullOrEmpty(tokens.GetValue(2).Trim())
                    )
                {
                    try
                    {
                        LinearCondition condition = new LinearCondition();
                        string[] conditionToken = tokens.GetValue(0).Split('➤');
                        for (int j = 0; j < conditionToken.Length; j++)
                        {
                            ClassicBoolState found;

                            if (TextToBoolStateMachineParser.IsClassicParse(conditionToken[j].Trim(), out found))
                            {
                                condition.Add(found);
                            }

                        }
                        condition.m_actionAsText = tokens.GetValue(1).Trim();
                        List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(tokens.GetValue(2).Trim());
                        ShogiParameter tmp;

                        if (ShogiParameter.HasParam(optionsFoundInProcess, "GlobalExitTime", out tmp))
                            condition.m_globalExitTimeInMs = uint.Parse(tmp.GetValue());
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "StepExitTime", out tmp))
                            condition.m_stepExitTimeInMs = uint.Parse(tmp.GetValue());
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "BreakTime", out tmp))
                            condition.m_breakTimeInMs = uint.Parse(tmp.GetValue());
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "Cooldown", out tmp))
                            condition.m_cooldownInMs = uint.Parse(tmp.GetValue());
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnFound", out tmp))
                            condition.m_triggerOnFound = true;
                        if (ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnExit", out tmp))
                            condition.m_triggerOnExit = true;
                        foundlinear.Add(condition);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }
    }


}
