using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenPositionsRegisterMono : MonoBehaviour
{
    public ScreenPositionsRegister m_dataBase = new ScreenPositionsRegister();


    public ScreenPositionsRegisterEvent m_onChanged;


    public void SetWith(ScreenPositionsRegister newRegister)
    {
        m_dataBase = newRegister;
    }

    public ScreenZoneFullRecord LookForZone(string name)
    {
        ScreenZoneFullRecord [] tmp;
        m_dataBase.m_screenZones.GetDuplication(out tmp);
        tmp = tmp.Where(k => k.m_givenNamed.Trim().ToLower() == name.Trim().ToLower()).ToArray();
        if (tmp.Length > 0)
            return tmp[0];
        return null;
    }

    public void Add(ScreenPositionsRegister newRegister)
    {
        Add(newRegister.m_screenPositions);
        Add(newRegister.m_screenZones);
    }
    public void NotifyChange() {
        m_onChanged.Invoke(m_dataBase);
    }

    public void ClearAll() {
        m_dataBase.ClearAll();
    }
    public void Add(ScreenPixelPositionsList position)
    {

        ScreenPositionAsPixel[] values;
        position.GetDuplication(out values);
        m_dataBase.m_screenPositions.AddAll(values);
    }
    public void Add(ScreenZonesList zone)
    {
        ScreenZoneFullRecord[] values;
        zone.GetDuplication(out values);
        m_dataBase.m_screenZones.AddAll(values);
    }
    public void Add(IEnumerable<ScreenPositionAsPixel> position)
    {
        m_dataBase.m_screenPositions.AddAll(position);
    }
    public void Add(IEnumerable< ScreenZoneFullRecord> zone)
    {
        m_dataBase.m_screenZones.AddAll(zone);
    }
    public void Add(ScreenPositionAsPixel position)
    {
        m_dataBase.m_screenPositions.Add(position);
    }
    public void Add(ScreenPositionAsPourcent position)
    {
        m_dataBase.m_screenPositions.Add(new ScreenPositionAsPixel()
        {
            m_name = position.m_name,
            m_mainScreenDimention = position.m_mainScreenDimention
            ,
            m_pixel = position.GetAsPixel()
        }); 
    }
    public void Add(ScreenZoneFullRecord zone)
    {
        m_dataBase.m_screenZones.Add(zone);
    }
    public void Add(NamedScreenPourcentPosition position)
    {
        m_dataBase.m_positions.Add(position);
    }
    public void Add(NamedScreenPourcentZone zone)
    {
        m_dataBase.m_zones.Add(zone);
    }
    public void Add(ScreenPositionFullRecord position)
    {
        if (position is ScreenPositionAsPixel)
            Add((ScreenPositionAsPixel)position);
        if (position is ScreenPositionAsPourcent)
            Add((ScreenPositionAsPourcent)position);
    }
}

[System.Serializable]
public class ScreenPositionsRegister {

    public ScreenZonesList m_screenZones= new ScreenZonesList();
    public ScreenPixelPositionsList m_screenPositions = new ScreenPixelPositionsList();
    public NamedZonesList m_zones= new NamedZonesList();
    public NamedPositionsList m_positions = new NamedPositionsList();



    public void ClearAll()
    {
        m_screenZones.ClearAll();
        m_screenPositions.ClearAll();
        m_zones.ClearAll();
        m_positions.ClearAll();
    }
}

[System.Serializable]
public class NamedZonesList : ListCollection<NamedScreenPourcentZone> { }

[System.Serializable] 
public class NamedPositionsList : ListCollection<NamedScreenPourcentPosition> { }
