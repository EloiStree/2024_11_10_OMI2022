using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowMonitorWithPercentMono : MonoBehaviour
{

    public string m_displayId;
    public RectTransform m_rectToAffect;
    public Text m_displayName;
    public float xL2RPct;
    public float yB2TPct;
    public float widthPct;
    public float heightPct;

    public void SetDisplayNameId(string id)
    {

        m_displayId = id;
        if (m_displayName)
        {
            m_displayName.text = id;
        }
    }
    public void SetDimension(float xL2RPct, float yB2TPct, float widthPct, float heightPct)
    {
        this.xL2RPct = xL2RPct;
        this.yB2TPct = yB2TPct;
        this.widthPct = widthPct;
        this.heightPct = heightPct;

        if (m_rectToAffect)
        {
            m_rectToAffect.anchorMin = new Vector2(xL2RPct, (yB2TPct));
            m_rectToAffect.anchorMax = new Vector2(xL2RPct + widthPct, yB2TPct + heightPct);
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
