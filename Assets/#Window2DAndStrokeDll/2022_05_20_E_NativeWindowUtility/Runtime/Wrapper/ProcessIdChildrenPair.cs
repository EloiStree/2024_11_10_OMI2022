using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProcessIdChildrenPair 
{
    public ProcessIdWrapper m_parentId;
    public ProcessIdWrapper m_childId;

    public ProcessIdChildrenPair(int id=0, int idChildren=0)
    {
        this.m_parentId = new ProcessIdWrapper(id);
        this.m_childId = new ProcessIdWrapper(idChildren);
    }

    
}
