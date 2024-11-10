using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_ScreenPositionToJomiClick : MonoBehaviour
{


    public bool m_isActive;
    public UI_ServerDropdownJavaOMI m_selection;
    public bool m_useClick;
    public ScreenPourcentPositionBeanEvent m_lastSend;

    public void MoveToRandomInZone(ScreenZoneFullRecord zone)
    {

        MoveToSquareRandomInZone(zone.m_zoneInPourcent);
    }
    public void MoveToSquareRandomInZone(ScreenZoneInPourcentBean zone)
    {
        MoveTo(zone.GetSquareRandom());
    }
    public void MoveToElpitiqueRandomInZone(ScreenZoneFullRecord zone)
    {
        MoveToElpitiqueRandomInZone(zone.m_zoneInPourcent);
    }
    public void MoveToElpitiqueRandomInZone(ScreenZoneInPourcentBean zone)
    {
        MoveTo(zone.GetEliptiqueRandom());
    }

    public void MoveTo(ScreenPositionInPourcentBean pourcent)
    {
        MoveTo(pourcent.GetLeftToRightValue(), pourcent.GetBotToTopValue());
    }
    public void MoveTo(ScreenPositionFullRecord position)
    {
        MoveTo(position.GetAsPourcent());
    } 
    public void MoveTo(float leftToRightPourcent, float botToTopPourcent)
    {
        if(!m_isActive)
            return;
        m_lastSend.Invoke(new ScreenPositionInPourcentBean(leftToRightPourcent, botToTopPourcent));
        foreach (var item in m_selection.GetJavaOMISelected())
        {
            if (m_useClick)
                item.MouseClick(JavaOpenMacroInput.JavaMouseButton.BUTTON1_DOWN_MASK);
            item.MouseMoveInPourcent(leftToRightPourcent, 1f - botToTopPourcent);
        }
    }

    public void SwitchListenState()
    {
        m_isActive = !m_isActive;
    }
    public void SetActiveState(bool state)
    {
        m_isActive = state;
    }
}
