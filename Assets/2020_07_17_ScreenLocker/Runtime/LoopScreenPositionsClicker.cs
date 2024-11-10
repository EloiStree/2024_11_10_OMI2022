using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoopScreenPositionsClicker : MonoBehaviour
{
    public ScreenPixelPositionsList m_positions = new ScreenPixelPositionsList();
    public ScreenZonesList m_zones = new ScreenZonesList();
    public ScreenPositionEvent m_requestClickOn = new ScreenPositionEvent();
    public ScreenZoneEvent m_requestClickOnZone = new ScreenZoneEvent();

    public bool m_usePositionsList;
    public bool m_useZoneList;

    public float m_timeBetweenClick = 0.1f;

    public int m_zoneIndex;
    public int m_positionIndex;
    public IEnumerator Start()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenClick);
            if (m_positions != null && m_positions.GetCount() > 0)
            {
                if (m_positionIndex >= m_positions.GetCount())
                    m_positionIndex = 0;
                if (m_usePositionsList)
                {

                    ScreenPositionFullRecord position = m_positions.Get(m_positionIndex);
                    m_requestClickOn.Invoke(position);
                }
                m_positionIndex++;
            }
            if (m_zones != null && m_zones.GetCount() > 0)
             {
                if (m_zoneIndex >= m_zones.GetCount())
                    m_zoneIndex = 0;
                
                if (m_useZoneList)
                {

                    ScreenZoneFullRecord zone = m_zones.Get(m_zoneIndex);
                    m_requestClickOnZone.Invoke(zone);
                }
                m_zoneIndex++;
            }

        }
    }


    public void SetCollectionOfPositions(ScreenPixelPositionsList list)
    {
        m_positions = list;
    }
    public void SetCollectionOfPositions(ScreenPositionsRegister register)
        {if (register == null)
                return;
        SetCollectionOfPositions(register.m_screenPositions);
        SetCollectoinOfZones(register.m_screenZones);
    }
    public void SetCollectoinOfZones(ScreenZonesList list)
    {
        m_zones = list;
    }
    public void AddZone(ScreenZoneFullRecord zone) { m_zones.Add(zone); }
    public void AddPosition(ScreenPositionAsPixel position) { m_positions.Add(position); }

}
