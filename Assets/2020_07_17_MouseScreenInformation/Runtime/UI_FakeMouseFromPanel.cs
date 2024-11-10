using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FakeMouseFromPanel : MonoBehaviour
{

    public ScreenZoneInPourcentBean m_positionGiven;
    public RectTransform m_affected;

    
    void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (m_affected == null) return;
        ScreenPositionInPourcentBean pc = m_positionGiven.GetCenter();
        if (pc != null) { 
            m_affected.anchorMin = new Vector2(pc.GetLeftToRightValue(), pc.GetBotToTopValue());
            m_affected.anchorMax = new Vector2(pc.GetLeftToRightValue(), pc.GetBotToTopValue());
        }
    }

    public void SetPosition(ScreenZoneInPourcentBean positionGiven) {
        m_positionGiven = positionGiven;
        Refresh();
    }
    private void OnValidate()
    {
        if(!Application.isPlaying)
        Refresh();

    }
    private void Reset()
    {
        m_affected = GetComponent<RectTransform>();
    }
}
