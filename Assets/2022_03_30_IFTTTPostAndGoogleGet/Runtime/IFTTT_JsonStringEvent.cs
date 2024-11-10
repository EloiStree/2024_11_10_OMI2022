using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IFTTT_JsonStringEvent : IFTTT_StringEvent ,I_IFTTTJsonStringEvent
{
    public string m_value0;
    public string m_value1;
    public string m_value2;

   
    public void GetValue0(out string value)
    {
        value = m_value0;
    }

    public void GetValue1(out string value)
    {
        value = m_value1;
    }

    public void GetValue2(out string value)
    {
        value = m_value2;
    }

    public void GetValueArray(out string[] value)
    {
        value = new string[] { m_value0, m_value1, m_value2 };
    }
}


public interface I_IFTTTStringEvent
{
    void GetStringEventID(out string stringEventId);
}
public interface I_IFTTTJsonStringEvent: I_IFTTTStringEvent
{
    void GetValue0(out string value);
    void GetValue1(out string value);
    void GetValue2(out string value);
    void GetValueArray(out string [] value);
}