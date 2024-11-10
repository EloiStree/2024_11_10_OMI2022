using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowMonitorsWithAnchorDebugMono : MonoBehaviour
{
    public UI_WindowMonitorMonoFromAnchor[] m_monitorsUI;
    public WindowMonitorsInformation.MonitorInformation[] m_monitorsInfo;
    public RectTransform m_mousePosition;
    public int m_x;
    public int m_y;
    public void Update()
    {
        WindowMonitorsInformation.GetCursorPosition(out  m_x, out  m_y);
        m_mousePosition.anchoredPosition =new Vector2( m_x -(m_mousePosition.sizeDelta.x/2f) , -m_y+(m_mousePosition.sizeDelta.y / 2f));
    }
    public void RefreshUI()
    {
        WindowMonitorsInformation.Refresh();
        m_monitorsInfo = WindowMonitorsInformation.m_devices.ToArray();

        for (int i = 0; i < m_monitorsUI.Length; i++)
        {
            if (i < m_monitorsInfo.Length )
            {
                WindowMonitorsInformation.MonitorInformation m = m_monitorsInfo[i];
                if (m.m_display.StateFlags != 0) { 
                m_monitorsUI[i].SetDisplayNameId(m.m_display.DeviceName);
                m_monitorsUI[i].SetDimension(m.m_devMode.dmPositionX, m.m_devMode.dmPositionY
                    , m.m_devMode.dmPelsWidth, m.m_devMode.dmPelsHeight);
                m_monitorsUI[i].Display();
                }
                else m_monitorsUI[i].Hide();
            }
            else m_monitorsUI[i].Hide();
        }
    }
}
