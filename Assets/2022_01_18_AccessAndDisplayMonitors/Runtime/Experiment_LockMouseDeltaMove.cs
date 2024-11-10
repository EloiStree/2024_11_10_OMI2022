using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WindowMonitorsMono;

public class Experiment_LockMouseDeltaMove : MonoBehaviour
{
    public WindowMonitorsMono m_windowInfo;

    public Vector2 delta;
    public MouseMoveSinceListReset m_deltaMouseMoveEvent;

    public PixelPosition_L2R_B2T m_current;
    public PixelPosition_L2R_B2T m_center;

    public MonitorsGroupInformation monitor;

    [System.Serializable]
    public class MouseMoveSinceListReset : UnityEvent<Vector2> { }

    

     void MoveMouseToCenterOfScreen() {

       
            m_windowInfo.GetMonitorInformation(out MonitorsGroupInformation info);
            info.GetFocusMonitor(out PercentMonitorInformatoRelative_L2RB2T monitor);
            monitor.GetCenterInGroup(out PercentPosition_L2R_B2T center );
            info.GetPixelFromPercent(in center, out m_center);
            m_windowInfo.GetNativeCursorPosition(in center, out PixelPosition_L2R_T2B position);
            m_windowInfo.MoveCursorTo(in position);
        
    }
 
    public void ResetCursorToCenterAndComputeDelta() {

        if (this.enabled)
        {
            m_windowInfo.RefreshMousePosition();
            m_windowInfo.GetMonitorInformation(out  monitor);
            monitor.GetCursorPosition(out m_current);

            MoveMouseToCenterOfScreen();

            delta.x = m_current.m_x_left2RightPx - m_center.m_x_left2RightPx;
            delta.y = m_current.m_y_bot2topPx - m_center.m_y_bot2topPx;
            m_deltaMouseMoveEvent.Invoke(delta);
        }
    }
 
}
