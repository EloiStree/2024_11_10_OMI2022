using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScreenSaveAndRecorder : MonoBehaviour
{


    public List<NamedPctPosition> m_screenRecorder;
    public UI_ServerDropdownJavaOMI m_target;
    public PointInter m_lastRecord;
    public PointInter m_lastApply;

    [System.Serializable]
    public class NamedPctPosition {
        public string m_name;
        public PointInter m_position;
    }

    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PointInter
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out PointInter lpPoint);



    public static void GetCursorPosition(out PointInter infoOfMouse)
    {
       
        GetCursorPos(out infoOfMouse);

    }
    public void Save(string name)
    {
        Debug.Log("Save");
        GetCursorPos(out PointInter point);
        m_lastRecord = point;
        SetScreenPositionFor(in name, in point);
    }

    private void GetScreenPositionFor(in string name,out bool found, out PointInter position)
    {
        for (int i = 0; i < m_screenRecorder.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(in name, in m_screenRecorder[i].m_name, true, true))
            {
                position = m_screenRecorder[i].m_position;
                found = true;
                return;
            }
        }
        position = new PointInter();
        found = false;
    }

    private void SetScreenPositionFor(in string name, in PointInter found)
    {
        for (int i = 0; i < m_screenRecorder.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(in name, in m_screenRecorder[i].m_name, true, true))
            {
                 m_screenRecorder[i].m_position = found;
                return;
            }
        }
        m_screenRecorder.Add(new NamedPctPosition() { m_name = name.ToString(), m_position=found }); 
    }

    public void Recover(string name)

    {
        GetScreenPositionFor(in name, out bool found, out PointInter sp);
        if (found)
        {
            Debug.Log("Recover");
            foreach (var item in m_target.GetJavaOMISelected())
            {
                item.MouseMove(sp.X, sp.Y);
                Debug.Log(" Applied ");
            }
            m_lastApply = sp;
        }
    }
}
