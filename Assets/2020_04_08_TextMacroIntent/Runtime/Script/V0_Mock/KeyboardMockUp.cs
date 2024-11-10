using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMockUp : MonoBehaviour
{
    public Text m_text;
    public Image m_backGround;
    public Color m_onColor = Color.green;
    public Color m_offColor = Color.gray;


    public void Press(string txt, bool isOn) {
        m_text.text = txt;
        m_backGround.color = isOn ? m_onColor : m_offColor;
    }
}
