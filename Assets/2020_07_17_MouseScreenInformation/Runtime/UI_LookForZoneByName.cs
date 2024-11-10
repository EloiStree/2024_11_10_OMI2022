using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LookForZoneByName : MonoBehaviour
{
    public UI_FakeMouseFromPanel m_fakeCursorUI;
    public MoveFakeCursorWithScreenPosition m_fakeCursorCamera;
    public ScreenPositionsRegisterMono m_register;

    public void LookFor(string name) {

      ScreenZoneFullRecord zone =  m_register.LookForZone(name);
        if(zone != null && m_fakeCursorUI != null )
             m_fakeCursorUI.SetPosition(zone.m_zoneInPourcent);
        if (zone != null &&  m_fakeCursorCamera != null)
            m_fakeCursorCamera.MoveTo(ScreenConvertion.GetCenterOf(zone));
    }
}

public class ScreenConvertion
{
    public static ScreenPositionFullRecord GetCenterOf(ScreenZoneFullRecord zoneInPourcent)
    {
        ScreenPositionAsPourcent n = new ScreenPositionAsPourcent();
        n.m_mainScreenDimention = zoneInPourcent.m_mainScreenDimention;
        n.m_name = zoneInPourcent.m_givenNamed;
        ScreenPositionInPourcentBean pct = zoneInPourcent.m_zoneInPourcent.GetCenter();
        n.m_pourcent.SetBotToTopValue(pct.GetBotToTopValue());
        n.m_pourcent.SetLeftToRightValue(pct.GetLeftToRightValue());

        return n;
    }
}