using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompressDigitalAnalogToString : MonoBehaviour
{
    public string[] m_digitalIndexName = new string[] { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", };
    public string[] m_analogIndexName = new string[] { "A1", "A2", "A3", "A4", "A5" };
    public int threshold = 4;
    public string m_active="Down";
    public string m_notActive = "Up";
    public ValueChange m_onValueChange;
    [Header("Debug")]
    public string m_debug;

    public CompressDigitalAndAnalogListener m_state = new CompressDigitalAndAnalogListener();
 [System.Serializable]
    public class ValueChange : UnityEvent<string> { }
    public void OnEnable()
    {
        m_state.AddAnalogListener(WhenAnalogChange);
        m_state.AddDigitalListener(WhenDigitChange);

    }
    public void OnDisable()
    {
        m_state.RemoveAnalogListener(WhenAnalogChange);
        m_state.RemoveDigitalListener(WhenDigitChange);

    }
    public void SetWithCompressStateMessage(string msg)
    {
        m_state.SetState(msg);

    }

    private void WhenDigitChange(string givenName, int index, bool newValue)
    {
        if (index < m_digitalIndexName.Length) {
            m_debug = string.Format("{0} {1}", m_digitalIndexName[index], newValue ? m_active : m_notActive);
            m_onValueChange.Invoke(m_debug);
        }
    }

    private void WhenAnalogChange(string givenName, int index, Digit oldValue, Digit newValue)
    {

        if (index < m_analogIndexName.Length) {
            m_debug = string.Format("{0} {1}", m_analogIndexName[index], (int)newValue > threshold ? m_active : m_notActive);
            m_onValueChange.Invoke(m_debug);
        }
    }
}
