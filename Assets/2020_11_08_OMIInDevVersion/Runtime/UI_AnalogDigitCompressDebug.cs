using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_AnalogDigitCompressDebug : MonoBehaviour
{
    public Text m_id;
    public UI_PinLed[] m_digits;
    public UI_PinLed[] m_analogs;
    public Color m_high;
    public Color m_low;

    public void SetPortId(uint portId) { m_id.text =""+ portId; }
    public void SetDigit(uint index, bool value)
    {

        if (index < m_digits.Length) { 
        
            m_digits[index].SetColor(value ? m_high : m_low);
            m_digits[index].SetId(index);
        }
    }

    public void SetAsNotUsed()
    {
        SetPortId(0);
        foreach (var item in m_digits)
        {
            item.SetColor(Color.white);
        }
        foreach (var item in m_analogs)
        {

            item.SetIntensity( 1f);
            item.SetColor ( Color.white);
        }
    }

    public void SetAnalog(uint index, bool value, short intensity)
    {

        if (index < m_analogs.Length)
        {
            m_analogs[index].SetIntensity(intensity / 9f);
            m_analogs[index].SetColor(value ? m_high : m_low);
            m_analogs[index].SetId(index);
        }
    }

    public int GetNumberOfDigit()
    {
        return m_digits.Length;
    }

    public int GetNumberOfAnalog()
    {
        return m_analogs.Length;
    }
}
