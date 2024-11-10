using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MoveCursorToDisplayPosition : MonoBehaviour
{


    public int m_timeToPush = 3000;
    public Action_Real_SetCursor_OverDisplayID_PX_LRTD m_moveIdPx;
    public Action_Real_SetCursor_OverDisplayName_PX_LRTD m_moveStringPx;
    public Action_Real_SetCursor_OverDisplayID_PCT_LRTD m_moveIdPct;
    public Action_Real_SetCursor_OverDisplayName_PCT_LRTD m_moveStringPct;

    [ContextMenu("Refresh_IDPX")]
    void Refresh_IDPX()
    {
        User32ActionAbstractCatchToExecute.PushActionIn(m_timeToPush, m_moveIdPx);
    }
    [ContextMenu("Refresh_StringIDPX")]
    void Refresh_StringIDPX()
    {
        User32ActionAbstractCatchToExecute.PushActionIn(m_timeToPush, m_moveStringPx);
    }
    [ContextMenu("Refresh_IDPCT")]
    void Refresh_IDPCT()
    {
        User32ActionAbstractCatchToExecute.PushActionIn(m_timeToPush, m_moveIdPct);
    }
    [ContextMenu("Refresh_StringIDPCT")]
    void Refresh_StringIDPCT()
    {
        User32ActionAbstractCatchToExecute.PushActionIn(m_timeToPush, m_moveStringPct);
    }

}
