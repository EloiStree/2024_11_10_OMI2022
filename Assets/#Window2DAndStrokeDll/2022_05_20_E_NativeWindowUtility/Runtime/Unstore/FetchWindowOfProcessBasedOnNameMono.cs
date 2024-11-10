using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FetchWindowOfProcessBasedOnNameMono : MonoBehaviour
{
    public ProcessesAccessMono m_processes;
    public bool m_reloadOnFetch = false;
    public string m_processNameToFetch = "Wow";
    public FetchWindowOfProcessBasedOnName m_fetchWindowInfo;

    public RelayFoundInfo m_relayRefresh;

    [System.Serializable]
    public class RelayFoundInfo : UnityEvent<FetchWindowOfProcessBasedOnName> { }

    private void Start()
    {
        FetchWindowsInfoWithParams();
    }

    public void SetWith(GroupOfProcessesInfo info) {
        FetchWindowInfoUtility.FetchInfoForTargetProcses(info, m_processNameToFetch
                , out m_fetchWindowInfo);
        m_relayRefresh.Invoke(m_fetchWindowInfo);
    }

    [ContextMenu("Frech Info")]
    private void FetchWindowsInfoWithParams()
    {
        FetchWindowInfoUtility.FetchInfoForTargetProcses(m_processes, m_processNameToFetch, m_reloadOnFetch
            , out m_fetchWindowInfo);
        m_relayRefresh.Invoke(m_fetchWindowInfo);
    }

    internal void Fetch(in int processId, out bool found,
        out DeductedInfoOfWindowSizeWithSource info)
    {
        found = false;
        info = null;
        foreach (var item  in m_fetchWindowInfo.m_fetchedWindowInfo)
        {
            if (Eloi.E_StringUtility.AreEquals("" + item.m_pointerDebug, "" + processId, true, true))
            {
                found = true;
                info = item;
                return;
            }
        }
    }
}


[System.Serializable]
public class FetchWindowOfProcessBasedOnName {

    public string m_processNameToFetch = "Wow";
    public List<DeductedInfoOfWindowSizeWithSource> m_fetchedWindowInfo= new List<DeductedInfoOfWindowSizeWithSource>();
}






public class FetchWindowInfoUtility {


    //IF you know who to access that info from user32 contact me please.
    public static int m_topBorderInPixel=32;
    public static int m_downBorderInPixel=10;
    public static int m_leftBorderInPixel=7;
    public static int m_rightBorderInPixel=7;

    public static void SetWindowBorders(in int top, in int right, in int down, in int left ) {
        m_topBorderInPixel = top;
        m_rightBorderInPixel = right;
        m_downBorderInPixel = down;
        m_leftBorderInPixel = left;
    
    }

    public static void Get(IntPtrWrapGet target, out DeductedInfoOfWindowSizeWithSource rectPadValue)
    {
        Get(target, out WindowIntPtrUtility.RectPadValue value);
        rectPadValue = new DeductedInfoOfWindowSizeWithSource(target, value);
    }
        public static void Get(IntPtrWrapGet target, out WindowIntPtrUtility.RectPadValue rectPadValue)
    {
        rectPadValue = new WindowIntPtrUtility.RectPadValue();
        WindowIntPtrUtility.GetWindowRect(target, ref rectPadValue);
    }
    public static void GetInRef(IntPtr target, ref WindowIntPtrUtility.RectPadValue rectPadValue)
    {
        WindowIntPtrUtility.GetWindowRect(target, ref rectPadValue);
    }
    public static void Get(WindowIntPtrUtility.RectPadValue from, out DeductedInfoOfWindowSize rectPadValue)
    {

        rectPadValue = new DeductedInfoOfWindowSize(from);

    }
    public static void Get(IntPtrWrapGet target, out DeductedInfoOfWindowSize rectPadValue)
    {
        Get(target, out WindowIntPtrUtility.RectPadValue r);
        Get(r, out rectPadValue);
    }


