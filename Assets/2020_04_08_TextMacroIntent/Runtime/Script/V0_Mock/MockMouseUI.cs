using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockMouseUI : MonoBehaviour
{
    public Image[] m_buttons;
    public Image m_scrollLeft;
    public Image m_scrollUp;
    public Image m_scrollRight;
    public Image m_scrollDown;
    public Image m_scrollCenter;
    public Color m_onColor = Color.green;
    public Color m_offColor= Color.gray;

    public enum MouseButton { ScLeft, ScUp, ScDown, ScRight, ScCenter, B0, B1, B2, B3, B4 }

    public void SetOn(MouseButton buttonType, bool isOn) {

        Color color = isOn ? m_onColor : m_offColor;
        switch (buttonType)
        {
            case MouseButton.ScLeft:
                m_scrollLeft.color = color;
                break;
            case MouseButton.ScUp:
                m_scrollUp.color = color;
                break;
            case MouseButton.ScDown:
                m_scrollDown.color = color;
                break;
            case MouseButton.ScRight:
                m_scrollRight.color = color;
                break;
            case MouseButton.ScCenter:
                m_scrollCenter.color = color;
                break;
            case MouseButton.B0:
                m_buttons[0].color = color;
                break;
            case MouseButton.B1:
                m_buttons[1].color = color;
                break;
            case MouseButton.B2:
                m_buttons[2].color = color;
                break;
            case MouseButton.B3:
                m_buttons[3].color = color;
                break;
            case MouseButton.B4:
                m_buttons[4].color = color;
                break;
            default:
                break;
        }
    }
}
