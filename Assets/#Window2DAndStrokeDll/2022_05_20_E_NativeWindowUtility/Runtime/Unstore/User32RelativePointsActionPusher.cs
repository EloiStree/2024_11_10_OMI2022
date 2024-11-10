
using Eloi;
using UnityEngine;

public class User32RelativePointsActionPusher
{

    private readonly static object threadLock = new object();
    public delegate void MouseActionBasedOnRelativePoints(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point);
    public delegate void MouseActionBasedOnRelativeKey(IntPtrWrapGet pointer);
    public static void PointsListOfPressReleaseActions(
        IntPtrWrapGet pointer,
        MouseActionBasedOnRelativeKey toDoAtTheStart,
        MouseActionBasedOnRelativePoints actionDown,
        MouseActionBasedOnRelativePoints actionUp,
        MouseActionBasedOnRelativePoints moveTo,
        MouseActionBasedOnRelativeKey toDoAtTheEnd,
        User32RelativePixelPointLRTB[] points,
        out int msCountAtEnd,
        int forgroundMsWait = 150,
        int betweenActionMsWait = 90,
        int pressActionMsWait = 0,
        int tempTimeBeforeStartMs = 0,
        int previsionMoveAfter = 30,
        bool useForgroundAtStart = true
        )
    {
        lock (threadLock)
        {
            int ms = tempTimeBeforeStartMs;
            if (useForgroundAtStart)
            {
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    WindowIntPtrUtility.SetForegroundWindow(pointer);
                });

                ms += forgroundMsWait;
            }

            if (toDoAtTheStart != null)
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    toDoAtTheStart(pointer);
                });
            for (int i = 0; i < points.Length; i++)
            {
                User32RelativePixelPointLRTB p = new User32RelativePixelPointLRTB(points[i].m_pixelLeft2Right, points[i].m_pixelTop2Bot);

                if (i == 0)
                {
                    ms += (previsionMoveAfter);
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                        if (moveTo != null)
                            moveTo(pointer, p);
                    });
                    ms += (previsionMoveAfter);
                }

                ms += betweenActionMsWait;
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (actionDown != null)
                        actionDown(pointer, p);
                });
                ms += pressActionMsWait;
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (actionUp != null)
                        actionUp(pointer, p);
                });
                ms += (previsionMoveAfter);
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (moveTo != null)
                        moveTo(pointer, p);
                });
                ms += (previsionMoveAfter);


            }
            ms += betweenActionMsWait;
            if (toDoAtTheEnd != null)
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {

                    toDoAtTheEnd(pointer);
                });
            msCountAtEnd = ms;
        }

    }
}



public class User32WindowRectUtility
{

    public static void RefreshInfoOf(ref DeductedInfoOfWindowSizeWithSource rectInfo)
    {
        WindowIntPtrUtility.RefreshInfoOf(ref rectInfo);
    }

    public static void HorizontalLineLeftRightClick(
        in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Bot2TopPercent01 marginDown,
        in Left2RightPercent01 marginLeft,
        in Right2LeftPercent01 marginRight,
        int pointCount,
        out User32RelativePixelPointLRTB[] points
        )
    {
        points = new User32RelativePixelPointLRTB[pointCount];
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int xl);
        rectInfo.m_frameSize.GetRightToLeftToRelative((marginRight.GetPercent()), out int xr);
        for (int i = 0; i < pointCount; i++)
        {
            float percent = i / (float)pointCount;
            points[i] = new User32RelativePixelPointLRTB((int)Mathf.Lerp(xl, xr, percent), y);
        }
    }
    public static void VerticalLineBotTopClick(in DeductedInfoOfWindowSizeWithSource rectInfo, in Left2RightPercent01 marginLeft, in Bot2TopPercent01 marginDown, in Right2LeftPercent01 marginTop)
    {
        Eloi.E_ThrowException.ThrowNotImplemented();
    }

    public enum HeightMeasure { BasedOnWidth, BasedOnHeight, BasedOnMagnitude }
    public static void RandomSphereClickAroundPoint(in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Left2RightPercent01 marginLeft,
        in Bot2TopPercent01 marginDown,
        in Percent01 screenPercent,
        HeightMeasure heightMeasure,
        int pointCount,
        out User32RelativePixelPointLRTB[] points)
    {
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int x);
        int radiusInPixel = 1;
        if (heightMeasure == HeightMeasure.BasedOnWidth)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerWidth * screenPercent.GetPercent());
        if (heightMeasure == HeightMeasure.BasedOnHeight)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerHeight * screenPercent.GetPercent());

        points = new User32RelativePixelPointLRTB[pointCount];
        for (int i = 0; i < points.Length; i++)
        {
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int rx);
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int ry);
            points[i].m_pixelLeft2Right = x + rx;
            points[i].m_pixelTop2Bot = y + ry;
        }
    }
}

[System.Serializable]
public struct User32RelativePixelPointLRTB
{
    public int m_pixelLeft2Right;
    public int m_pixelTop2Bot;

    public User32RelativePixelPointLRTB(int pixelLeft2Right, int pixelTop2Bot)
    {
        m_pixelLeft2Right = pixelLeft2Right;
        m_pixelTop2Bot = pixelTop2Bot;
    }
}