    public static void FetchInfoForTargetProcses(GroupOfProcessesInfo access, string processNameToFetch,
         out FetchWindowOfProcessBasedOnName windowProcessName)
    {
        windowProcessName = new FetchWindowOfProcessBasedOnName();
        windowProcessName.m_processNameToFetch = processNameToFetch;
        windowProcessName.m_fetchedWindowInfo.Clear();
        access.GetWith(out List<WindowIntPtrUtility.ProcessInformation> list);
        foreach (var item in list)
        {
            if (Eloi.E_StringUtility.AreEquals(processNameToFetch, item.m_processName))
            {
                WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(IntPtrTemp.Int(item.m_processId),
                    out bool found, out IntPtrWrapGet childPointer);
                if (found)
                {
                    FetchWindowInfoUtility.Get(childPointer, out WindowIntPtrUtility.RectPadValue rect);
                    if (rect.IsNotZero())
                    {
                        windowProcessName.m_fetchedWindowInfo.Add(new DeductedInfoOfWindowSizeWithSource(childPointer, rect));
                    }
                }

            }
        }
    }

    public static void FetchInfoForTargetProcses(ProcessesAccessMono access,string processNameToFetch,  bool realoadOnFetch,
        out FetchWindowOfProcessBasedOnName windowProcessName)
    {
        windowProcessName = new FetchWindowOfProcessBasedOnName();
        windowProcessName.m_processNameToFetch = processNameToFetch;
         GroupOfProcessesParentToChildrens groupOfProcess;
        // WindowIntPtrUtility.
        access.FetchListOfProcessesBasedOnName(
               processNameToFetch,
                out groupOfProcess,
                realoadOnFetch);

        windowProcessName.m_fetchedWindowInfo.Clear();
        List<ProcessIdWithChildGroupInfo> process = groupOfProcess.m_processesAndChildrens;
        foreach (var item in process)
        {
            item.GetListOfPointers(out List<IntPtrWrapGet> pointers, true);
            foreach (var itemPtr in pointers)
            {
                FetchWindowInfoUtility.Get(itemPtr, out WindowIntPtrUtility.RectPadValue rect);
                if (rect.IsNotZero())
                {
                    windowProcessName.m_fetchedWindowInfo.Add(new DeductedInfoOfWindowSizeWithSource(itemPtr, rect));
                }
            }
        }
    }

}


[System.Serializable]
public class DeductedInfoOfWindowSizeWithSource {

    public IntPtrWrapGet m_pointer;
    public int m_pointerDebug;
    public DeductedInfoOfWindowSize m_frameSize;
    public DeductedInfoOfWindowSizeWithSource(IntPtrWrapGet pointer, 
        WindowIntPtrUtility.RectPadValue from)
    {
        SetPointer(pointer);
        SetWith(from);
    }

    public void SetPointer(IntPtrWrapGet pointer) {
        this.m_pointer = pointer;
        this.m_pointerDebug = pointer.GetAsInt();
    }
    public void SetWith(WindowIntPtrUtility.RectPadValue rect)
    {
        m_frameSize = new DeductedInfoOfWindowSize(rect);
    }
}


[System.Serializable]
public class DeductedInfoOfWindowSize
{
    public WindowIntPtrUtility.RectPadValue m_given;
    public WindowIntPtrUtility.RectPadValue m_givenWithoutBorder;

    public int m_innerWidth;
    public int m_innerHeight;
    public int m_innerCenterX;
    public int m_innerCenterY;

