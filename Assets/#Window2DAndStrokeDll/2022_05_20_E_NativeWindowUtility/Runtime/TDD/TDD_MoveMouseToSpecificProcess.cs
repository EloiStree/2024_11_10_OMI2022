using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MoveMouseToSpecificProcess : MonoBehaviour
{

    public int m_windowIndex;

    [Range(0,1)]
    public float m_left2RightPercent;
    [Range(0, 1)]
    public float m_bot2TopPercent;


    [Range(0, 1)]
    public float m_left2RightPercentCurrent;
    [Range(0, 1)]
    public float m_bot2TopPercentCurrent;


    public int m_padTop;
    public int m_padLeft;
    public int m_padRight;
    public int m_padDown;

    public int m_xMouse;
    public int m_yMouse;


    public FetchWindowOfProcessBasedOnName m_givenInfoOnWindow;

    public void SetDeductedInfoToUse(FetchWindowOfProcessBasedOnName info) {
        m_givenInfoOnWindow = info;    
    }


    public void Update()
    {
        WindowIntPtrUtility.GetCursorPos(out int x, out int y);
        {
            m_xMouse = x;
            m_yMouse = y;
            if (m_givenInfoOnWindow != null
               && m_givenInfoOnWindow.m_fetchedWindowInfo.Count > m_windowIndex)
            {
                DeductedInfoOfWindowSizeWithSource info = m_givenInfoOnWindow.m_fetchedWindowInfo[m_windowIndex];
                info.m_frameSize.GetLeftToRightPercentFor( x, out m_left2RightPercentCurrent);
                info.m_frameSize.GetBottomToTopPercentFor( y, out m_bot2TopPercentCurrent);

                info.m_frameSize.GetLeftToRightPixelFor(x, out m_padLeft);
                info.m_frameSize.GetRightToLeftPixelFor(x, out m_padRight);
                info.m_frameSize.GetTopToBottomPixelFor(y, out m_padTop);
                info.m_frameSize.GetBottomToTopPixelFor(y, out m_padDown);
                //WindowIntPtrUtility.MouseClickRight();
                //WindowIntPtrUtility.MouseClickMiddle();

                // WindowIntPtrUtility.SetForegroundWindow(info.m_pointer);

            }
        }

    }

    [ContextMenu("MoveMouseForTest")]
    private void MoveMouseForTest()
    {
        if (m_givenInfoOnWindow != null 
            && m_givenInfoOnWindow.m_fetchedWindowInfo.Count > m_windowIndex) {

            DeductedInfoOfWindowSizeWithSource info = m_givenInfoOnWindow.m_fetchedWindowInfo[m_windowIndex];
            info.m_frameSize.GetLeftToRightToAbsolute(in m_left2RightPercent, out int x);
            info.m_frameSize.GetBottomToTopToAbsolute(in m_bot2TopPercent, out int y);
            WindowIntPtrUtility.SetCursorPos(x, y);
            WindowIntPtrUtility.MouseClickLeft();
            //WindowIntPtrUtility.MouseClickRight();
            //WindowIntPtrUtility.MouseClickMiddle();

          //  WindowIntPtrUtility.ShowWindow(info.m_pointer, 3);
             WindowIntPtrUtility.SetForegroundWindow(info.m_pointer);

        }
    }
}
