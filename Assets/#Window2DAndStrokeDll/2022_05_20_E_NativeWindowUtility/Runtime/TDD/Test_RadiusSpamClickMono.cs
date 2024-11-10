using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_RadiusSpamClickMono : MonoBehaviour
{
    public int m_processId;
    [ContextMenu("Left click")]
    public void LeftClick()
    {
        m_radiusSpamming.LeftClick(m_processId);
    }
    [ContextMenu("Right click")]
    public void RightClick()
    {
        m_radiusSpamming.RightClick(m_processId);
    }
    [ContextMenu("Middle click")]
    public void MiddleClick()
    {
        m_radiusSpamming.RightClick(m_processId);
    }
    public RadiusSpammingClick m_radiusSpamming;
    [System.Serializable]
    public class RadiusSpammingClick {

        public Eloi.Left2RightPercent01 m_left2Right = new Eloi.Left2RightPercent01(0.5f);
        public Eloi.Bot2TopPercent01 m_bot2Top= new Eloi.Bot2TopPercent01(0.5f);
        public Eloi.Percent01 m_radiusPercent = new Eloi.Percent01(0.1f);
        public User32WindowRectUtility.HeightMeasure m_basedOn = User32WindowRectUtility.HeightMeasure.BasedOnWidth;
        public DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget;
        public int m_numberOfClick = 15;
        public int m_timeBetweenClicksMs = 80;
        public int m_timePressedClicksMs = 3;
        public int m_timeToSwitchWindowMs = 150;
        public int m_timeBeforeStarting = 15;
        public bool m_useForground;
        public bool m_useRealClick;
        public void LeftClick(int processId)
        {
            Click(IntPtrTemp.Int(processId), Eloi.E_EnumUtility.LeftRightMidEnum.Left);

        }
        public void RightClick(int processId)
        {
            Click(IntPtrTemp.Int(processId), Eloi.E_EnumUtility.LeftRightMidEnum.Right);
        }
        public void MiddleClick(int processId)
        {
            Click(IntPtrTemp.Int(processId), Eloi.E_EnumUtility.LeftRightMidEnum.Middle);
        }


        public void Click(IntPtrWrapGet processId, Eloi.E_EnumUtility.LeftRightMidEnum side) {
            FetchWindowInfoUtility.Get(processId, out m_deductedInfoTarget);
            User32WindowRectUtility.RandomSphereClickAroundPoint(
                in m_deductedInfoTarget,
                in m_left2Right,
                in m_bot2Top,
                in m_radiusPercent,
                m_basedOn,
                m_numberOfClick,
                out User32RelativePixelPointLRTB[] points

                );

            User32RelativePointsActionPusher.PointsListOfPressReleaseActions(
                processId
                , null,
                (IntPtrWrapGet p, User32RelativePixelPointLRTB pt) =>
                {
                    if (m_useRealClick)
                    {
                        m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right
                        (pt.m_pixelLeft2Right, out int x);
                        m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(
                            pt.m_pixelTop2Bot, out int y);

                        if (side == Eloi.E_EnumUtility.LeftRightMidEnum.Left)
                            User32MouseManager.LeftClick(x, y);
                        else if (side == Eloi.E_EnumUtility.LeftRightMidEnum.Right)
                            User32MouseManager.RightClick(x, y);
                        else 
                            User32MouseManager.MiddleClick(x, y);
                    }
                    else {
                        if (side == Eloi.E_EnumUtility.LeftRightMidEnum.Left)
                            User32MouseManager.LeftClick(p, pt.m_pixelLeft2Right, pt.m_pixelTop2Bot);
                        else if(side == Eloi.E_EnumUtility.LeftRightMidEnum.Right)
                            User32MouseManager.RightClick(p, pt.m_pixelLeft2Right, pt.m_pixelTop2Bot);
                        else
                            User32MouseManager.MiddleClick(p, pt.m_pixelLeft2Right, pt.m_pixelTop2Bot);
                    }


                },
                null,
                null,
                null,
                points,
                out int timeUsed,
                m_timeToSwitchWindowMs,
                m_timeBetweenClicksMs,
                m_timePressedClicksMs,
                m_timeBeforeStarting,30
                ,
                m_useForground
                );
        }
    }
}


