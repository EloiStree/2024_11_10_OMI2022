using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ProcessesAccessInScene : Eloi.GenericSingletonOfMono<ProcessesAccessMono> { }


public class ProcessesAccessMono : MonoBehaviour
{

    public GroupOfProcessesInfo m_lastRefreshOfProcess = new GroupOfProcessesInfo();
    public GroupOfProcessesPairParentToChildren m_ProcessToChildrenPair = new GroupOfProcessesPairParentToChildren();
    public GroupOfProcessesParentToChildrens m_processToChildrens = new GroupOfProcessesParentToChildrens();

    public RelayFoundInfo m_onRefresh;
    [System.Serializable]
    public class RelayFoundInfo : UnityEvent<GroupOfProcessesInfo> { }

    public bool m_autoLoadAllAtWake;



    public void FetchProcessInfoBasedOnIndex(in string processIdName, in int processIndex, out bool found, out TargetParentProcessIntPtr pointer)
    {
        found = false;
        pointer = new TargetParentProcessIntPtr();
        m_lastRefreshOfProcess.GetWith(out List<WindowIntPtrUtility.ProcessInformation> processes);
        int count=0;
        foreach (var item in processes)
        {
            if (Eloi.E_StringUtility.AreEquals(item.m_processName, processIdName,true, true)) {
                if (processIndex == count) {
                    found = true;
                    pointer.SetAsInt(item.m_processId);
                    return;
                }
                count++;
            }
        }
    }

    public void GetParentOf(IntPtrWrapGet id, out bool found, out IntPtrWrapGet parentId)
    {
        for (int i = 0; i < m_ProcessToChildrenPair.m_processesAndChildrens.Count; i++)
        {
            if (m_ProcessToChildrenPair.m_processesAndChildrens[i]
                .m_childId.GetProcessId() ==
                id.GetAsInt())
            {
                found = true;
                parentId =IntPtrTemp.Int( m_ProcessToChildrenPair.m_processesAndChildrens[i].m_parentId.GetProcessId());
                return;
            }
        }
        found = false;
        parentId = null;
    }

    public void RefreshIfFirstTime()
    {
        if (!m_wasRefreshOnce)
            RefreshListOfProcesses();
    }

    void Awake()
    {
        if(m_autoLoadAllAtWake)
        RefreshListOfProcesses();
    }
     bool m_wasRefreshOnce;
    [ContextMenu("Refresh")]
    public void RefreshListOfProcesses()
    {
        m_wasRefreshOnce = true;
        WindowIntPtrUtility.FetchAllProcesses(out
            List<WindowIntPtrUtility.ProcessInformation> allProcess);
        m_lastRefreshOfProcess.SetWith(allProcess);

        m_ProcessToChildrenPair.Clear();
        m_processToChildrens.Clear();

        for (int i = 0; i < allProcess.Count; i++)
        {
            m_ProcessToChildrenPair.AddFromId(allProcess[i].m_processId);
            m_processToChildrens.AddFromId(allProcess[i].m_processId, allProcess[i].m_processName);
        }
        m_onRefresh.Invoke(m_lastRefreshOfProcess);
    }

    [ContextMenu("RefreshWithOnlyNotePad")]
    public void RefreshWithOnlyNotePad() => RefreshListOfProcessesWithName("notepad");
    [ContextMenu("RefreshWithOnlyPaint")]
    public void RefreshWithOnlyPaint() => RefreshListOfProcessesWithName("mspaint");
    [ContextMenu("RefreshWithOnlyWow")]
    public void RefreshWithOnlyWow() => RefreshListOfProcessesWithName("Wow");
    public void RefreshListOfProcessesWithName(string name)
    {
        FetchListOfProcessesBasedOnName(name, out m_processToChildrens, true);
    }

    public void FetchListOfProcessesBasedOnName(string processName,
        out GroupOfProcessesParentToChildrens found,
        bool reloadProcesses) {

        if (reloadProcesses)
        {
            FetchAndUpdateCurrentProcess();
        }

        found = new GroupOfProcessesParentToChildrens();
        m_lastRefreshOfProcess.GetWith(out List<WindowIntPtrUtility.ProcessInformation> info);
        for (int i = 0; i < info.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(processName, info[i].m_processName, true, true))
            {
                found.AddFromId(info[i].m_processId, info[i].m_processName);
            }
        }
    }

    private void FetchAndUpdateCurrentProcess()
    {
        WindowIntPtrUtility.FetchAllProcesses(out
       List<WindowIntPtrUtility.ProcessInformation> allProcess);
        m_lastRefreshOfProcess.SetWith(allProcess);
    }
}

[System.Serializable]
public class GroupOfProcessesInfo {
    [SerializeField]  List<WindowIntPtrUtility.ProcessInformation>
        m_listOfProcesses = new List<WindowIntPtrUtility.ProcessInformation>();

    public void SetWith(List<WindowIntPtrUtility.ProcessInformation> info) {
        m_listOfProcesses = info;
    }
    public void GetWith(out List<WindowIntPtrUtility.ProcessInformation> info) {
        info = m_listOfProcesses;
    }

}

[System.Serializable]
public class GroupOfProcessesPairParentToChildren
{
    public List<ProcessIdChildrenPair> m_processesAndChildrens = new List<ProcessIdChildrenPair>();

    public void Clear() { m_processesAndChildrens.Clear(); }
    public void AddProcess(ProcessIdChildrenPair process)
    {
        m_processesAndChildrens.Add(process);
    }
    public void AddFromId(int id)
    {

        IntPtrWrapGet[] ps = WindowIntPtrUtility.GetProcessIdChildrenWindows(IntPtrTemp.Int(  id));
        for (int i = 0; i < ps.Length; i++)
        {
            AddProcess(new ProcessIdChildrenPair(id, ps[i].GetAsInt()));
        }
    }
}
[System.Serializable]
public class GroupOfProcessesParentToChildrens
{
    public List<ProcessIdWithChildGroupInfo> m_processesAndChildrens = new List<ProcessIdWithChildGroupInfo>();

    public void Clear() { m_processesAndChildrens.Clear(); }
    public void AddProcess(ProcessIdWithChildGroupInfo process)
    {
        m_processesAndChildrens.Add(process);
    }
    public void AddFromId(int id, string debugName="")
    {

        IntPtrWrapGet[] ps = WindowIntPtrUtility.GetProcessIdChildrenWindows(IntPtrTemp.Int( id));
        foreach (var item in ps)
        {
        ProcessIdWithChildGroupInfo i = new ProcessIdWithChildGroupInfo(id, item.GetAsInt());
        i.SetDebugName(debugName);
        AddProcess(i);

        }
       
    }
}
