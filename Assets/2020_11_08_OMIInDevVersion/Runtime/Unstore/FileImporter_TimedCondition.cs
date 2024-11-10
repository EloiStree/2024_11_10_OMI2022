using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_TimedCondition : MonoBehaviour
{

    public TimedConditionMono m_timeManager;

    public void ClearRegister()
    {
        m_timeManager.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<TimedConditionBetween> foundbetween;
                List<TimedConditionBeforeAfter> foundbefore;
                LoadTimeCondition(File.ReadAllText(filePath[i]), out foundbetween, out foundbefore);
                for (int y = 0; y < foundbetween.Count; y++)
                {

                    m_timeManager.Add(foundbetween[y]);
                }
                for (int y = 0; y < foundbefore.Count; y++)
                {

                    m_timeManager.Add(foundbefore[y]);
                }
            }
        }
    }
    private void LoadTimeCondition(string textToLoad, out List<TimedConditionBetween> timeBetween, out List<TimedConditionBeforeAfter> timeBeforeAfter)
    {
        timeBetween = new List<TimedConditionBetween>();
        timeBeforeAfter = new List<TimedConditionBeforeAfter>();
      
        //MouseN + MouseO  ♦ Tab↓ Tab↑ ♦ ☗MinTime 1000 ☗MaxTime 3000 ☗TriggerOnRange
        //MouseN + MouseO  ♦ Click↓ Click↑ ♦ ☗MinTime 1000 ☗MaxTime 3000  ☗TriggerOnRange ☗TriggerOutOfRange
        //MouseN + MouseO  ♦ Tab↓ Tab↑ ♦ ☗Time 500   ☗TriggerOnReleaseInRange  
        //MouseN + MouseO  ♦ Tab↓ Tab↑ ♦ ☗Time 100   ☗TriggerOnReleaseInRange 
        //MouseN + MouseO  ♦ Tab↓ Tab↑ ♦ ☗MinTime 1000 ☗MaxTime 3000 

        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        string action = "";
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
                    ClassicBoolState conditionFound = null;
                    action = tokens.GetValue(1).Trim();

                    try
                    {


                        if (TextToBoolStateMachineParser.IsClassicParse(tokens.GetValue(0).Trim(), out conditionFound))
                        {
                            List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(tokens.GetValue(2).Trim());
                            ShogiParameter tmp;

                            bool triggerOnRange = ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnRange");
                            bool triggerOnOutOfRange = ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOutOfRange");
                            bool triggerOnRelease = ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnReleaseInRange");
                            string typeAsString = "";

                            if (ShogiParameter.HasParam(optionsFoundInProcess, "Type", out tmp))
                                typeAsString = tmp.GetValue().Trim().ToLower();


                            if (ShogiParameter.HasParam(optionsFoundInProcess, "Time"))
                            {
                                TimedConditionBeforeAfter timedCondition = new TimedConditionBeforeAfter();

                                if (ShogiParameter.HasParam(optionsFoundInProcess, "Time", out tmp))
                                    timedCondition.m_timeInMs = uint.Parse(tmp.GetValue());
                                timedCondition.m_condition = conditionFound;
                                timedCondition.m_actionToTrigger = action;
                                timedCondition.m_triggerOnInRange = triggerOnRange;
                                timedCondition.m_triggerOnOutRange = triggerOnOutOfRange;
                                timedCondition.m_triggerOnReleaseInRange = triggerOnRelease;
                                if (typeAsString == "before")
                                    timedCondition.m_timeConditionType = TimedConditionBeforeAfter.TimeType.Before;
                                else
                                    timedCondition.m_timeConditionType = TimedConditionBeforeAfter.TimeType.After;
                                //condition.m_stepExitTimeInMs = uint.Parse(tmp.GetValue());
                                timeBeforeAfter.Add(timedCondition);
                            }
                            else if (ShogiParameter.HasParam(optionsFoundInProcess, "MaxTime") && ShogiParameter.HasParam(optionsFoundInProcess, "MinTime"))
                            {
                                TimedConditionBetween timedCondition = new TimedConditionBetween();
                                if (ShogiParameter.HasParam(optionsFoundInProcess, "MinTime", out tmp))
                                    timedCondition.m_timeInMsMin = uint.Parse(tmp.GetValue());
                                if (ShogiParameter.HasParam(optionsFoundInProcess, "MaxTime", out tmp))
                                    timedCondition.m_timeInMsMax = uint.Parse(tmp.GetValue());

                                timedCondition.m_condition = conditionFound;
                                timedCondition.m_actionToTrigger = action;
                                timedCondition.m_triggerOnInRange = triggerOnRange;
                                timedCondition.m_triggerOnOutRange = triggerOnOutOfRange;
                                timedCondition.m_triggerOnReleaseInRange = triggerOnRelease;
                                if (typeAsString == "before")
                                    timedCondition.m_timeConditionType = TimedConditionBetween.TimeType.Before;
                                else if (typeAsString == "after")
                                    timedCondition.m_timeConditionType = TimedConditionBetween.TimeType.After;
                                else if (typeAsString == "notbetween")
                                    timedCondition.m_timeConditionType = TimedConditionBetween.TimeType.NotBetween;
                                else
                                    timedCondition.m_timeConditionType = TimedConditionBetween.TimeType.Between;

                                timeBetween.Add(timedCondition);


                            }
                        }



                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Exception:" + e.StackTrace);
                    }

                }
            }
        }
    }
}
