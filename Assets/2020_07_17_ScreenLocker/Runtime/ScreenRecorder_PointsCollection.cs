using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ScreenRecorder_PointsCollection : MonoBehaviour
{
    public MouseInformationAbstract m_mouseInformation;
    public ScreenPixelPositionsList m_record = new ScreenPixelPositionsList();

    public ScreenPositionEvent m_onRecordPositionChange;
    public ScreenZoneEvent m_onRecordedZoneChange;


    public void ClearRecord()
    {
        m_record.ClearAll();

    }
    public ScreenPixelPositionsList GetCurrentCollectionRecord() {
        return m_record;
    }
    int m_lastRecordId=0;
    public void RecordScreenKeyAnonymously() {
        ScreenPositionFullRecord tmp;
        RecordScreenPositionWithName("" + m_lastRecordId++, out tmp);
    }
    public void RecordScreenPositionWithName(string name, out ScreenPositionFullRecord recored)
    {
        recored = GetScreenPositionNamed( name);
        m_onRecordPositionChange.Invoke(recored);

    }

    private ScreenPositionFullRecord GetScreenPositionNamed( string name)
    {
        ScreenPositionFullRecord recored;
        if (name == null)
            name = "";
        ScreenPositionAsPixel key = new ScreenPositionAsPixel();
        key.m_mainScreenDimention.SetDimension(m_mouseInformation.GetScreenWidth(), m_mouseInformation.GetScreenHeight());

        int btPx, lrPx;
        m_mouseInformation.GetMousePositionOnScreen(out btPx, out lrPx);
        key.m_pixel.SetBotToTopValue(btPx);
        key.m_pixel.SetLeftToRightValue(lrPx);
        key.m_name = name;
        m_record.Add(key);
        recored = key;
        return recored;
    }

    public bool m_isRecordingZone;
    public ScreenPositionFullRecord m_startZoneRecord;
    public ScreenPositionFullRecord m_endZoneRecord;
    public void StartRecordingZone()
    {
        m_isRecordingZone = true;
        m_startZoneRecord = GetScreenPositionNamed("");
    }
    public void StopRecordingZone( )
    {
        StopRecordingZone("");
    }
    public void StopRecordingZone(string saveName = "")
    {

        if (m_isRecordingZone) { 
            m_isRecordingZone = false;
            m_endZoneRecord = GetScreenPositionNamed("");
            m_onRecordedZoneChange.Invoke(new ScreenZoneFullRecord()
            {
                m_givenNamed = saveName,
                m_mainScreenDimention = m_startZoneRecord.m_mainScreenDimention,
                m_zoneInPourcent = new ScreenZoneInPourcentBean(
                    m_startZoneRecord.GetAsPourcent(),
                    m_endZoneRecord.GetAsPourcent())
            }) ; 
        }
    }
}