    public DeductedInfoOfWindowSize( WindowIntPtrUtility.RectPadValue from)
    {
        this.m_given = from;
        m_givenWithoutBorder= RemoveBorderWithAppDefault(m_given);
        m_innerWidth = Math.Abs(m_givenWithoutBorder.m_borderLeft
            - m_givenWithoutBorder.m_borderRight);
        m_innerHeight = Math.Abs(m_givenWithoutBorder.m_borderTop
            - m_givenWithoutBorder.m_borderBottom);
        m_innerCenterX = (int)((m_givenWithoutBorder.m_borderLeft
            + m_givenWithoutBorder.m_borderRight) / 2f);
        m_innerCenterY = (int)((m_givenWithoutBorder.m_borderTop
            + m_givenWithoutBorder.m_borderBottom) / 2f);
    }

   
    public void GetTopToBottom(in float percent, out int y)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        y = (int)(m_givenWithoutBorder.m_borderTop + m_innerHeight * percent);
    }
    public void GetRightToLeft(in float percent, out int x)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        x = (int)(m_givenWithoutBorder.m_borderRight - m_innerWidth * percent);
    }
    public void GetBottomToTopToAbsolute(in float percent, out int y)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        y = (int)(m_givenWithoutBorder.m_borderBottom - m_innerHeight * percent);
    }
    public void GetTopToBottomToAbsolute(in float percent, out int y)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        y = (int)(m_givenWithoutBorder.m_borderTop + m_innerHeight * percent);
    }
    public void GetLeftToRightToAbsolute(in float percent, out int x)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        x = (int)(m_givenWithoutBorder.m_borderLeft + m_innerWidth * percent);
    }
    public void GetBottomToTopToRelative(in float percent, out int y)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        y = (int)(m_innerHeight * (1f-percent));
    }
    public void GetLeftToRightToRelative(in float percent, out int x)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        x = (int)(m_innerWidth * percent);
    }

    public void GetTopToBottomToRelative(in float percent, out int y)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        y = (int)(m_innerHeight * percent);
    }
    public void GetRightToLeftToRelative(in float percent, out int x)
    {
        Eloi.E_CodeTag.QualityAssurance.RequestTestingInAsSoonAsPossible();
        x = (int)(m_innerWidth * (1f - percent));
    }



    private WindowIntPtrUtility.RectPadValue RemoveBorderWithAppDefault(WindowIntPtrUtility.RectPadValue given)
    {
        WindowIntPtrUtility.RectPadValue result = new WindowIntPtrUtility.RectPadValue();
        result.m_borderTop = given.m_borderTop + FetchWindowInfoUtility.m_topBorderInPixel;
        result.m_borderLeft = given.m_borderLeft + FetchWindowInfoUtility.m_leftBorderInPixel;
        result.m_borderRight = given.m_borderRight - FetchWindowInfoUtility.m_rightBorderInPixel;
        result.m_borderBottom = given.m_borderBottom - FetchWindowInfoUtility.m_downBorderInPixel;
        return result;
    }

    public void GetLeftToRightPercentFor(int x, out float left2RightPercent)
    {
        float left = m_givenWithoutBorder.m_borderLeft;
        float right = m_givenWithoutBorder.m_borderRight;
        left2RightPercent = (x - left) / (right - left);

    }

    public void GetBottomToTopPercentFor(int y, out float bot2TopPercent)
    {
        float top = m_givenWithoutBorder.m_borderTop;
        float bot = m_givenWithoutBorder.m_borderBottom;
        bot2TopPercent = (1f - ((y - top) / (bot - top)));
    }

    public void GetLeftToRightPixelFor(int x, out int left2RightPixel)
    {
        int left = m_givenWithoutBorder.m_borderLeft;
        int right = m_givenWithoutBorder.m_borderRight;
        left2RightPixel = x - left;
    }

    public void GetBottomToTopPixelFor(int y, out int bot2TopPixel)
    {
        int bot = m_givenWithoutBorder.m_borderBottom;
        bot2TopPixel = (bot-y);
    }
    public void GetRightToLeftPixelFor(int x, out int right2LeftPixel)
    {
        int left = m_givenWithoutBorder.m_borderLeft;
        int right = m_givenWithoutBorder.m_borderRight;
        right2LeftPixel = right-x;

    }
    public void GetTopToBottomPixelFor(int y, out int top2BotPixel)
    {
        int top = m_givenWithoutBorder.m_borderTop;
        top2BotPixel = y-top;
    }

    public void GetAbsoluteFromRelativePixelLeft2Right(int pixelLeft2RightRelative, out int x)
    {
        float percent = pixelLeft2RightRelative / (float) m_innerWidth;
        GetLeftToRightToAbsolute(percent, out x);
    }

    public void GetAbsoluteFromRelativePixelBot2Top(int pixelBot2TopRelative, out int absoluteY)
    {
        float percent = pixelBot2TopRelative / (float)m_innerHeight;
        GetBottomToTopToAbsolute(percent, out absoluteY);
    }

    public void GetAbsoluteFromRelativePixelTop2Bot(int pixelTop2BotRelative, out int absoluteY)
    {
        float percent =(pixelTop2BotRelative/(float)m_innerHeight);
        GetTopToBottomToAbsolute(percent, out absoluteY);
    }
}