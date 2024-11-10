using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowMonitorsWithPercentDebugMono : MonoBehaviour
{

    public WindowMonitorsMono m_monitorsInfo;
    public AspectRatioFitter m_screenRatio;

    public RectTransform m_mousePosition;
    public UI_WindowMonitorWithPercentMono[] m_monitors;

    public void RefreshMonitorUI() {

        m_monitorsInfo.GetMonitorInformation(out MonitorsGroupInformation info);

        m_screenRatio.aspectRatio = (float)info.m_totalWidthPx / (float)info.m_totalHeightPx;
        int lenght = info.m_monitorsAsPercent.Length;
        for (int i = 0; i < lenght; i++)
        {
            PercentMonitorInformatoRelative_L2RB2T m = info.m_monitorsAsPercent[i];
            m_monitors[i].SetDisplayNameId(m.m_deviceName);
            m_monitors[i].SetDimension(
                (float)m.m_x_left2RightPct,
                (float)m.m_y_bot2topPct,
                (float)m.m_widthPct,
                (float)m.m_heightPct);
        }
        
        for (int i = 0; i < m_monitors.Length; i++)
        {
            if (i < lenght)
                m_monitors[i].Display();
            else m_monitors[i].Hide();
        }
    }
    public void RefreshCursorUI() {

        m_monitorsInfo.GetMonitorInformation(out MonitorsGroupInformation info);

        m_mousePosition.anchorMin = new Vector2((float)info.m_cursorAsPercent.m_x_left2RightPct, 
            (float)info.m_cursorAsPercent.m_y_bot2topPct);
        m_mousePosition.anchorMax = new Vector2((float)info.m_cursorAsPercent.m_x_left2RightPct,
            (float)info.m_cursorAsPercent.m_y_bot2topPct);
       // m_mousePosition.anchoredPosition = new Vector2((m_mouseRadius / 2f), (m_mouseRadius / 2f));
        m_mousePosition.anchoredPosition = new Vector2(0,0);
        m_mousePosition.sizeDelta = new Vector2(m_mouseRadius , m_mouseRadius );


    }

    public float m_mouseRadius = 20;
}
