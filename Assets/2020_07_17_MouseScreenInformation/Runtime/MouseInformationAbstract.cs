using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInformationAbstract : MonoBehaviour
{
    [SerializeField] bool [] m_isMouseButtonsActive= new bool[3];
    [SerializeField] ScreenDirectionFormat m_screenDirection;
    [SerializeField] int m_cursorBotToTopInPx;
    [SerializeField] int m_cursorLeftToRightInPx;
    [SerializeField] int m_mainScreenWidth;
    [SerializeField] int m_mainscreenHeight;
    public bool IsLeftButtonActive() { return m_isMouseButtonsActive[0]; }
    public bool IsRightButtonActive() { return m_isMouseButtonsActive[1]; }
    public bool IsMiddleButtonActive() { return m_isMouseButtonsActive[2]; }

    public void SetLeftButtonActive(bool isOn) {  m_isMouseButtonsActive[0] = isOn; }
    public void SetRightButtonActive(bool isOn) {  m_isMouseButtonsActive[1] = isOn; }
    public void SetMiddleButtonActive(bool isOn) {  m_isMouseButtonsActive[2] = isOn; }

    public void SetMainScreenResolution(int widthPx, int heightPx)
    {
        m_mainScreenWidth = widthPx;
        m_mainscreenHeight = heightPx;
    }

    public void GetPourcent(out float leftRight, out float botTop)
    {
        leftRight = m_cursorLeftToRightInPx / (float)m_mainScreenWidth;
        botTop = m_cursorBotToTopInPx / (float)m_mainscreenHeight;
    }

    public int GetScreenWidth() { return m_mainScreenWidth;  }
    public int GetScreenHeight() { return m_mainscreenHeight; }

    public bool IsCenterButtonActive() { return m_isMouseButtonsActive[2]; }
    public bool IsAlternativeButtonActive (int indexAlternative) { 
            int index = 3 + indexAlternative;
            if(IsButtonIndexExist(index))
                return m_isMouseButtonsActive[index];
        return false;
    }
    public bool IsButtonIndexExist(int index) { return index>-1 &&  index < m_isMouseButtonsActive.Length; }

    public void SetPosition_D2T_L2R(int downToTop, int leftToRight) {
        m_cursorBotToTopInPx = downToTop;
        m_cursorLeftToRightInPx = leftToRight;
    }
    public void SetButtonsCount(int count) {
        if(m_isMouseButtonsActive.Length!=count)
        m_isMouseButtonsActive = new bool[count];
    }
    public void SetButtonValue(int index,bool isActive) {
        if (IsButtonIndexExist(index)) {
            m_isMouseButtonsActive[index]=isActive;
        }       
    }

    public void GetMousePositionOnScreen(out int botToTop, out int leftToRight) {
        botToTop = m_cursorBotToTopInPx;
        leftToRight = m_cursorLeftToRightInPx;
    }
}
