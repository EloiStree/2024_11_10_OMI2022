using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProcessIdWrapper 
{
    [SerializeField] int m_processId;

    public ProcessIdWrapper(int id)
    {
        this.m_processId = id;
    }
    public ProcessIdWrapper()
    {
        this.m_processId = 0;
    }

    public void GetProcessId(out int processId) {
        processId = m_processId;
    }
    public int GetProcessId() { 
        return m_processId; 
    }
    public void SetId(int processId) {
        m_processId = processId;
    }

}
