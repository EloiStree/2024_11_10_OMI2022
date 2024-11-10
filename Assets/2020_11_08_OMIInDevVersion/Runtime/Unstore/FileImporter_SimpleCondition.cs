using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImporter_SimpleCondition : MonoBehaviour
{
    public SimpleConditionMono m_conditionManager;

    public void ClearRegister()
    {
        m_conditionManager.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                List<AndBooleanChangeToAction<string>> found;
                LoadAndBooleanToJOMI(File.ReadAllText(filePath[i]), out found);
                for (int y = 0; y < found.Count; y++)
                {

                    m_conditionManager.Add(found[y]);
                }
               
            }
        }
    }

    private void LoadAndBooleanToJOMI(string textToLoad, out List <AndBooleanChangeToAction<string>> found)
    {
        found = new List<AndBooleanChangeToAction<string>>();
        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        string tmpCondition;
        string tmpJomiShortcut;
        string processedOption;
        ClassicBoolState tmpState;
        AndBooleanChangeToAction<string> tmpListener;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length > 0 && lines[i][0] == '#')
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() > 1)
            {
                tmpListener = new AndBooleanChangeToAction<string>();
                tmpCondition = tokens.GetValue(0).Trim();
                tmpJomiShortcut = tokens.GetValue(1).Trim();
                if (TextToBoolStateMachineParser.IsClassicParse(tmpCondition, out tmpState))
                {
                    tmpListener.m_andBooleanState = tmpState;
                    tmpListener.m_informationToTrigger = tmpJomiShortcut;
                    found.Add(tmpListener);
                }

                if (tokens.GetCount() > 2)
                {
                    processedOption = tokens.GetValue(2);
                    while (processedOption.IndexOf("  ") > -1)
                        processedOption = processedOption.Replace("  ", " ");

                    List<ShogiParameter> optionsFoundInProcess = ShogiParameter.FindParametersInString(processedOption);
                    
                    ShogiParameter tmpFound;
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "LoopOnTrue", out tmpFound))
                    {
                        try
                        {
                            float time = int.Parse(tmpFound.GetValue()) / 1000f;
                            tmpListener.m_looperTrue = new ActionLooper<ActionAsString>(tmpListener.m_informationToTrigger, time, false,
                                        null, new ActionAsString(tmpListener.m_informationToTrigger));
                            tmpListener.m_useLoopTrue = true;
                        }
                        catch (Exception) { }
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "LoopOnFalse"))
                    {
                        try
                        {
                            float time = int.Parse(tmpFound.GetValue()) / 1000f;
                            tmpListener.m_looperFalse = new ActionLooper<ActionAsString>(tmpListener.m_informationToTrigger, time, false,
                                null, new ActionAsString(tmpListener.m_informationToTrigger));
                            tmpListener.m_useLoopFalse = true;
                        }
                        catch (Exception) { }
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnTrue"))
                    {
                        tmpListener.m_listenToTrueChange = true;
                    }
                    if (ShogiParameter.HasParam(optionsFoundInProcess, "TriggerOnFalse"))
                    {

                        tmpListener.m_listenToFalseChange = true;
                    }


                }

            }
        }

    }



}
[System.Serializable]
public class AndBooleanChangeToAction<T>
{
    public T m_informationToTrigger;
    public ClassicBoolState m_andBooleanState = new ClassicBoolState(0.1f);
    public BooleanSwitchListener m_eventListener = new BooleanSwitchListener();
    public ActionLooper<ActionAsString> m_looperTrue = null;
    public ActionLooper<ActionAsString> m_looperFalse = null;
    public bool m_listenToTrueChange;
    public bool m_listenToFalseChange;
    public bool m_useLoopTrue;
    public bool m_useLoopFalse;


}


[System.Serializable]
public class ActionLooper<T>
{
    public DoWithParameters m_toDo;
    public T m_defaultAction;
    public float m_timeBetweenSend = 50;
    public bool m_isActive = false;
    public float m_timeBeforeNextSend = 0;

    public ActionLooper(string shortcutToSend, float loopDelayBetweenSend, bool isActiveByDefault, DoWithParameters toDo, T defaultAction)
    {
        m_toDo = toDo;
        m_timeBetweenSend = loopDelayBetweenSend;
        m_timeBeforeNextSend = loopDelayBetweenSend;
        m_defaultAction = defaultAction;
    }
    public void SetTheAction(DoWithParameters action) {
        m_toDo = action;    
    }

    public delegate void DoWithParameters(T parameter);

    public void SetAsActive(bool isPlaying) { if (isPlaying) Play(); else Stop(); }
    public void Play() { m_isActive = true; }
    public void Stop() { m_isActive = false; }
    public bool IsActive() { return m_isActive; }

    public float GetDelayBetweenSend() { return m_timeBetweenSend; }
    public float GetTimeLeft() { return m_timeBeforeNextSend; }
    public void ResetTCountDown() { m_timeBeforeNextSend = m_timeBetweenSend; }
    public void RemoveTimeToCountdown(float timeInSeconds) { m_timeBeforeNextSend -= timeInSeconds; }
    public bool IsReadyToExecute() { return GetTimeLeft() <= 0; }
    public void DoAction(T parameter) { m_toDo(parameter); }
    public void DoDefaultAction() { m_toDo(m_defaultAction); }

}