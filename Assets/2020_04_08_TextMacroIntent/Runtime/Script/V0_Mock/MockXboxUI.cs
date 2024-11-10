using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockXboxUI : MonoBehaviour
{
    [Header("Joystick")]
    public Image m_lj;
    public Image m_rj;

    [Header("Side buttons")]
    public Image m_lb;
    public Image m_lt;
    public Image m_rb;
    public Image m_rt;

    [Header("Buttons")]
    public Image m_x;
    public Image m_y;
    public Image m_b;
    public Image m_a;
    [Header("Arrows")]
    public Image m_left;
    public Image m_up;
    public Image m_right;
    public Image m_down;
    public Color m_onColor = Color.green;
    public Color m_offColor = Color.gray;

    public enum XboxButton :int{ X,Y,B,A,JL,JR,Left,Up,Down,Right, LB,LT,RB,RT }

    public void SetOn(XboxButton buttonType, bool isOn)
    {

        Color color = isOn ? m_onColor : m_offColor;
        switch (buttonType)
        {
            case XboxButton.X:
                m_x.color = color;
                break;
            case XboxButton.Y:
                m_y.color = color;

                break;
            case XboxButton.B:
                m_b.color = color;

                break;
            case XboxButton.A:
                m_a.color = color;

                break;
            case XboxButton.JL:
                m_lj.color = color;

                break;
            case XboxButton.JR:
                m_rj.color = color;

                break;
            case XboxButton.Left:
                m_left.color = color;
                break;
            case XboxButton.Up:
                m_up.color = color;
                break;
            case XboxButton.Down:
                m_down.color = color;
                break;
            case XboxButton.Right:
                m_right.color = color;
                break;
            case XboxButton.LB:
                m_lb.color = color;
                break;
            case XboxButton.LT:
                m_lt.color = color;

                break;
            case XboxButton.RB:
                m_rb.color = color;

                break;
            case XboxButton.RT:
                m_rt.color = color;

                break;
            default:
                break;
        }
    }
}
