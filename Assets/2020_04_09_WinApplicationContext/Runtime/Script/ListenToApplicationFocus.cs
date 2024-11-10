using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenToApplicationFocus : MonoBehaviour
{
    [SerializeField] float m_timeBetweenCheck=0.5f;
    [SerializeField] OnFocusChange m_onFocusChange;
    public WindowFocusChangeEvent m_onFocusChangeEvent;
    [SerializeField] string m_focusWindowName;
    private string m_previousFocusName;

    [TextArea(2, 20)]
    [SerializeField] List<string> m_availableWindows;


    [System.Serializable]
    public class OnFocusChange : UnityEvent<string, string> { }

    public IEnumerator Start()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenCheck);
            CheckFocus();
        }
    }
    private void OnValidate()
    {
        CheckFocus();
    }

    public void CheckFocus()
    {
        OpenWindowGetter.GetFocusWindow(out m_focusWindowName, out m_availableWindows);
        if (m_previousFocusName != m_focusWindowName) {
            m_onFocusChange.Invoke(m_previousFocusName, m_focusWindowName);
            if(m_onFocusChangeEvent!=null)
            m_onFocusChangeEvent(m_previousFocusName, m_focusWindowName);
            m_previousFocusName = m_focusWindowName;
        }
    }
    public string GetPreviousFocusWindowName() { return m_previousFocusName; }
    public string GetCurrentFocusWindowName() { return m_focusWindowName; }

    public delegate void WindowFocusChangeEvent(string previousWinName, string currentWinName);

    public void AddListener(WindowFocusChangeEvent listener)
    {
        m_onFocusChangeEvent += listener;

    }
    public void RemoveListener(WindowFocusChangeEvent listener)
    {

        m_onFocusChangeEvent -= listener;
    }


}
