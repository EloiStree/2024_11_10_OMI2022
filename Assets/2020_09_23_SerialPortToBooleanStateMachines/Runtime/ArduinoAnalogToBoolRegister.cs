using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoAnalogToBoolRegister : MonoBehaviour
{
    
    public AnalogDigitalCompressionSerialized m_arduinoReceivedMessage;
    public BooleanStateRegisterMono m_booleanStateRegister;

    public AnalogRangeToBooleanLabel[] m_analogObserved = new AnalogRangeToBooleanLabel[] {
        new AnalogRangeToBooleanLabel(1, "Paint Up Left Foot"),
        new AnalogRangeToBooleanLabel(2, "Paint Down Left Foot"),
        new AnalogRangeToBooleanLabel(3, "Paint Up Right Foot"),
        new AnalogRangeToBooleanLabel(4, "Paint Down Right Foot"),
        new AnalogRangeToBooleanLabel(5, "Paint Center Foot"),
        new AnalogRangeToBooleanLabel(6, "Light Up Left Foot"),
        new AnalogRangeToBooleanLabel(7, "Light Down Left Foot"),
        new AnalogRangeToBooleanLabel(8, "Light Up Right Foot"),
        new AnalogRangeToBooleanLabel(9, "Light Down Right Foot"),
        new AnalogRangeToBooleanLabel(10, "Light Center Foot"),


    };





    public void UseReceivedMessage(string message) {
        message = message.Trim();
        if (string.IsNullOrEmpty(message))
            return;
        if (message.Length < 1)
            return;
        if (message[0] != '#')
            return;
        bool isValide;
        m_arduinoReceivedMessage.SetWithCompressMessage(message, out isValide);

        for (int i = 0; i < m_analogObserved.Length; i++)
        {
            m_analogObserved[i].SetValue(m_arduinoReceivedMessage.GetAnalogValue(m_analogObserved[i].m_arduinoAnlogIndex));

        }

        

        BooleanStateRegister register = m_booleanStateRegister.GetRegister();
        for (int i = 0; i < m_analogObserved.Length; i++)
        {
            register.Set(m_analogObserved[i].m_label, m_analogObserved[i].GetState());

        }

       
    }

}



[System.Serializable]
public class AnalogRangeToBooleanLabel {
    public string m_label;
    public uint m_arduinoAnlogIndex;
    public float m_minRange = 0;
    public float m_maxRange = 4;
    [Header("Debug")]
    public bool m_state;
    public float m_value;

    public AnalogRangeToBooleanLabel() { }
    public AnalogRangeToBooleanLabel(uint index, string label) {
        m_arduinoAnlogIndex = index;
        m_label = label;
    }

    public void SetValue(float value) {

        m_value = value;
        m_state = (m_value >= m_minRange && m_value <= m_maxRange);
    }

    public bool GetState()
    {
        return m_state;
    }
}
