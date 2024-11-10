
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AoeUtilityToJOMI : MonoBehaviour
{

    public Image m_icon;
    public DeleteMeAOETest m_aoeMacro;
    public Sprite sprite;
    public string m_callId;
    private void Start()
    {
        Refresh();
    }

    public void CallMacro(string nameId)
    {
        Refresh();
        if (m_aoeMacro)
            m_aoeMacro.TryToCall(nameId);

    }
    public void CallLinked()
    {
        Refresh();
        if(m_aoeMacro)
        m_aoeMacro.TryToCall(m_callId);

    }

    public void SetIcon(Sprite sprite) {
        m_icon.sprite = sprite;
    }

    private void Refresh()
    {
        if (m_aoeMacro == null)
            m_aoeMacro = GameObject.FindObjectOfType<DeleteMeAOETest>();
    }

    internal void SetCallId(string toCallId)
    {
        m_callId = toCallId;
    }

    private void OnValidate()
    {
        Refresh();
        if(sprite!=null)
        m_icon.sprite = sprite;
    }
}
