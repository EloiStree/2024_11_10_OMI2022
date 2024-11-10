using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win32ThreadIdChangeObserver : MonoBehaviour
{
    public int m_parentProcessId;
    public int m_unitProcessId;
    public int m_childrenId;
    public int m_previousId;
    public Eloi.PrimitiveUnityEvent_Int m_newProcessId;
    public Eloi.PrimitiveUnityEvent_Int m_newProcessIdActiveChildren;

    public bool m_useTheUnityOne;
    public List<int> m_unityIds = new List<int>();
    
    private void Awake()
    {

        SetCurrentAsUnityIds();
    }

    private void SetCurrentAsUnityIds()
    {
        WindowIntPtrUtility.GetCurrentChildrenFocusProcessId(out IntPtrWrapGet id);
        WindowIntPtrUtility.GetParentAndChildOf(id, out IntPtrWrapGet parentProcessId, out IntPtrWrapGet childrenId);
        
        m_unityIds.Clear();
        m_unityIds.Add(parentProcessId.GetAsInt());
        IntPtrWrapGet[] pts = WindowIntPtrUtility.GetProcessIdChildrenWindows(parentProcessId);
        for (int i = 0; i < pts.Length; i++)
        {
            m_unityIds.Add(pts[i].GetAsInt());
        }
    }

    void Update()
    {

        WindowIntPtrUtility.GetCurrentChildrenFocusProcessId(out IntPtrWrapGet pid);
        if (pid.GetAsInt() != m_previousId) {
            if (!m_useTheUnityOne && IsUnityThreadId(pid.GetAsInt()))
            {
            }
            else
            {
                WindowIntPtrUtility.GetParentAndChildOf(pid, out IntPtrWrapGet parentProcessId, out IntPtrWrapGet childrenId);
                m_parentProcessId = parentProcessId.GetAsInt();
                m_childrenId = childrenId.GetAsInt();
                m_newProcessId.Invoke(m_parentProcessId);
                m_newProcessIdActiveChildren.Invoke(m_childrenId);
                m_previousId = m_parentProcessId;
            }
        }
        
    }

    private bool IsUnityThreadId(int pid)
    {
        for (int i = 0; i < m_unityIds.Count; i++)
        {
            if (pid == m_unityIds[i])
                return true;
        }
        return false;
    }
}
