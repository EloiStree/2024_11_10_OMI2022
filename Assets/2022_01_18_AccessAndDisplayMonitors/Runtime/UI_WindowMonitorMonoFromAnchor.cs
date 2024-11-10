using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowMonitorMonoFromAnchor : MonoBehaviour
{
    public string m_displayId;
    public RectTransform m_rectToAffect;
    public Text m_displayName;

    public void SetDisplayNameId(string id) {

        m_displayId = id;
        if (m_displayName) { 
         m_displayName.text = id;
        }
    }
    public void SetDimension(int x, int y, int width, int height) {

        if (m_rectToAffect)
        { 
            m_rectToAffect.anchoredPosition = new Vector2(x, -y );
            m_rectToAffect.sizeDelta = new Vector2(width, height);
        }

    }

    public void Hide()
    {
        m_rectToAffect.gameObject.SetActive(false);
    }
    public void Display()
    {
        m_rectToAffect.gameObject.SetActive(true);
    }
}
