using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToScreenPositionCollection : MonoBehaviour
{
    public UI_ToScreenPosition [] m_screenZonesUI;
    public ScreenZonesList m_screenZonesInPourcent;
    public bool m_loadAtStart=true;
    public ScreenZoneEvent m_foundZone;
    public ScreenZoneListEvent m_onLoadedZones;
    public void Translate() {

        m_screenZonesInPourcent.ClearAll();
        for (int i = 0; i < m_screenZonesUI.Length; i++)
        {
           ScreenZoneFullRecord zone = m_screenZonesUI[i].GetZoneInformationFromCanvas();
           m_screenZonesInPourcent.Add(zone);
            m_foundZone.Invoke(zone);
        }
        m_onLoadedZones.Invoke(m_screenZonesInPourcent);
    }

    private void Start()
    {
        if(m_loadAtStart)
            Translate();
    }
}
