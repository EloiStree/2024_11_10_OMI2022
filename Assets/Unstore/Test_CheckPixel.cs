using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test_CheckPixel : MonoBehaviour
{

    public LoadDisplayFromJava m_displays;
    public List<DisplayPixelCheck> m_pixelCheck = new List<DisplayPixelCheck>();
 

    void Update()
    {

        for (int i = 0; i < m_pixelCheck.Count; i++)
        {
            if (m_displays.IsDisplayDefined(i)) { 
                m_pixelCheck[i].m_pixelColor = m_displays.GetPixel(m_pixelCheck[i].m_displayIndex, m_pixelCheck[i].m_botTopPourcent, m_pixelCheck[i].m_leftRightPourcent);
                m_pixelCheck[i].CheckColorV0();

            }
        }
        
    }
}

[System.Serializable]
public class DisplayPixelCheck {


    public int m_displayIndex;
    public float m_leftRightPourcent=0.5f;
    public float m_botTopPourcent=0.5f;
    public Color m_pixelColor;
    public Color m_wantedColor;
    public BooleanSwitchListener m_isTheSame;
    public UnityEvent m_isTrue;
    public UnityEvent m_isFalse;
    public float  m_pourcentDiff = 0.1f;

    public void CheckColorV0()
    {
        bool hasChange;
        bool value = Mathf.Abs(m_pixelColor.r - m_wantedColor.r) < m_pourcentDiff
            && Mathf.Abs(m_pixelColor.g - m_wantedColor.g) < m_pourcentDiff
            && Mathf.Abs(m_pixelColor.b - m_wantedColor.b) < m_pourcentDiff;
        m_isTheSame.SetValue( value, out hasChange );
        if (hasChange) {
            if (value)  
                m_isTrue.Invoke();
            else
                m_isFalse.Invoke();
            
        }
    }
}

