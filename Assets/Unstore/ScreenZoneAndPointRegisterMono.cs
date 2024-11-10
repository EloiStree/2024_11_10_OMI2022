using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenZoneAndPointRegisterMono : MonoBehaviour
{

    public List<NamedScreenPourcentZone> m_screenZones = new List<NamedScreenPourcentZone>();
    public List<NamedScreenPourcentPosition> m_screenPoints = new List<NamedScreenPourcentPosition>();

    public void Clear()
    {
        m_screenPoints.Clear();
        m_screenZones.Clear();
    }

    public void Add(NamedScreenPourcentPosition position)
    {
        m_screenPoints.Add(position);
    }
    public void Add(NamedScreenPourcentZone zone) {
        m_screenZones.Add(zone);
    }

    public void Get(string name, out bool found, out NamedScreenPourcentZone zone)
    {
        found = false;
        zone = null;
        for (int i = 0; i < m_screenZones.Count; i++)
        {
            if (m_screenZones[i].GetName() == name) {
                zone = m_screenZones[i];
                found = true;
                return;
            }
        }
    }
    public void Get(string name, out bool found, out NamedScreenPourcentPosition position)
    {
        found = false;
        position = null;
        for (int i = 0; i < m_screenPoints.Count; i++)
        {
            if (m_screenPoints[i].GetName() == name) {
                position = m_screenPoints[i];
                found = true;
                return;
            }
        }
    }
}
