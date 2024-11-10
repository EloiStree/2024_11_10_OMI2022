using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BooleanStateItemDefault : UI_BooleanStateItem
{

    public Text m_textToAffectColor;
    public Image m_imageToAffectColor;

    protected override void SetWithColor(Color color)
    {
        if (m_textToAffectColor)
            m_textToAffectColor.color = color;
        if (m_imageToAffectColor)
            m_imageToAffectColor.color = color;
    }
    protected new void Reset()
    {
        base.Reset();
        m_textToAffectColor = GetComponentInChildren<Text>();
        m_imageToAffectColor = GetComponentInChildren<Image>();
    }
    public void SendTextToClipboard() {

        ClipboardUtility.Set(m_textToAffectColor.text + "↓ " + m_textToAffectColor.text + "↑");
    }
}

public abstract class UI_BooleanStateItem : MonoBehaviour
{

    public Text m_nameOfTheBoolean;
    public Color m_isTrue = Color.green;
    public Color m_isFalse=Color.red;
 
    public void SetName(string name) { m_nameOfTheBoolean.text = name; }
    public void SetValue(bool value) { SetWithColor(value ? m_isTrue : m_isFalse); }

    protected abstract void SetWithColor(Color color);

   

    protected void Reset()
    {
        m_nameOfTheBoolean = GetComponentInChildren<Text>();
    }
}
