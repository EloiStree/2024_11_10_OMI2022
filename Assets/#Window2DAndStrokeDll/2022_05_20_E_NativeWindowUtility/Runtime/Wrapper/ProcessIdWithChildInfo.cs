using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessIdWithChildInfo
{
    public ProcessIdParent m_parent;
    public ProcessIdChildrenPair m_childrens;

    public ProcessIdWithChildInfo(ProcessIdParent parent, ProcessIdChildrenPair childrens)
    {
        m_parent = parent;
        m_childrens = childrens;
    }
}


[System.Serializable]
public class ProcessIdWithChildGroupInfo
{
    public string m_nameToDebug;
    public ProcessIdParent m_parent= new ProcessIdParent();
    public ProcessIdChildrenPair [] m_childrens = new ProcessIdChildrenPair[0];

    public void SetDebugName(string name) {
        m_nameToDebug = name;
    }
    public ProcessIdWithChildGroupInfo(ProcessIdParent parent, params ProcessIdChildrenPair[] childrens)
    {
        m_parent = parent;
        m_childrens = childrens;
    }

    internal void GetListOfPointers(out List<IntPtrWrapGet> pointers, bool withParent=true)
    {
        List<IntPtrWrapGet> result = m_childrens.Select(k => IntPtrTemp.Int(k.m_childId.GetProcessId())).ToList();
        if(withParent)
        result.Add(IntPtrTemp.Int(m_parent.GetProcessId()));
        pointers = result;
    }

    public ProcessIdWithChildGroupInfo(int parent, params int[] childrens)
    {
        SetChildren(parent,childrens);
    }
    public ProcessIdWithChildGroupInfo(int parent, params IntPtr[] childrens)
    {
        SetChildren(parent, childrens.Select(k => (int) k).ToArray());
    }
    public void SetChildren(int parent, int[] childrenId)
    {
        m_parent = new ProcessIdParent(parent);
        m_childrens = new ProcessIdChildrenPair[childrenId.Length];
        if (childrenId == null)
            return;
        for (int i = 0; i < childrenId.Length; i++)
        {
            m_childrens[i] = new ProcessIdChildrenPair();
            m_childrens[i].m_parentId = m_parent;
            m_childrens[i].m_childId = new  ProcessIdWrapper(childrenId[i]);
        }
    }


}

