using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUtilityMono : MonoBehaviour
{

    public ScreenZoneAndPointRegisterMono m_screenRegister;
    public MicrosoftWindowsManager m_windowManager;



    public void MoveWindowAt(string name, string screenlocation)
    {
        bool found;
        NamedScreenPourcentZone zone;
        m_screenRegister.Get(screenlocation.ToLower(), out found, out zone);
        float pt1lr, pt1dt, pt2lr, pt2ldt;
        pt1lr = zone.m_zone.GetBotLeft().GetLeftToRightValue();
        pt1dt = zone.m_zone.GetBotLeft().GetBotToTopValue();
        pt2lr = zone.m_zone.GetTopRight().GetLeftToRightValue();
        pt2ldt = zone.m_zone.GetTopRight().GetBotToTopValue();

        m_windowManager.MoveWindow(name, pt1lr, pt1dt, pt2lr, pt2ldt);
    }

    public void SetFocusOn(string name)
    {
        m_windowManager.SetFocusOn(name);
    }

    internal void SaveCurrentWindowPositionAs(string name)
    {
        float pt1lr, pt1dt, pt2lr, pt2ldt;
        m_windowManager.GetCurrentWindowPosition(out pt1lr, out pt1dt, out pt2lr, out pt2ldt);
       // m_screenRegister.Add()
    }

    public void SaveCurrentWindowAsFocusWith(string name, bool permanenceSave)
    {
        m_windowManager.SaveCurrentFocusAs(name, permanenceSave);
    }

    internal void ChangeTitle(string name, string title)
    {
        m_windowManager.ChangeWindowTitle(name, title);
    }

    internal void ChangeTitle(string title)
    {
        m_windowManager.ChangeCurrentWindowTitle( title);
    }
}
