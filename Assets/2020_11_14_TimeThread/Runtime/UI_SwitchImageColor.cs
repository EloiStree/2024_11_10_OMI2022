using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SwitchImageColor : MonoBehaviour
{
    public Image m_linked;
    public Color[] m_color = new Color[] { Color.white, Color.green };
    public int index = 0;


    public void Switch() {
        index++;
        if (index >= m_color.Length)
            index = 0;
        m_linked.color = m_color[index];
    }
    private void Reset()
    {
        m_linked = GetComponent<Image>();
    }
}
