using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AbstractMidi_DebugLine_NoteChange : MonoBehaviour
{


    public UI_AbstractMidi_DebugLine_IMidiEventBasicGet m_basicInfo;
    public UI_AbstractMidi_DebugLine_IMidiEventNamedElementGet m_elementName;

    public Slider m_velocityPourcent;
    public Toggle m_isOnCheckDisplay;
    public Image m_isOnColorDisplay;
    public Color m_isOnColor = Color.green;
    public Color m_isOffColor = Color.green+ Color.white*0.5f;



    public void PushIn(IMidiNoteEventGet value)
    {
        if (value == null)
        {
            Flush();
            return;
        }

        m_basicInfo.PushIn(value);
        m_elementName.PushIn(value);

        value.IsOn(out bool isOn);
        value.GetVelocity(out Eloi.IPercent01Get pctValue);
        if (m_isOnCheckDisplay != null)
            m_isOnCheckDisplay.isOn = isOn;
        if(m_isOnColorDisplay != null)
            m_isOnColorDisplay.color = isOn? m_isOnColor: m_isOffColor;
        if (m_velocityPourcent)
            m_velocityPourcent.value = pctValue.GetPercent();
    }

    public void Hide(bool hide)
    {
        this.gameObject.SetActive(hide);
    }

    public void Flush()
    {
        m_basicInfo.Flush();
        m_elementName.Flush();
        if (m_isOnCheckDisplay != null)
            m_isOnCheckDisplay.isOn=false;
        if (m_isOnColorDisplay != null)
            m_isOnColorDisplay.color = Color.white;
        if (m_velocityPourcent)
            m_velocityPourcent.value =0;
    }
}
