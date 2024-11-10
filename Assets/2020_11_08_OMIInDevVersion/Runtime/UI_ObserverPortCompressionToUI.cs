using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ObserverPortCompressionToUI : MonoBehaviour
{

    public UI_AnalogDigitCompressDebug[] m_debugAnalogDigitalUI;
    void Update()
    {
      uint [] portsObserved=  AnalogPortCompressionObserver.GetPortObserved();
        for (int i = 0; i < m_debugAnalogDigitalUI.Length; i++)
        {
            if (i >= portsObserved.Length)
            {
                m_debugAnalogDigitalUI[i].SetAsNotUsed();
            }
            else {
                m_debugAnalogDigitalUI[i].SetPortId(portsObserved[i]);
                for (int j = 0; j < m_debugAnalogDigitalUI[i].GetNumberOfDigit(); j++)
                {
                    bool value;
                    AnalogPortCompressionObserver.GetDigitalState(portsObserved[i], (uint)j, out value, false);
                    m_debugAnalogDigitalUI[i].SetDigit((uint)j,value);

                }
                for (int j = 0; j < m_debugAnalogDigitalUI[i].GetNumberOfAnalog(); j++)
                {
                    short value;
                    AnalogPortCompressionObserver.GetAnalogState(portsObserved[i], (uint)j, out value, 0);
                    m_debugAnalogDigitalUI[i].SetAnalog((uint)j, true, value);

                }

            }
        }


    }
}
