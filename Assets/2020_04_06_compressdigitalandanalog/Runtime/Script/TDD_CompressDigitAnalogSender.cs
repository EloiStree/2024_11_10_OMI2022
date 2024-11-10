using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDD_CompressDigitAnalogSender : MonoBehaviour
{
    public float m_timeBetweenSend = 0.5f;
    public bool[] m_digital = new bool[4];
    public int[] m_analog = new int[2];
    public string m_lastState;
    public CompressDigitAnalogEvent m_newStateEvent;

    [System.Serializable]
    public class CompressDigitAnalogEvent : UnityEvent<string> { }


    // Start is called before the first frame update
IEnumerator Start()
    {
        while (true) {

            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenSend);
            RandChange();
            SendState();
        }
        
    }

    private void SendState()
    {
        string fakeCompress = "#";
        for (int i = 0; i < m_digital.Length; i++)
        {
            fakeCompress += m_digital[i] ? "1" : "0";
        }
        fakeCompress += "|";
        for (int i = 0; i < m_analog.Length; i++)
        {
            fakeCompress += m_analog[i];
        }
        m_lastState = fakeCompress;
        m_newStateEvent.Invoke(fakeCompress);
    }

    private void RandChange()
    {
        int index = 0;

        index = UnityEngine.Random.Range(0, m_digital.Length-1);
        if(m_digital.Length> 0 )
            m_digital[index] = !m_digital[index];

        index = UnityEngine.Random.Range(0, m_analog.Length-1);
        if (m_analog.Length > 0)
            m_analog[index] = UnityEngine.Random.Range(0, 9);

    }
}
