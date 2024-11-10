using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouseInformationWindow32DLL : MonoBehaviour
{
    public MouseInformationAbstract m_toAffect;
    public Vector2 m_point;
    public PointInter m_tmp;

    void Update()
    {
        GetCursorPosition(out m_tmp);
        m_point.x = m_tmp.X;
        m_point.y = m_tmp.Y;
        m_toAffect.SetMainScreenResolution( Screen.currentResolution.width, Screen.currentResolution.height);
        m_toAffect.SetPosition_D2T_L2R(Screen.currentResolution.height - m_tmp.Y, m_tmp.X);


    }



    [StructLayout(LayoutKind.Sequential)]
    public struct PointInter
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out PointInter lpPoint);

    public static void GetCursorPosition(out PointInter infoOfMouse )
    {
        GetCursorPos(out infoOfMouse);

    }

}
