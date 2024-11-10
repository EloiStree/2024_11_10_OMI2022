using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FlushInputField : MonoBehaviour
{
    public InputField m_inputTarget;
    public float m_checkTime = 2f;
    public int m_maxSize = 1000;
    private void Awake()
    {
        InvokeRepeating("CheckSize", 0, m_checkTime);
    }

    public void CheckSize() {
        if(m_inputTarget.text.Length>m_maxSize)
        m_inputTarget.text = m_inputTarget.text.Substring(0, m_maxSize);
    }
    void Reset()
    {
        m_inputTarget = GetComponent<InputField>();
    }
}
