using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFromProcessesTargetIntPtrMono : AbstractContaintIntPtrWrapMono
{
    public ProcessesAccessMono m_processesAccess;
    public string m_processIdName="Wow";
    public int m_processIndex= 0;

    [Header("Debug")]
    public TargetParentProcessIntPtr m_processIdParent;
    public TargetChildrenProcessIntPtr m_processIdChildren;

    [ContextMenu("RefreshSourceListOfProcess")]
    public void RefreshSourceListOfProcess() {
        m_processesAccess.RefreshListOfProcesses();
    }

    [ContextMenu("Refresh")]
    public void Refresh() {
        m_processesAccess.FetchProcessInfoBasedOnIndex(in m_processIdName,in m_processIndex, out bool found, out TargetParentProcessIntPtr pointer);
        if (!found)
        {
            m_processIdParent.SetToZero();
            m_processIdChildren.SetToZero();
        }
        else {
            m_processIdParent.SetAsInt(pointer.GetAsInt());
            WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(pointer, out bool foundChild, out IntPtrWrapGet target);
            if (!foundChild)
                m_processIdChildren.SetToZero();
            else m_processIdChildren.SetAsIntPtr(target.GetAsIntPtr());
        }
    }


    public override void GetIntPtrInChildren(out IntPtrWrapGet info)
    {
        info = m_processIdChildren;
    }
}
