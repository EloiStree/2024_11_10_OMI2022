using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MouseRegister : MonoBehaviour
{

    public MouseInformationAbstract m_defaultMouseTracker;
    public Dictionary<string, ScreenPositionInPourcentBean> m_recordedPosition = new Dictionary<string, ScreenPositionInPourcentBean>();


    public void RefreshPosition(string name) {

        float lr, bt;
        m_defaultMouseTracker.GetPourcent(out lr, out bt);
        if (Contain(name)) {
            SetPosition(name, lr, bt);
        }
        SetPosition(name, new ScreenPositionInPourcentBean(lr, bt));
    }

    private void SetPosition(string name, float leftRight, float botTop)
    {
        if (Contain(name))
        {
            m_recordedPosition[name].SetBotToTopValue(botTop);
            m_recordedPosition[name].SetLeftToRightValue(leftRight);
        }
        else SetPosition(name, new ScreenPositionInPourcentBean(leftRight, botTop));
    }

    private bool Contain(string name)
    {
        return m_recordedPosition.ContainsKey(name);
    }
 

    public void SetPosition(string name, ScreenPositionInPourcentBean pourcentPosition)
    {

        if (!m_recordedPosition.ContainsKey(name))
        {
            m_recordedPosition.Add(name, pourcentPosition);
        }
        else m_recordedPosition[name] = pourcentPosition;
    }


    public void  GetPosition(string name, out bool hasPosition, out ScreenPositionInPourcentBean position) {

        hasPosition = m_recordedPosition.ContainsKey(name);
        if (hasPosition)
            position = m_recordedPosition[name];
        else position = null;
    }
}
