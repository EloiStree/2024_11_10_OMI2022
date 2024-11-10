using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInfoToBooleansDirectionMono : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public MouseInformationAbstract m_mouseInfo;

    public NamedOrientation m_mouseOrientation;


    [System.Serializable]
    public class NamedOrientation
    {

        public string m_N = "MouseN";
        public string m_S = "MouseS";
        public string m_W = "MouseO";
        public string m_E = "MouseE";
        public string m_SE = "MouseSE";
        public string m_SW = "MouseSO";
        public string m_NE = "MouseNE";
        public string m_NW = "MouseNO";
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
    public enum DiagonalDirection { SO, SE, NO, NE, None }
    public enum AllDirection { SO, SE, NO, NE, S, N, O, E, None }
    public float m_directionZonePourcent = 0.8f;


    public Vector2 m_deltaNorm;
    public Direction m_vertical = Direction.None;
    public Direction m_horizontal = Direction.None;
    public DiagonalDirection m_diagonal = DiagonalDirection.None;

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

        if (m_lastDeltaVertical != 0)
        {
            m_vertical = m_lastDeltaVertical < 0 ? Direction.S : Direction.N;
        }
        if (m_lastDeltaHorizontal != 0)
        {
            m_horizontal = m_lastDeltaHorizontal < 0 ? Direction.O : Direction.E;
        }


        if (m_vertical == Direction.S && m_horizontal == Direction.O)
            m_diagonal = DiagonalDirection.SO;
        else if (m_vertical == Direction.S && m_horizontal == Direction.E)
            m_diagonal = DiagonalDirection.SE;
        else if (m_vertical == Direction.N && m_horizontal == Direction.O)
            m_diagonal = DiagonalDirection.NO;
        else if (m_vertical == Direction.N && m_horizontal == Direction.E)
            m_diagonal = DiagonalDirection.NE;


        if (m_lastDeltaVertical != 0 || m_lastDeltaHorizontal != 0)
        {

            m_deltaNorm = new Vector2(m_lastDeltaHorizontal, m_lastDeltaVertical).normalized;
            if (m_usedBooleanRegister)
            {
                if (m_lastDeltaVertical != 0)
                {
                    if(!string.IsNullOrEmpty(m_mouseOrientation.m_N))
                        reg.Set(m_mouseOrientation.m_N, m_vertical == Direction.N);
                    if (!string.IsNullOrEmpty(m_mouseOrientation.m_S))
                        reg.Set(m_mouseOrientation.m_S, m_vertical == Direction.S);
                }
                if (m_lastDeltaHorizontal != 0)
                {
                    if (!string.IsNullOrEmpty(m_mouseOrientation.m_W))
                        reg.Set(m_mouseOrientation.m_W, m_horizontal == Direction.O);
                    if (!string.IsNullOrEmpty(m_mouseOrientation.m_E))
                        reg.Set(m_mouseOrientation.m_E, m_horizontal == Direction.E);
                }
                if (!string.IsNullOrEmpty(m_mouseOrientation.m_SE))
                    reg.Set(m_mouseOrientation.m_SE, m_diagonal == DiagonalDirection.SE);
                if (!string.IsNullOrEmpty(m_mouseOrientation.m_SW))
                    reg.Set(m_mouseOrientation.m_SW, m_diagonal == DiagonalDirection.SO);
                if (!string.IsNullOrEmpty(m_mouseOrientation.m_NE))
                    reg.Set(m_mouseOrientation.m_NE, m_diagonal == DiagonalDirection.NE);
                if (!string.IsNullOrEmpty(m_mouseOrientation.m_NW))
                    reg.Set(m_mouseOrientation.m_NW, m_diagonal == DiagonalDirection.NO);
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
