using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_ClickInSpecificPointsOfSpecificWindows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        RefreshProcessName();
        
    }

    public List<string> processesName;

    public string m_processToFind;
    public int m_processIndexToFind;
    public bool m_processFound;
    public IntPtr m_handleIndexFound;
    public int m_handleIndexDebug;

    private void RefreshProcessName()
    {
        BeanUtility_AccessProcessIdBasedOn.GetAllProcessesName(
            out processesName);
        BeanUtility_AccessProcessIdBasedOn.GetScreenPositionFromWindow(
            m_processToFind,
            m_processIndexToFind,
            out m_processFound,
            out m_handleIndexFound);
        m_handleIndexDebug =(int) m_handleIndexFound;
    }

    private void OnValidate()
    {
        RefreshProcessName();
    }
}
