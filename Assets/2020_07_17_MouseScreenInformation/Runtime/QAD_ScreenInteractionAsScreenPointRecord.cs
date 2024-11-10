using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QAD_ScreenInteractionAsScreenPointRecord : MonoBehaviour
{

    //WARNING THE CODE HERE IS DIRTY AS SHIT
    //WARNING THE CODE HERE IS DIRTY AS SHIT
    //WARNING THE CODE HERE IS DIRTY AS SHIT
    //WARNING THE CODE HERE IS DIRTY AS SHIT
    //WARNING THE CODE HERE IS DIRTY AS SHIT

    public ScreenPositionEvent m_onScreenMouseDown;
    public ScreenPositionEvent m_onScreenMouseUp;
    public ScreenZoneEvent m_zoneEmitted;

    public bool m_useMouseMiddleToReset=true;
    public bool m_useFourTouchClear=true;
    public UnityEvent m_onClearRequested;
    [Header("Debug")]

    public bool m_mouseDown;
    public Vector3 m_lastMousePosition;
    public bool m_isFirstFingerDown;
    public bool m_isSecondFingerDown;
    public Vector3 m_firstFingerPosition;
    public Vector3 m_secondFingerPosition;

    public bool m_isRightClickDown;
    public Vector3 m_rightClickPositionStart;
    public Vector3 m_rightClickPositionEnd;

    public ZoneDetection m_zonebuilder= new ZoneDetection();
    [System.Serializable]
    public class ZoneDetection
    {
        public bool m_isTwoFingerWasUsed;
        public bool m_isThreeFingerWasUsed;

        public void Reset()
        {
            m_isTwoFingerWasUsed = false;
        }
    }


    void Update()
    {
        if (Application.isFocused)
        {

           

            bool isMouseDown = Input.GetMouseButton(0);
            bool isMouseRightDown = Input.GetMouseButton(1);
            if(Input.GetMouseButtonDown(1))
                m_rightClickPositionStart = Input.mousePosition; 
            if (isMouseDown)
                m_lastMousePosition = Input.mousePosition;
            if (isMouseRightDown)
                m_rightClickPositionEnd = Input.mousePosition;

            if (Input.GetMouseButtonUp(1))
            {
                CreateZoneAndNotifyFrom(m_rightClickPositionStart, m_rightClickPositionEnd);
            }


                if (m_mouseDown!= isMouseDown)
            {
                if (isMouseDown)
                    m_onScreenMouseDown.Invoke(GetScreenPositionOf(Input.mousePosition));
                else
                    m_onScreenMouseUp.Invoke(GetScreenPositionOf(Input.mousePosition));
                m_mouseDown = isMouseDown;
            }

            m_isRightClickDown = isMouseRightDown;


            bool isFirstFingerDown = Input.touches.Length > 0;
            bool isSecondFingerDown = Input.touches.Length > 1;
            if (isFirstFingerDown)
                m_firstFingerPosition = Input.touches[0].position;
            if (isSecondFingerDown)
                m_secondFingerPosition = Input.touches[1].position;

            if (isSecondFingerDown != m_isSecondFingerDown) {
                if (isSecondFingerDown)
                    m_zonebuilder.m_isTwoFingerWasUsed = true;
                else CreateZoneAndNotifyFrom(m_firstFingerPosition, m_secondFingerPosition);
            }
            if (isFirstFingerDown != m_isFirstFingerDown) {
                if (!isFirstFingerDown && m_zonebuilder.m_isTwoFingerWasUsed==false) {
                    m_onScreenMouseUp.Invoke(GetScreenPositionOf(m_firstFingerPosition));
                    ResetAllFingerInformation();
                }
            }

            m_isFirstFingerDown = isFirstFingerDown;
            m_isSecondFingerDown = isSecondFingerDown;


            if (Input.touches.Length > 2)
                m_zonebuilder.m_isThreeFingerWasUsed = true;

            if (m_useMouseMiddleToReset && Input.GetMouseButtonDown(2))
                m_onClearRequested.Invoke();
            if (m_useFourTouchClear && m_zonebuilder.m_isThreeFingerWasUsed && Input.touchCount == 0) { 
                m_onClearRequested.Invoke();
                m_zonebuilder.m_isThreeFingerWasUsed = false;
            }
        }

    }

    private void ResetAllFingerInformation()
    {
        m_zonebuilder.Reset();
        m_firstFingerPosition = Vector3.zero;
        m_secondFingerPosition = Vector3.zero;
    }

    private void CreateZoneAndNotifyFrom(Vector3 firstFingerPosition, Vector3 secondFingerPosition)
    {
        ScreenZoneFullRecord zone = new ScreenZoneFullRecord()
        {
            m_givenNamed = "",
            m_mainScreenDimention = new MainScreenDimensionBean(Screen.width, Screen.height),
            m_zoneInPourcent =new ScreenZoneInPourcentBean(
                GetScreenPourcentBeanOf(firstFingerPosition),
                GetScreenPourcentBeanOf(secondFingerPosition))
        };
        m_zoneEmitted.Invoke(zone);

    }
    private ScreenPositionInPourcentBean GetScreenPourcentBeanOf(Vector3 positionInPxFromUnity)
    {
        return new ScreenPositionInPourcentBean(positionInPxFromUnity.x / (float)Screen.width, positionInPxFromUnity.y / (float)Screen.height);
    }
    private ScreenPositionFullRecord GetScreenPositionOf(Vector3 positionInPxFromUnity)
    {
        return new ScreenPositionAsPixel()
        {
            m_name = "",
            m_mainScreenDimention = new MainScreenDimensionBean(Screen.width, Screen.height),
            m_pixel = new ScreenPositionInPixelBean((int)positionInPxFromUnity.x, (int)positionInPxFromUnity.y)
        };
         
    }
}
