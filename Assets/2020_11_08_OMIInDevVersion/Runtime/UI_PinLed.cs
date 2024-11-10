using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class UI_PinLed : MonoBehaviour
{
    public Text m_pinId;
    public Image m_intensity;

    public void SetId(uint id)
    {
        m_pinId.text = ""+id;
    }
    public void SetColor(Color color)
    {
        m_intensity.color = color;
    }
    public void SetIntensity(float pourcent)
    {
        m_intensity.fillAmount = pourcent ;
    }
}
