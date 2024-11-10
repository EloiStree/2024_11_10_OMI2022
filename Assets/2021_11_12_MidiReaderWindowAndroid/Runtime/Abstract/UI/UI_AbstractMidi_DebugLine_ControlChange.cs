using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AbstractMidi_DebugLine_ControlChange : MonoBehaviour
{


    public UI_AbstractMidi_DebugLine_IMidiEventBasicGet m_basicInfo;
    public UI_AbstractMidi_DebugLine_IMidiEventNamedElementGet m_elementName;


    public InputField m_percentRawValueDisplay;
    public Slider m_percentDisplay;



    public void PushIn(IMidiControlChangeEventGet value)
    {
        if (value == null)
        {
            Flush();
            return;
        }

        m_basicInfo.PushIn(value);
        m_elementName.PushIn(value);

        value.GetControlRawValue(out int controlValue);
        if (m_percentRawValueDisplay != null)
            m_percentRawValueDisplay.text = "" + controlValue;
        value.GetControlPercentValue(out IPercent01Get pourcentOnValue);
        if (m_percentDisplay != null)
            m_percentDisplay.value = pourcentOnValue.GetPercent();

    }

    public void Hide(bool hide)
    {
        this.gameObject.SetActive(hide);
    }

    public void Flush()
    {
        m_basicInfo.Flush();
        m_elementName.Flush();
        if (m_percentRawValueDisplay != null)
            m_percentRawValueDisplay.text = "";
        if (m_percentDisplay != null)
            m_percentDisplay.value = 0;
    }


}