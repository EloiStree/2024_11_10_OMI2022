using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class ScreenZoneToBooleans : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public MouseInformationAbstract m_mouseInformation;

  

    //public List<CircleScreenPosition> m_circleZoneOnScreen= new List<CircleScreenPosition>();
    public List<NamedScreenPourcentZone> m_squareZone = new List<NamedScreenPourcentZone>();

    [Header("Debug")]
    public float m_leftRight;
        public float m_botTop;

    public void RefreshInfo() {
        if (m_register == null) return;
        BooleanStateRegister reg = null;
        m_register.GetRegister(ref reg);
        if (reg == null) return;
        m_mouseInformation.GetPourcent(out m_leftRight, out m_botTop);

        for (int i = 0; i < m_squareZone.Count; i++)
        {
            reg.Set(m_squareZone[i].GetName(),
                m_squareZone[i].m_zone.IsInZone(m_leftRight, m_botTop));
            if (m_squareZone[i].GetName()== "leftopscreen")
            Debug.Log(string.Format("Test{0}:{1}", m_squareZone[i].GetName(), m_squareZone[i].m_zone.IsInZone(m_leftRight, m_botTop)));

        }
    }


    public void AddScreenZone(NamedScreenPourcentZone screenSquareZone) {

        m_squareZone.Add(screenSquareZone);
    }

    public void ClearAll()
    {
        m_squareZone.Clear();
    }
}

