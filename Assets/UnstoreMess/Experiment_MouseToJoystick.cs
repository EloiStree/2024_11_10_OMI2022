using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_MouseToJoystick : MonoBehaviour
{
    public UDPThreadSender m_xomi;
    public string m_boolCondition = "usemouse2xomi";
    public bool m_booleanCondition;
    public float m_radiusPourcentHorizontal = 0.2f;
    public float m_radiusPourcentVertical = 0.2f;
    public float m_radiusPourcentHorizontalDeathZone = 0.05f;
    public float m_radiusPourcentVerticalDeathZone = 0.05f;
    public float m_radiusPourcentHorizontalMax = 0.05f;
    public float m_radiusPourcentVerticalMax = 0.05f;
    public float m_radiusPixel = 150;

    public Vector2 m_joystickState;
    public WindowMonitorsMono m_windowMonitors;

    public MouseInformationAbstract m_mouse;

    public float m_verticalAmplificator=1f;
    public float m_horizontalAmplificator=0.5f;


    //public Vector3

    public void PushState()
    {

        //m_windowMonitors.GetSelectedMonitor(out MonitorsAliasRegister  monitor);
        //m_windowMonitors.GetCursorPositionL2RB2T(in monitor, out Vector2 value);


        //        Eloi.E_UnityRandomUtility.GetRandomDirectionNormalized(out Vector3 d);
        //m_joystickState.x = d.x;
        //m_joystickState.y = d.y;
        //m_xomi.AddMessageToSendToAll(new MessageToAll("🎮r:" + m_joystickState.x+ ":"+ m_joystickState.y));
        //m_xomi.SendAllAsSoonAsPossible();
    }

    public int m_previousBT, m_previousLR;
    public void Update()
    {
       // BaseOnMouseMove();
        BaseOnCenterScreen();
    }
    public MonitorsGroupInformation m_monitors;
    public PercentMonitorInformatoRelative_L2RB2T m_monitor;
    public PercentPosition_L2R_B2T m_cursor;
    public PercentPosition_L2R_B2T m_monitorFocusCenterPosition;
    public Vector2 m_previousPosition;
    private void BaseOnCenterScreen()
    {
       m_windowMonitors.RefreshMousePosition();
       m_windowMonitors.GetMonitorInformation(out m_monitors);
       m_monitors.GetFocusMonitor(out  m_monitor);
       m_monitors.GetCursorPosition(out  m_cursor);
       m_monitors.GetCenterOf(in m_monitor, out m_monitorFocusCenterPosition);
        Vector2 center = new Vector2((float)m_monitorFocusCenterPosition.m_x_left2RightPct, (float)m_monitorFocusCenterPosition.m_y_bot2topPct); ;
        Vector2 pt = new Vector2((float)m_cursor.m_x_left2RightPct, (float)m_cursor.m_y_bot2topPct);
        m_joystickState = (pt - center);
        if (Mathf.Abs(m_joystickState.x) < m_radiusPourcentHorizontalDeathZone)
        {
            m_joystickState.x = 0;
        }
        else
        {
            m_joystickState.x = Mathf.Sign(m_joystickState.x) * ((Mathf.Abs(m_joystickState.x )- m_radiusPourcentHorizontalDeathZone) /
            (m_radiusPourcentHorizontal - m_radiusPourcentHorizontalDeathZone));
            m_joystickState.x = Mathf.Clamp(m_joystickState.x *m_horizontalAmplificator, 
                -m_radiusPourcentHorizontalMax, m_radiusPourcentHorizontalMax);
        }
        if (Mathf.Abs(m_joystickState.y) < m_radiusPourcentVerticalDeathZone)
        {
            m_joystickState.y = 0;
        }
        else { 
        m_joystickState.y = Mathf.Sign(m_joystickState.y) *( (Mathf.Abs(m_joystickState.y) - m_radiusPourcentVerticalDeathZone) /
           (m_radiusPourcentVertical - m_radiusPourcentVerticalDeathZone));

            m_joystickState.y = Mathf.Clamp(m_joystickState.y * m_verticalAmplificator, -m_radiusPourcentVerticalMax, m_radiusPourcentVerticalMax);

            
        }




        Vector2 delta = pt- m_previousPosition;
        bool hasMoved = (delta.magnitude > 0.0001f);
        if (hasMoved) { 
           
                m_xomi.AddMessageToSendToAll(new MessageToAll("🎮r:" + m_joystickState.x  + ":" + m_joystickState.y ));
                m_xomi.SendAllAsSoonAsPossible();
            
        }

        m_previousPosition = pt;
    }

    private void BaseOnMouseMove()
    {
        m_mouse.GetMousePositionOnScreen(out int bt, out int lr);

        int deltaBT = bt - m_previousBT;
        int deltaLR = lr - m_previousLR;
        bool haschange = deltaBT != 0 || deltaLR != 0;
        m_joystickState.x += deltaLR;
        m_joystickState.y += deltaBT;
        if (m_joystickState.x > m_radiusPixel)
            m_joystickState.x = m_radiusPixel;
        if (m_joystickState.y > m_radiusPixel)
            m_joystickState.y = m_radiusPixel;
        if (m_joystickState.x < -m_radiusPixel)
            m_joystickState.x = -m_radiusPixel;
        if (m_joystickState.y < -m_radiusPixel)
            m_joystickState.y = -m_radiusPixel;

        if (haschange)
        {
            if (m_joystickState.magnitude > 0.1f)
            {
                m_xomi.AddMessageToSendToAll(new MessageToAll("🎮r:" + (m_joystickState.x / m_radiusPixel) + ":" + (m_joystickState.y / m_radiusPixel)));
                m_xomi.SendAllAsSoonAsPossible();
            }
            else
            {
                m_xomi.AddMessageToSendToAll(new MessageToAll("🎮r:0:0"));
                m_xomi.SendAllAsSoonAsPossible();
            }
        }

        m_previousBT = bt;
        m_previousLR = lr;
    }
}
