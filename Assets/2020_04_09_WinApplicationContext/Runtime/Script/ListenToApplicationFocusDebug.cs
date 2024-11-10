using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListenToApplicationFocusDebug : MonoBehaviour
{
    [Header("Inspector")]
    public string m_currentFocusFiltered;
    public string m_currentFocus;
    public string m_previousFocusFiltered;
    public string m_previousFocus;
    [Header("UI")]
    public InputField m_tCurrentFocusFiltered;
    public InputField m_tCurrentFocus;
    public InputField m_tPreviousFocusFiltered;
    public InputField m_tPreviousFocus;
    [Header("History")]
    public string m_history;
    public InputField m_tHistory;

    public void DisplayFocusSwitchInfo(FocusSwitchInfo value)
    {
        m_history = string.Format("{0}|  {1}\n", value.currentFocusApp, value.currentFocusTitle) + m_history;

        m_history = m_history.Substring(0, m_history.Length> 2000?2000: m_history.Length);
        if (m_tHistory)
            m_tHistory.text = m_history;
        DisplayAppFound(value.previousFocusApp, value.currentFocusApp);
        DisplayTitleValue(value.previousFocusTitle,value.currentFocusTitle);
    }

    public void DisplayAppFound(string previousFocus, string currentFocus)
    {
        m_currentFocusFiltered = currentFocus;
        if (m_tCurrentFocusFiltered)
            m_tCurrentFocusFiltered.text = m_currentFocusFiltered;
        m_previousFocusFiltered = previousFocus;
        if (m_tPreviousFocusFiltered)
            m_tPreviousFocusFiltered.text = m_previousFocusFiltered;
    }
    public void DisplayTitleValue(string previousFocus, string currentFocus)
    {
        m_currentFocus = currentFocus;
        if (m_tCurrentFocus)
            m_tCurrentFocus.text = m_currentFocus;
        m_previousFocus = previousFocus;
        if (m_tPreviousFocus)
            m_tPreviousFocus.text = m_previousFocus;
    }
}
