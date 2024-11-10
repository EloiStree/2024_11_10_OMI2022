using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TDD_PushFakeMessageToSpecificProcessId : MonoBehaviour
{

    public int m_id;
    public User32PostMessageKeyEnum m_toPush = User32PostMessageKeyEnum.VK_SPACE;

    public void SetTargetProcessId(int id) {
        m_id = id;
    }

    public int m_timeBetweenChangeMs=100;
    [ContextMenu("SetSpace")]
    public void SetSpace() => m_toPush = User32PostMessageKeyEnum.VK_SPACE;
    [ContextMenu("SetO")]
    public void SetO() => m_toPush = User32PostMessageKeyEnum.VK_0;

    public bool m_usePost;
    [ContextMenu("Push")]
    public void Push()
    {
        WindowIntPtrUtility.SetForegroundWindow((IntPtr)m_id);
        Thread.Sleep(m_timeBetweenChangeMs);
        //WindowIntPtrUtility.ShowWindow((IntPtr)m_id, 3);
        IntPtrWrapGet p = IntPtrTemp.Int(m_id);
        SendKeyMessageToWindows.SendKeyDown(m_toPush, p, m_usePost);
        Thread.Sleep(m_timeBetweenChangeMs);
        SendKeyMessageToWindows.SendKeyUp(m_toPush, p, m_usePost);
    }


    [ContextMenu("Foreground")]
    public void Hightlight()
    {
        
    }
}
