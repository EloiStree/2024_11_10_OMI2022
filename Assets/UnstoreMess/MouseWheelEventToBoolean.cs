using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWheelEventToBoolean : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;

    public string m_mouseWheelLeft="wheelLeft";
    public string m_mouseWheelRight="wheelRight";
    public string m_mouseWheelUp="wheelUp";
    public string m_mouseWheelDown="wheelDown";
    public MouseWheelEvent m_lastReceived;
    public  void PushMouseWheelEvent( MouseWheelEvent mouseWheel)
    {
        m_lastReceived = mouseWheel;
        if (mouseWheel.m_direction == MouseWheelEvent.Direction.Left
            && m_mouseWheelLeft.Trim().Length > 0)
        {
            m_register.Set(m_mouseWheelLeft, true, true);
            m_register.Set(m_mouseWheelLeft, false, false);
        }
       else  if (mouseWheel.m_direction == MouseWheelEvent.Direction.Right
           && m_mouseWheelRight.Trim().Length > 0)
        {
            m_register.Set(m_mouseWheelRight, true, true);
            m_register.Set(m_mouseWheelRight, false, false);
        }
        else if (mouseWheel.m_direction == MouseWheelEvent.Direction.Up
           && m_mouseWheelUp.Trim().Length > 0)
        {
            m_register.Set(m_mouseWheelUp, true, true);
            m_register.Set(m_mouseWheelUp, false, false);
        }
        else if (mouseWheel.m_direction == MouseWheelEvent.Direction.Down
           && m_mouseWheelDown.Trim().Length > 0)
        {
            m_register.Set(m_mouseWheelDown, true, true);
            m_register.Set(m_mouseWheelDown, false, false);
        }
    }

}
