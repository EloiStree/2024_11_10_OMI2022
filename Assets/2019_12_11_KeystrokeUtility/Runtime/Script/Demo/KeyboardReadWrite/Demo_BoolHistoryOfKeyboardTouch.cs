using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_BoolHistoryOfKeyboardTouch : MonoBehaviour
{
    public KeyboardReadMono m_keyboard;
    [Header("Set Before Play")]
    [Range(1, 100)]
    public int m_maxSwitchCount = 30;
    [SerializeField]
    BoolHistory history;
    public KeyboardTouch m_observer = KeyboardTouch.T;
    [Header("State")]
    public bool m_state;
    public int m_countTrue;
    public int m_countFalse;
    [Header("Debug View")]
    public bool m_useDebugView;
    public float m_timeObserve = 3f;
    [Range(0.05f, 10)]
    public float m_dilatation = 4;
    [TextArea(0, 6)]
    public string m_description;
    [TextArea(0, 6)]
    public string m_numericDescription;
    public void Start()
    {
         history = m_keyboard.GetTouchHistory(m_observer);
        history.SetMaxSaveTo(m_maxSwitchCount);

    }
  
    void Update()
    {
        
        history.HasChangeRecenlty(m_timeObserve, out m_state, out m_countTrue , out m_countFalse);
        if (m_state)
            m_countTrue += 1;
        else 
            m_countFalse += 1;
        if (m_useDebugView) { 
            m_description = history.GetDescriptionNowToPast(m_timeObserve,m_dilatation, "-","_","","");
            m_numericDescription = history.GetNumericDescriptionNowToPast(m_timeObserve);
        }
    }
}
