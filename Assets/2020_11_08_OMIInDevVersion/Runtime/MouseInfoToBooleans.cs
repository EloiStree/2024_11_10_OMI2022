using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseInfoToBooleans : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public MouseInformationAbstract m_mouseInfo;

    public NamedOrientation m_mouseOrientation;


    [System.Serializable]
    public class NamedOrientation
    {

        public string m_N = "MouseN";
        public string m_S = "MouseS";
        public string m_O = "MouseO";
        public string m_E = "MouseE";
        public string m_SE = "MouseSE";
        public string m_SO = "MouseSO";
        public string m_NE = "MouseNE";
        public string m_NO = "MouseNO";
    }
    public bool m_usedBooleanRegister;
    public string m_mouseMovingNamed = "MouseMoving";
    public float m_mouseMovingEndDelay = 0.1f;
    public int m_bot2Top;
    public int m_left2Right;
    public int m_previousBot2Top;
    public int m_previousLeft2Right;
    public bool m_mouseIsMoving;
    public float m_mouseIsMovingCountDown;
    public int m_deathZoneInPx = 10;
    public int m_lastDeltaHorizontal;
    public int m_lastDeltaVertical;
    public enum Direction { S, N, O, E, None }
    public float m_directionZonePourcent = 0.8f;


    public Vector2 deltaNorm;
    void Update()
    {
        BooleanStateRegister reg = null;
        if (m_register == null) 
            return;
        m_register.GetRegister(ref reg);
        if (reg == null)
            return;

        m_mouseInfo.GetMousePositionOnScreen(out m_bot2Top, out m_left2Right);
        m_lastDeltaVertical = m_bot2Top - m_previousBot2Top;
        m_lastDeltaHorizontal = m_left2Right - m_previousLeft2Right;
        if (m_lastDeltaVertical != 0 || m_lastDeltaHorizontal != 0)
        {

            deltaNorm = new Vector2(m_lastDeltaHorizontal, m_lastDeltaVertical).normalized;
            Direction vertical = Direction.None;
            Direction horizontal = Direction.None;
            if (deltaNorm.x < -m_directionZonePourcent)
                horizontal = Direction.O;
            else if (deltaNorm.x > m_directionZonePourcent)
                horizontal = Direction.E;
            else horizontal = Direction.None;
            if (deltaNorm.y < -m_directionZonePourcent)
                vertical = Direction.S;
            else if (deltaNorm.y > m_directionZonePourcent)
                vertical = Direction.N;
            else vertical = Direction.None;

            if (m_usedBooleanRegister)
            {

                reg.Set(m_mouseOrientation.m_N, vertical == Direction.N);
                reg.Set(m_mouseOrientation.m_S, vertical == Direction.S);
                reg.Set(m_mouseOrientation.m_O, horizontal == Direction.O);
                reg.Set(m_mouseOrientation.m_E, horizontal == Direction.E);
                reg.Set(m_mouseOrientation.m_SE, vertical == Direction.S && horizontal == Direction.E);
                reg.Set(m_mouseOrientation.m_SO, vertical == Direction.S && horizontal == Direction.O);
                reg.Set(m_mouseOrientation.m_NE, vertical == Direction.N && horizontal == Direction.E);
                reg.Set(m_mouseOrientation.m_NO, vertical == Direction.N && horizontal == Direction.O);

            }
        }
        else
        {
            if (m_usedBooleanRegister)
            {

                reg.Set(m_mouseOrientation.m_N, false);
                reg.Set(m_mouseOrientation.m_S, false);
                reg.Set(m_mouseOrientation.m_O, false);
                reg.Set(m_mouseOrientation.m_E, false);
                reg.Set(m_mouseOrientation.m_SE, false);
                reg.Set(m_mouseOrientation.m_SO, false);
                reg.Set(m_mouseOrientation.m_NE, false);
                reg.Set(m_mouseOrientation.m_NO, false);
            }
        }

        if ((m_bot2Top != m_previousBot2Top || m_left2Right != m_previousLeft2Right))
        {
            if (Mathf.Abs(m_bot2Top - m_previousBot2Top) > m_deathZoneInPx ||
                Mathf.Abs(m_left2Right - m_previousLeft2Right) > m_deathZoneInPx)
            {

                m_mouseIsMoving = true;
                m_mouseIsMovingCountDown = m_mouseMovingEndDelay;
            }
        }
        else m_mouseIsMoving = false;

        reg.Set(m_mouseMovingNamed, m_mouseIsMovingCountDown > 0);

        if (m_mouseIsMovingCountDown >= 0)
            m_mouseIsMovingCountDown -= Time.deltaTime;

        //if (m_mouseIsMoving) { 
        m_previousBot2Top = m_bot2Top;
        m_previousLeft2Right = m_left2Right;
        //}

    }
}
