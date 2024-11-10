using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMonitorsMono : MonoBehaviour
{
    public enum MonitorDirection
    {
        Left2Right,
        Right2Left,
        Bot2Top,
        Top2Bot,
        FromBotLeft,
        FromTopLeft,
        FromBotRight,
        FromTopRight
           , None
    }



    public GivenRawWindowInforamtion m_nativeInfoGiven;
    [System.Serializable]
    public class GivenRawWindowInforamtion{


        public RawMousePositionInformation m_mouseFromZero = new RawMousePositionInformation();
        public WindowMonitorsInformation.MonitorInformation[] m_nativeInformation = new WindowMonitorsInformation.MonitorInformation[0];
    }
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    private static extern int SetCursorPos(int x, int y);
    public void MoveCursorTo(in PixelPosition_L2R_T2B position)
    {
        SetCursorPos(position.m_x_left2Right, position.m_y_top2Bot);
    }

    public void GetNativeCursorPosition(in PercentPosition_L2R_B2T point, out PixelPosition_L2R_T2B position)
    {
        GetMainScreenRawInfoRelativeToGlobal(out RawMonitorInformation rawInfo);
        rawInfo.GetCornerTopLeft(out PixelPosition_L2R_T2B corner);
       // Debug.Log(corner.m_x_left2Right + "::" + corner.m_y_top2Bot);
        int x =  -corner.m_x_left2Right + ((int)( point.m_x_left2RightPct * m_resultInfo.m_totalWidthPx));
        int y =  -corner.m_y_top2Bot + ( (int) ( (1.0-point.m_y_bot2topPct) * m_resultInfo.m_totalHeightPx ) ) ;


        position = new PixelPosition_L2R_T2B(x,y);


        
    }

    private void GetMainScreenRawInfoRelativeToGlobal(out RawMonitorInformation rawInfo)
    {
        rawInfo = null;
        for (int i = 0; i < m_processInfo.m_monitorInformationRelativeToGlobal.Count; i++)
        {
            if (m_processInfo.m_monitorInformationRelativeToGlobal[i].m_isMainScreen) { 
                rawInfo = m_processInfo.m_monitorInformationRelativeToGlobal[i];
                return;
            }
        }
    }

    [Header("Raw")]
    public ProcessInfo m_processInfo;
    [System.Serializable]
    public class ProcessInfo {
        public RawMousePositionInformation m_mouseFromZero = new RawMousePositionInformation();
        public List<RawMonitorInformation> m_monitorInformation = new List<RawMonitorInformation>();

        public RawMousePositionInformation m_mouseFromGlobalZero = new RawMousePositionInformation();
        public List<RawMonitorInformation> m_monitorInformationRelativeToGlobal = new List<RawMonitorInformation>();
    }

   

    [Header("Abstract")]
    public MonitorsGroupInformation m_resultInfo;

    public void GetDisplayNames(out string[] displayNames)
    {
        displayNames = m_processInfo.m_monitorInformation.Select(k => k.m_deviceName).ToArray();
    }

    public void GetMonitorInformation(out MonitorsGroupInformation info)
    {
        info = m_resultInfo;
    }

    public void Get(string displayName, out LRBTPercentRectPosition monitor)
    {
        GetRelativePosition(displayName, out RawMonitorInformation info);
        monitor = new LRBTPercentRectPosition();
        monitor.m_widthPercent = (info.m_width) / m_width;
        monitor.m_heightPercent =  (info.m_height) / m_height;

        monitor.m_xLeft2RightPercent = (info.m_x_left2Right ) / (double)m_width;
        monitor.m_yBot2TopPercent = 1.0 -( (info.m_y_top2Bot ) / (double)m_height);

    }

    private void GetRelativePosition(string displayName, out RawMonitorInformation monitor)
    {
        for (int i = 0; i < m_processInfo.m_monitorInformationRelativeToGlobal.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(m_processInfo.m_monitorInformationRelativeToGlobal[i].m_deviceName, displayName))
            {
                monitor = m_processInfo.m_monitorInformationRelativeToGlobal[i];
                return;
            }
        }
        monitor = null;
    }

    public void Get(string displayName, out RawMonitorInformation monitor)
    {
        for (int i = 0; i < m_processInfo.m_monitorInformation.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(m_processInfo.m_monitorInformation[i].m_deviceName, displayName))
            {
                monitor = m_processInfo.m_monitorInformation[i];
                return;
            }
        }
        monitor = null;
    }

    public void Get(MonitorDirection direction, int index, out bool found, out string name)
    {
        Get(direction, index, out found, out RawMonitorInformation raw);
        if (found)
        {
            name = raw.m_deviceName;
        }
        else name = "";

    }
        public void Get(MonitorDirection direction, int index, out bool found, out RawMonitorInformation name)
    {
        GetPointsOfMonitor(out List<PointToMonitorName> points);
        List<string> idsFound = new List<string>();
        List<string> orderFound = new List<string>();


        if (direction == MonitorDirection.Left2Right)
            points = points.OrderBy(k => k.m_position.m_x_left2Right).ToList();
        else if (direction == MonitorDirection.Right2Left)
            points = points.OrderByDescending(k => k.m_position.m_x_left2Right).ToList();
        else if (direction == MonitorDirection.Top2Bot)
            points = points.OrderBy(k => k.m_position.m_y_top2Bot).ToList();
        else if (direction == MonitorDirection.Bot2Top)
            points = points.OrderByDescending(k => k.m_position.m_y_top2Bot).ToList();
        else if (direction == MonitorDirection.FromBotLeft)
            points = points.OrderBy(k =>GetDistance( k.m_position, m_downLeft)).ToList();
        else if (direction == MonitorDirection.FromBotRight)
            points = points.OrderByDescending(k => GetDistance(k.m_position, m_downRight)).ToList();
        else if (direction == MonitorDirection.FromTopLeft)
            points = points.OrderBy(k => GetDistance(k.m_position, m_topLeft)).ToList();
        else if (direction == MonitorDirection.FromTopRight)
            points = points.OrderByDescending(k => GetDistance(k.m_position, m_topRight)).ToList();



        for (int i = 0; i < points.Count; i++)
        {
            if (!idsFound.Contains(points[i].m_idName)) {
                idsFound.Add(points[i].m_idName);
                orderFound.Add(points[i].m_idName);
            }
        }

        if (index < orderFound.Count)
        {
            found = true;
            Get(orderFound[index], out name);
        }
        else {
            found = false;
            name = null;
        }




    }

    private float GetDistance(PixelPosition_L2R_T2B a, PixelPosition_L2R_T2B b)
    {
        return Vector2.Distance(new Vector2(a.m_x_left2Right, a.m_y_top2Bot), new Vector2(b.m_x_left2Right, b.m_y_top2Bot));
    }

    private void GetPointsOfMonitor( out List<PointToMonitorName> points)
    {
        points= new List<PointToMonitorName>();
        foreach (var item in m_processInfo.m_monitorInformation)
        {
            AppendNamePointOfMonitor(item, ref  points);

        }
    }

    private void AppendNamePointOfMonitor(RawMonitorInformation monitor, ref List<PointToMonitorName> points)
    {
        monitor.GetCorners(out PixelPosition_L2R_T2B [] corners);
      
        foreach (var item in corners)
        {
            points.Add(new PointToMonitorName(monitor.m_deviceName, item));

        }
        monitor.GetCenter(out PixelPosition_L2R_T2B center);
        points.Add(new PointToMonitorName(monitor.m_deviceName, center));

    }

    [System.Serializable]
    public class PointToMonitorName {
        public string m_idName;
        public PixelPosition_L2R_T2B m_position;

        public PointToMonitorName(string idName, PixelPosition_L2R_T2B position)
        {
            m_idName = idName;
            m_position = position;
        }
    }

    public void GetMousePositionAsPourcent(out PourcentPosition_L2R_B2T mouse)
    {
        

         mouse = new PourcentPosition_L2R_B2T();
        if (m_width != 0 && m_height != 0) { 
            mouse.m_left2RightPercent =(m_nativeInfoGiven. m_mouseFromZero.m_x_left2Right - m_topLeft.m_x_left2Right) / (double)m_width;
            mouse.m_bot2TopPercent =(  1.0-(m_nativeInfoGiven.m_mouseFromZero.m_y_top2Bot - m_topLeft.m_y_top2Bot) / (double)m_height);
        }
    }

    [Header("Corner")]
    public PixelPosition_L2R_T2B m_topLeft;
    public PixelPosition_L2R_T2B m_topRight;
    public PixelPosition_L2R_T2B m_downLeft;
    public PixelPosition_L2R_T2B m_downRight;


    [Header("Information")]
    public int m_width;
    public int m_height;




    [System.Serializable]
    public class PixelPosition_L2R_T2B
    {
        public int m_x_left2Right;
        public int m_y_top2Bot;

        public PixelPosition_L2R_T2B(int x_left2Right, int y_top2Bot)
        {
            m_x_left2Right = x_left2Right;
            m_y_top2Bot = y_top2Bot;
        }
    }

    [System.Serializable]
    public class RawMousePositionInformation {
        public int m_x_left2Right;
        public int m_y_top2Bot;
    }

    [System.Serializable]
    public class RawMonitorInformation {
        public string m_deviceName="";
        public bool m_isMainScreen;
        public int m_x_left2Right;
        public int m_y_top2Bot;
        public int m_width;
        public int m_height;

        internal void SetDimension(int x_left2Right, int y_top2Bot, int width, int height)
        {
            m_x_left2Right = x_left2Right;
            m_y_top2Bot = y_top2Bot;
            m_width = width;
            m_height = height;
        }

        public void GetCornerTopLeft(out PixelPosition_L2R_T2B topLeft) { topLeft = new PixelPosition_L2R_T2B(m_x_left2Right, m_y_top2Bot); }
        public void GetCornerTopRight(out PixelPosition_L2R_T2B topRight) { topRight = new PixelPosition_L2R_T2B(m_x_left2Right+m_width, m_y_top2Bot); }
        public void GetCornerDownLeft(out PixelPosition_L2R_T2B downLeft) { downLeft = new PixelPosition_L2R_T2B(m_x_left2Right, m_y_top2Bot+m_height); }
        public void GetCornerDownRight(out PixelPosition_L2R_T2B downRight) { downRight = new PixelPosition_L2R_T2B(m_x_left2Right + m_width, m_y_top2Bot + m_height); }

        public void GetCorners(out PixelPosition_L2R_T2B [] corners) { 
            corners = new PixelPosition_L2R_T2B[4];
            GetCornerTopLeft(out corners[0]);
            GetCornerTopRight(out corners[1]);
            GetCornerDownLeft(out corners[2]);
            GetCornerDownRight(out corners[3]);
        }

        internal void GetCenter(out PixelPosition_L2R_T2B center)
        {
            GetCornerTopLeft(out PixelPosition_L2R_T2B topLeft);
            GetCornerDownRight(out PixelPosition_L2R_T2B downRight);
            center = new PixelPosition_L2R_T2B(
                (topLeft.m_x_left2Right + downRight.m_x_left2Right) / 2,
                (topLeft.m_y_top2Bot + downRight.m_y_top2Bot) / 2);
        }
    }

   

    public void Refresh() {
        //REFRESH NATIVE INFO
        WindowMonitorsInformation.Refresh();

        // SET NATIVE TO ABSTRACT
        m_nativeInfoGiven.m_nativeInformation = WindowMonitorsInformation.m_devices.ToArray();
        m_processInfo.m_monitorInformation.Clear();



        for (int i = 0; i < m_nativeInfoGiven.m_nativeInformation.Length; i++)
        {

            WindowMonitorsInformation.MonitorInformation m = m_nativeInfoGiven.m_nativeInformation[i];
                if (m.m_display.StateFlags != 0)
                {
                    m_processInfo.m_monitorInformation.Add(new RawMonitorInformation());
                    m_processInfo.m_monitorInformation[i].m_deviceName = (m.m_display.DeviceName);
                    m_processInfo.m_monitorInformation[i].SetDimension(m.m_devMode.dmPositionX, m.m_devMode.dmPositionY
                            , m.m_devMode.dmPelsWidth, m.m_devMode.dmPelsHeight);
                    if (m.m_devMode.dmPositionX == 0 && m.m_devMode.dmPositionY == 0) {
                        m_processInfo.m_monitorInformation[i].m_isMainScreen = true;
                    }
                }
           
        }
   
      
        ComputeCorner();

        // REFRESH GLOBAL INFO
        m_height = m_downRight.m_y_top2Bot - m_topLeft.m_y_top2Bot;
        m_width = m_downRight.m_x_left2Right - m_topLeft.m_x_left2Right;

        m_processInfo.m_monitorInformationRelativeToGlobal.Clear();
        m_resultInfo.m_monitorsAsPixel = new PixelMonitorInformatoRelative_L2RB2T[m_processInfo.m_monitorInformation.Count];
        m_resultInfo.m_monitorsAsPercent = new PercentMonitorInformatoRelative_L2RB2T[m_processInfo.m_monitorInformation.Count];
        m_resultInfo.m_totalWidthPx = m_width;
        m_resultInfo.m_totalHeightPx = m_height;

        for (int i = 0; i < m_processInfo.m_monitorInformation.Count; i++)
        {

            RawMonitorInformation m = m_processInfo.m_monitorInformation[i];
            RawMonitorInformation mr = new RawMonitorInformation();
            mr.m_deviceName = m.m_deviceName;
            mr.SetDimension(m.m_x_left2Right - m_topLeft.m_x_left2Right, m.m_y_top2Bot - m_topLeft.m_y_top2Bot, m.m_width, m.m_height);
            mr.m_isMainScreen = m.m_isMainScreen;
            m_processInfo.m_monitorInformationRelativeToGlobal.Add( mr);

            //////////////////
            m_resultInfo.m_monitorsAsPixel[i] = new PixelMonitorInformatoRelative_L2RB2T();
            m_resultInfo.m_monitorsAsPixel[i].m_deviceName = mr.m_deviceName;
            m_resultInfo.m_monitorsAsPixel[i].m_widthPx = mr.m_width;
            m_resultInfo.m_monitorsAsPixel[i].m_heightPx = mr.m_height;
            m_resultInfo.m_monitorsAsPixel[i].m_x_left2RightPx = mr.m_x_left2Right;
            m_resultInfo.m_monitorsAsPixel[i].m_y_bot2topPx = m_resultInfo.m_totalHeightPx - mr.m_y_top2Bot - mr.m_height;
            m_processInfo.m_monitorInformation[i].m_isMainScreen = mr.m_isMainScreen;

            m_resultInfo.m_monitorsAsPercent[i] = new PercentMonitorInformatoRelative_L2RB2T();
            m_resultInfo.m_monitorsAsPercent[i].m_deviceName = mr.m_deviceName;
            m_resultInfo.m_monitorsAsPercent[i].m_widthPct = mr.m_width / (double) m_resultInfo.m_totalWidthPx;
            m_resultInfo.m_monitorsAsPercent[i].m_heightPct = mr.m_height / (double) m_resultInfo.m_totalHeightPx;
            m_resultInfo.m_monitorsAsPercent[i].m_x_left2RightPct = mr.m_x_left2Right / (double) m_resultInfo.m_totalWidthPx;
            m_resultInfo.m_monitorsAsPercent[i].m_y_bot2topPct = (m_resultInfo.m_monitorsAsPixel[i].m_y_bot2topPx) / (double) m_resultInfo.m_totalHeightPx;
            m_resultInfo.m_monitorsAsPercent[i].m_isMainScreen = mr.m_isMainScreen;

        }


        m_resultInfo.m_cursorAsPx.m_x_left2RightPx = m_processInfo.m_mouseFromGlobalZero.m_x_left2Right;
        m_resultInfo.m_cursorAsPx.m_y_bot2topPx = m_resultInfo.m_totalHeightPx - m_processInfo.m_mouseFromGlobalZero.m_y_top2Bot;

        m_resultInfo.m_cursorAsPercent.m_x_left2RightPct = m_resultInfo.m_cursorAsPx.m_x_left2RightPx / (float)m_resultInfo.m_totalWidthPx;
        m_resultInfo.m_cursorAsPercent.m_y_bot2topPct = m_resultInfo.m_cursorAsPx.m_y_bot2topPx / (float)m_resultInfo.m_totalHeightPx;


        RefreshMousePosition();

      
    }

    private void ComputeCorner()
    {
        int minX = 100000; int maxX= -1000000;
        int minY = 100000; int maxY= -100000;
        List<PixelPosition_L2R_T2B> points = new List<PixelPosition_L2R_T2B>();


        foreach (var item in m_processInfo.m_monitorInformation)
        {
            item.GetCorners(out PixelPosition_L2R_T2B[] corners);
            points.AddRange(corners);
        }
        foreach (var item in points)
        {
            if (item.m_x_left2Right < minX)
                minX = item.m_x_left2Right;
            if (item.m_x_left2Right > maxX)
                maxX = item.m_x_left2Right;
            if (item.m_y_top2Bot < minY)
                minY = item.m_y_top2Bot;
            if (item.m_y_top2Bot > maxY)
                maxY = item.m_y_top2Bot;
        }


        m_topLeft.m_x_left2Right = minX;
        m_topRight.m_x_left2Right = maxX;
        m_downLeft.m_x_left2Right = minX;
        m_downRight.m_x_left2Right = maxX;
        m_topLeft.m_y_top2Bot = minY;
        m_topRight.m_y_top2Bot = minY;
        m_downLeft.m_y_top2Bot = maxY;
        m_downRight.m_y_top2Bot = maxY;
    }

    public void RefreshMousePosition() {
        WindowMonitorsInformation.GetCursorPosition(out m_nativeInfoGiven.m_mouseFromZero.m_x_left2Right, out m_nativeInfoGiven.m_mouseFromZero.m_y_top2Bot);
      
        m_processInfo.m_mouseFromZero = new RawMousePositionInformation()
        {
            m_x_left2Right = m_nativeInfoGiven.m_mouseFromZero.m_x_left2Right,
            m_y_top2Bot = m_nativeInfoGiven.m_mouseFromZero.m_y_top2Bot
        };
        m_processInfo.m_mouseFromGlobalZero.m_x_left2Right = m_processInfo.m_mouseFromZero.m_x_left2Right - m_topLeft.m_x_left2Right;
        m_processInfo.m_mouseFromGlobalZero.m_y_top2Bot = m_processInfo.m_mouseFromZero.m_y_top2Bot - m_topLeft.m_y_top2Bot;

        m_resultInfo.m_cursorAsPx.m_x_left2RightPx = m_processInfo.m_mouseFromGlobalZero.m_x_left2Right;
        m_resultInfo.m_cursorAsPx.m_y_bot2topPx = m_resultInfo.m_totalHeightPx - m_processInfo.m_mouseFromGlobalZero.m_y_top2Bot;
       
        m_resultInfo.m_cursorAsPercent.m_x_left2RightPct = m_resultInfo.m_cursorAsPx.m_x_left2RightPx  / (float)m_resultInfo.m_totalWidthPx;
        m_resultInfo.m_cursorAsPercent.m_y_bot2topPct = m_resultInfo.m_cursorAsPx.m_y_bot2topPx / (float)m_resultInfo.m_totalHeightPx;

    }

    [System.Serializable]
    public class PourcentPosition_L2R_B2T
    {
        public double m_left2RightPercent;
        public double m_bot2TopPercent;
    }

    [System.Serializable]
    public class LRBTPercentRectPosition
    {
        public double m_xLeft2RightPercent;
        public double m_yBot2TopPercent;
        public double m_widthPercent;
        public double m_heightPercent;
    }
}
