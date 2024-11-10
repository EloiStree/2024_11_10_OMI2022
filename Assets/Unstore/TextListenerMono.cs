using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextListenerMono : MonoBehaviour
{
    public string m_textListener="";
    public bool m_isListening;
    public UnityStringEvent m_textRequested;

    public void Clear() {
        m_textListener = "";
    }

    public void SwitchTheListenerState()
    {
        m_isListening = !m_isListening;
    }

    public void StartListening() {
        m_isListening = true;
    }
    public void StopListening() {

        m_isListening = false;
    }

    public void PushAndClear() {
        Push();
        Clear();
    }
    public void Push() {
        m_textRequested.Invoke(m_textListener);
    }

    public void Append(string text)
    {
        if (m_isListening)
            m_textListener += text;
    }
    public void Append(char c)
    {
        if (m_isListening)
            m_textListener += c;
    }

    public void Set(string text)
    {
        m_textListener = text;
    }
}
