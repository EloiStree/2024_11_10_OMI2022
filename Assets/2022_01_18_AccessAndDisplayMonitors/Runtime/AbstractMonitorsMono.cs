using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMonitorsMono : MonoBehaviour
{

    public MonitorsGroupInformation m_monitors;

}


[System.Serializable]
public class MonitorsGroupInformation
{
    public int m_totalWidthPx;
    public int m_totalHeightPx;
    public PixelPosition_L2R_B2T m_cursorAsPx;
    public PercentPosition_L2R_B2T m_cursorAsPercent;
    public PixelMonitorInformatoRelative_L2RB2T[] m_monitorsAsPixel;
    public PercentMonitorInformatoRelative_L2RB2T[] m_monitorsAsPercent;

    public void GetFocusMonitor(out PercentMonitorInformatoRelative_L2RB2T monitor)
    {
        monitor = null;
        for (int i = 0; i < m_monitorsAsPercent.Length; i++)
        {
            if (m_monitorsAsPercent[i].ContainPoint(in m_cursorAsPercent)) {
                monitor = m_monitorsAsPercent[i];
                return;
            }

        }

    }

    public void GetPixelFromPercent(in PercentPosition_L2R_B2T percentCenter, out PixelPosition_L2R_B2T pixelCenter)
    {
        pixelCenter = 
            new PixelPosition_L2R_B2T(
                (int)(percentCenter.m_x_left2RightPct * m_totalWidthPx),
                (int)(percentCenter.m_y_bot2topPct * m_totalHeightPx)
            );

    }

    public void GetCursorPosition(out PercentPosition_L2R_B2T cursor)
    {
        cursor = m_cursorAsPercent;

    }
    public void GetCursorPosition(out PixelPosition_L2R_B2T cursor)
    {
        cursor = m_cursorAsPx;

    }

    public void GetCenterOf(in PercentMonitorInformatoRelative_L2RB2T monitor, out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitor.GetCenterInGroup(out monitorFocusCenterPosition);
    }
}

[System.Serializable]
public class PixelMonitorInformatoRelative_L2RB2T
{
    public string m_deviceName="";
    public bool m_isMainScreen;
    public int m_x_left2RightPx;
    public int m_y_bot2topPx;
    public int m_widthPx;
    public int m_heightPx;
}
[System.Serializable]
public class PercentMonitorInformatoRelative_L2RB2T
{
    public string m_deviceName="";
    public bool m_isMainScreen;
    public double m_x_left2RightPct;
    public double m_y_bot2topPct;
    public double m_widthPct;
    public double m_heightPct;

    public bool ContainPoint( in PercentPosition_L2R_B2T point)
    {
        return 
            point.m_x_left2RightPct>= m_x_left2RightPct && point.m_x_left2RightPct<= m_x_left2RightPct+m_widthPct
            &&
            point.m_y_bot2topPct >= m_y_bot2topPct && point.m_y_bot2topPct <= m_y_bot2topPct + m_heightPct;

    }

    public void GetCenterInGroup(out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitorFocusCenterPosition = new PercentPosition_L2R_B2T()
        {
            m_x_left2RightPct = m_x_left2RightPct + m_widthPct / 2.0,
            m_y_bot2topPct = m_y_bot2topPct + m_heightPct / 2.0
        };
    }
    public void GetBotLeftCorner(out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitorFocusCenterPosition = new PercentPosition_L2R_B2T()
        {
            m_x_left2RightPct = m_x_left2RightPct ,
            m_y_bot2topPct = m_y_bot2topPct 
        };
    }
    public void GetBotRightCorner(out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitorFocusCenterPosition = new PercentPosition_L2R_B2T()
        {
            m_x_left2RightPct = m_x_left2RightPct + m_widthPct / 2.0,
            m_y_bot2topPct = m_y_bot2topPct 
        };
    }
    public void GetTopLeftCorner(out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitorFocusCenterPosition = new PercentPosition_L2R_B2T()
        {
            m_x_left2RightPct = m_x_left2RightPct ,
            m_y_bot2topPct = m_y_bot2topPct + m_heightPct / 2.0
        };
    }
    public void GetTopRightCorner(out PercentPosition_L2R_B2T monitorFocusCenterPosition)
    {
        monitorFocusCenterPosition = new PercentPosition_L2R_B2T()
        {
            m_x_left2RightPct = m_x_left2RightPct + m_widthPct / 2.0,
            m_y_bot2topPct = m_y_bot2topPct + m_heightPct / 2.0
        };
    }

  
}

[System.Serializable]
public class PixelPosition_L2R_B2T
{
    public int m_x_left2RightPx;
    public int m_y_bot2topPx;
    public PixelPosition_L2R_B2T()
    {
        m_x_left2RightPx = 0;
        m_y_bot2topPx = 0;
    }
    public PixelPosition_L2R_B2T(int x_left2RightPx, int y_bot2topPx)
    {
        m_x_left2RightPx = x_left2RightPx;
        m_y_bot2topPx = y_bot2topPx;
    }
}

[System.Serializable]
public class PercentPosition_L2R_B2T
{
    public double m_x_left2RightPct;
    public double m_y_bot2topPct;
}


